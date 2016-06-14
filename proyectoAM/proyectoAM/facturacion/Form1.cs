using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using proyectoAM.clases;
using System.IO;

namespace Santana_Version_Final.factura
{
    public partial class Form1 : Form
    {
         
        string nombre; //Aca va el dato "nombre" de la tabla cliente
        string rnc; //Aca va el dato "RNC" de la tabla cliente
        string telefono;
        string email;
        string compania;
        string moneda;
        string trabajo;
        string pago;
        string vence; //Este dato puede variar a uno que funcione con DataTimePicker
        DataTable tabla;
        //addFactura agrego = new addFactura();
        // 
        string user;

        double subtotal; //Aca va el sub-total de la suma de todos los precios del DataGridView
        double precio; //Aca va el precio de los productos del DataGridView
        double itbis = 0.13; //Aca va el ITBIS, se multiplica con subtotal
        double subitebis;
        double total; //Aca va el precio total de la suma de todos los precios del DataGridView

        DataTable addColumns()
        {
            tabla = new DataTable();
            tabla.Columns.Add("ID");
            tabla.Columns.Add("Nombre");
            tabla.Columns.Add("RNC");
            tabla.Columns.Add("Telefono");
            tabla.Columns.Add("E-mail");
            tabla.Columns.Add("Compañia");
            tabla.Columns.Add("Moneda");
            tabla.Columns.Add("Trabajo a Realizar");
            tabla.Columns.Add("Condiciones de Pago");
            tabla.Columns.Add("Vence");
            /* tabla.Columns.Add("Precio");
             tabla.Columns.Add("Cantidad");
             tabla.Columns.Add("Descripcion");*/
            tabla.Columns.Add("Total");

            return tabla;
        }

        void addNombre()
        {
            cbNombre.Items.Add("Oscar");
            cbNombre.Items.Add("Jairo");
            cbNombre.Items.Add("Kelly");
            cbNombre.Items.Add("Daniel");
        }

        void addCompania()
        {
            cbCompania.Items.Add("7AM");
            cbCompania.Items.Add("Coca-cola");
            cbCompania.Items.Add("Pepsi");
            cbCompania.Items.Add("Infotep");
        }

        void addTrabajo()
        {
            cbTrabajo.Items.Add("Desarrollo de Pagina Web");
            cbTrabajo.Items.Add("Continuacion de Red Social");
            cbTrabajo.Items.Add("Posicionamiento");
            //cbTrabajo.Items.Add("Infotep");
        }

        void addCondicionPago()
        {
            cbPago.Items.Add("Efectivo");
            cbPago.Items.Add("Credito");
        }

        void addMoneda()
        {
            cbMoneda.Items.Add("RD$");
            cbMoneda.Items.Add("$");
        }

        void reset()
        {
            txtPrecio.Text = null;


            cbNombre.Update();
            txtRnc.Text = null;
            txtTelefono.Text = null;
            txtEmail.Text = null;
            cbCompania.Update();
            cbMoneda.Text = null;
            cbTrabajo.Update();
            cbPago.Update();

        }

        public Form1()
        {
            InitializeComponent();
            // btnAgregar.Enabled = false;
            btnEliminar.Enabled = false;
            btnExportar.Enabled = false;
            //DataTable tabla = agrego.muestra();
            Tbla.DataSource = addColumns();
            Tbla.Columns[0].Visible = false;
            addNombre();
            addCompania();
            addTrabajo();
            addCondicionPago();
            addMoneda();
            user = Environment.UserName.ToString();
        }



        private void btnAgregar_Click(object sender, EventArgs e)
        {
            /* if (agrego.agregar(new string[] { nombre, rnc, telefono,email,compania,moneda,trabajo,pago,vence }))
             {
                 MessageBox.Show("Factura Guardada");
             }*/

            precio = float.Parse(txtPrecio.Text);
            subtotal = precio + subtotal;
            subitebis = precio * itbis;
            total = subitebis + subtotal;
            textBox5.Text = Convert.ToString(subtotal);
            nombre = cbNombre.SelectedItem.ToString();
            rnc = txtRnc.Text.ToString();
            telefono = txtTelefono.Text.ToString();
            email = txtEmail.Text.ToString();
            compania = cbCompania.SelectedItem.ToString();
            moneda = cbMoneda.Text.ToString();
            trabajo = cbTrabajo.SelectedItem.ToString();
            pago = cbPago.SelectedItem.ToString();
            vence = dtVence.Value.ToString().Remove(8);

            tabla.Rows.Add(nombre, rnc, telefono, email, compania, moneda, trabajo, pago, vence, Convert.ToString(precio));

            reset();

        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
            btnAgregar.Enabled = true;
        }



        private void button1_Click(object sender, EventArgs e)
        {

            FileInfo info = new FileInfo("C:\\Users\\" + user + "/Documents/Plan.csv");
            if (info.Exists)
            {
                info.Delete();


            }
            StreamWriter fichero = new StreamWriter("C:\\Users\\" + user + "/Documents/Plan.ods", true, Encoding.UTF8);

            // fichero.Write("," + lblMes.Text + "\n");
            fichero.Write("FACTURA\n, NOMBRE, RNC, TELEFONO, EMAIL, CLIENTE, MONEDA, TRABAO CONDICIONES DE PAGO, FECHA DE VENCIMINETO, TOTAL");

            
            foreach (DataGridViewRow rows in tabla.Rows)
            {
                if (rows == null || rows.Cells[0].Value == null)
                    continue;
                fichero.Write(rows.Cells[0].Value.ToString() + " ," + rows.Cells[1].Value.ToString() + " ," + rows.Cells[3].Value.ToString() + " ," + rows.Cells[4].Value.ToString() + " ," + rows.Cells[5].Value.ToString() + " ," + rows.Cells[6].Value.ToString() + " ,   ," + rows.Cells[7].Value.ToString() + " ,  ," + rows.Cells[8].Value.ToString() + "\n");
            }
            
           // fichero.Close();
            if (info.Exists)
            {
                MessageBox.Show("Plan exportado");
            }
        }

        private void Tbla_DoubleClick(object sender, EventArgs e)
        {
            string id_detalle = Tbla.CurrentRow.Cells[0].Value.ToString();
           
            btnExportar.Enabled = true;
        }
    }
    
}
