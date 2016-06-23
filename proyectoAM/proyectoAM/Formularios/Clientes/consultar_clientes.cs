using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proyectoAM.Formularios.Clientes
{
    public partial class consultar_clientes : Form
    {
        DataTable table = new DataTable();
        clases.addFactura fact = new clases.addFactura();
        clases.addCliente cli = new clases.addCliente();
        clases.addItem ite = new clases.addItem();
        public static string ide;
        public static string tiempo;
        public static string cliente;

        public consultar_clientes()
        {
            InitializeComponent();
            dtvTabla.DataSource = cli.muestracliente();
            dtvTabla.Columns[0].Visible = false;
        }

        private void consultar_clientes_Load(object sender, EventArgs e)
        {

        }

        private void dtvTabla_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ide = txtBuscar.Text;
            tiempo = dtpTiempo.Value.ToString("dd/MM/yyyy");
            dtvTabla.DataSource = cli.mostrarNombre(ide, tiempo);
        }

        private void dtvTabla_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ide = dtvTabla.CurrentRow.Cells[0].Value.ToString();
            cliente = dtvTabla.CurrentRow.Cells[1].Value.ToString();
            mostrar_factura mf = new mostrar_factura();
            mf.ShowDialog();
        }
    }
}
