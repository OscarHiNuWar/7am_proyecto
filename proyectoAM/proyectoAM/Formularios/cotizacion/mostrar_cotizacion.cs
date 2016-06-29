using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proyectoAM.Formularios.cotizacion
{
    public partial class mostrar_cotizacion : Form
    {
        DataTable table = new DataTable();
        clases.addCliente cli = new clases.addCliente();
        clases.addItem ite = new clases.addItem();
        public string id = consultar_cotizacion.ide;
        public string nombre = consultar_cotizacion.cliente;
        public string fecha = consultar_cotizacion.tiempo;
        MySqlDataReader reader;
        MySqlConnection cn;
        MySqlCommand cmd;
        string rc;
        double precio, itbis, total;
        string[] pasar;

        clases.addFactura fact = new clases.addFactura();
        

        public void conecta() { cn = conDB.conecta(); cn.Open(); }

        public void mandaDatos()
        {
            pasar[0] = txtNombre.ToString();
            foreach (DataGridViewRow row in dgvFact.Rows)
            {

                pasar[1] = row.Cells[0].ToString();
                pasar[2] = row.Cells[1].ToString();
                pasar[3] = row.Cells[3].ToString();
                pasar[4] = row.Cells[4].ToString();
               // pasar[5] = row.Cells[5].ToString();
            }
            fact.iniciarFactura(pasar);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mandaDatos();
        }

        public mostrar_cotizacion()
        {
            InitializeComponent();
            dgvFact.DataSource = ite.muestra(id, nombre, fecha);
            rn();
            txtRnc.Text = rc;
            string sprecio;

            foreach (DataGridViewRow row in dgvFact.Rows)
            {
                sprecio = row.Cells[4].Value.ToString();
                //sprecio = sprecio.Remove(0, 3);
                precio = double.Parse(sprecio) + precio;
            }
            //string sprecio = dgvFact.RowsAdded.Cells[3].Value.ToString();

            //precio = double.Parse(sprecio);
            itbis = precio * 0.18;
            total = precio + itbis;

            txtSubTotal.Text = precio.ToString();
            txtItbis.Text = itbis.ToString();
            txtTotal.Text = total.ToString();

            dgvFact.Columns[0].Visible = false;
        }

        public string rn()
        {
            conecta();

            try
            {
                string sql = "SELECT rnc FROM cliente WHERE nombre='" + nombre + "'";
                cmd = new MySqlCommand(sql, cn);
                reader = cmd.ExecuteReader();
                reader.Read();
                rc = reader["rnc"].ToString(); ;
                cn.Close();
                return rc;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error en: " + ex);
            }
            return null;
        }
    }
}
