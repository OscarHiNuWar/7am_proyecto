using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using proyectoAM.clases;
using System.IO;

namespace proyectoAM
{
    public partial class Form1 : Form
    {
        double total; //Aca va el precio total de la suma de todos los precios del DataGridView
        double subtotal=0;
        double itbis = 0.18;
        double subitbis=0;
        double precio=0;
        int cantidad=0;
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
        addFactura agrego = new addFactura();
        // 
        string user;
        int id=0;
        

        DataTable addColumns()
        {
            tabla = new DataTable();
            tabla.Columns.Add("ID");
            tabla.Columns.Add("Moneda");
            tabla.Columns.Add("Trabajo a Realizar");
            tabla.Columns.Add("Condiciones de Pago");
            tabla.Columns.Add("Vence");
            tabla.Columns.Add("Precio");
            tabla.Columns.Add("Cantidad");
           /*tabla.Columns.Add("Descripcion");*/
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
            cbPago.Items.Add("Cheque");
        }

        void addMoneda()
        {
            cbMoneda.Items.Add("RD$");
            cbMoneda.Items.Add("$");
        }

        void reset()
        {
            txtPrecio.Text = null;
            //btnAgregar.Enabled = false;
        }

        public Form1()
        {
            InitializeComponent();
            btnEliminar.Enabled = false;
            btnExportar.Enabled = false;
            //DataTable tabla = agrego.muestra();
            Tbla.DataSource = addColumns();
            addNombre();
            addCompania();
            addTrabajo();
            addCondicionPago();
            addMoneda();
            user = Environment.UserName.ToString();
            Tbla.Columns[0].Visible = false;
        }



        private void btnAgregar_Click(object sender, EventArgs e)
        {
            /* if (agrego.agregar(new string[] { nombre, rnc, telefono,email,compania,moneda,trabajo,pago,vence }))
             {
                 MessageBox.Show("Factura Guardada");
             }*/
            btnExportar.Enabled = true;
            id = id + 1;
            
            cantidad = Convert.ToInt32(nudCantidad.Text.ToString());
            precio = double.Parse(txtPrecio.Text);

            
            subtotal = (precio * cantidad) + subtotal;
            subitbis = (subtotal * itbis) + subitbis;
            total = (subtotal + subitbis);
            

            textBox5.Text = Convert.ToString(subtotal);
            textBox1.Text = Convert.ToString(subitbis);
            textBox2.Text = Convert.ToString(total);
            moneda = cbMoneda.Text.ToString();
            trabajo = cbTrabajo.SelectedItem.ToString();
            pago = cbPago.SelectedItem.ToString();
            vence = dtVence.Value.ToString().Remove(8);

            tabla.Rows.Add("",moneda, trabajo, pago, vence, Convert.ToString(precio), Convert.ToString(cantidad));

            reset();

        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            btnAgregar.Enabled = true;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            
            FileInfo info = new FileInfo("C:\\Users\\" + user + "/Documents/Plan.csv");
            if (info.Exists)
            {
                info.Delete();


            }
            StreamWriter fichero = new StreamWriter("C:\\Users\\" + user + "/Documents/Plan.csv", true, Encoding.UTF8);

            // fichero.Write("," + lblMes.Text + "\n");
            fichero.Write("FACTURA\n\n");

            if (cbNombre.SelectedItem != null)
            {
                fichero.Write("NOMBRE:          " + cbNombre.SelectedItem.ToString()+ "\n\n");
            }
            else
            {
                fichero.Write("NOMBRE:          " + cbNombre.Text.ToString() + "\tFECHA DE VENCIMINETO: "+dtVence.Value.ToString()+"\n\n");
            }

            if (cbCompania.SelectedItem != null)
            { 
                fichero.Write("COMPANIA:            " + cbCompania.SelectedItem.ToString() + "\n\n");
            }
            else
            {
                fichero.Write("COMPANIA:            " + cbCompania.Text.ToString() + "\n\n");
            }
            
            fichero.Write("NCF:         " + textBox3.Text.ToString() + "\n\n");
            fichero.Write("RNC:           " + txtRnc.Text.ToString() + "\n\n");
            fichero.Write("TELEFONO:            " + txtTelefono.Text.ToString() + "\n\n");
            fichero.Write("EMAIL:           " + txtEmail.Text.ToString()+ "\n\n");

            

            fichero.Write("MONEDA, TRABAJO, CONDICIONES DE PAGO, PRECIO, CANTIDAD\n");

            
            foreach (DataGridViewRow rows in Tbla.Rows)
            {
                
                fichero.Write(rows.Cells[1].Value.ToString() + " ," + rows.Cells[2].Value.ToString() + " ," + rows.Cells[3].Value.ToString() + " ," + rows.Cells[4].Value.ToString() + " ," + rows.Cells[5].Value.ToString() + "\n");
            }

            fichero.Write("\n\n\nSUB - TOTAL:           "+textBox5.Text.ToString()+"\n");
            fichero.Write("ITBIS:           " + textBox1.Text.ToString() + "\n");
            fichero.Write("TOTAL:           " + textBox2.Text.ToString()+"\n");


           fichero.Close();
            if (info.Exists)
            {
                MessageBox.Show("Factura exportada");
            }
        }

        
        private void txtPrecio_KeyPress_1(object sender, KeyPressEventArgs e)
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
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
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
        }

        private void Tbla_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            btnEliminar.Visible = true;
            btnEliminar.Enabled = true;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Estas seguro de eliminar este Dia feriado", "Eliminar Feriado", MessageBoxButtons.YesNo) == DialogResult.Yes)
            { 
                foreach (DataGridViewRow item in this.Tbla.SelectedRows)
            {
                Tbla.Rows.RemoveAt(item.Index);
            }
            }
        }


        /* private void Tbla_DoubleClick(object sender, EventArgs e)
         {
             string id = Tbla.CurrentRow.Cells[0].Value.ToString();

             btnExportar.Enabled = true;
         }*/
    }
    
}
