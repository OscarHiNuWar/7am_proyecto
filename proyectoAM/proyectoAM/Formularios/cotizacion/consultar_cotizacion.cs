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
    public partial class consultar_cotizacion : Form
    {
        DataTable table = new DataTable();
        clases.addFactura fact = new clases.addFactura();
        clases.addCliente cli = new clases.addCliente();
        clases.addItem ite = new clases.addItem();
        public static string ide;
        public static string tiempo;
        public static string cliente;


        public consultar_cotizacion()
        {
            InitializeComponent();
            dtvTabla.DataSource = cli.mostrarCotiza();
            //ide = dtvTabla.CurrentRow.Cells[0].Value.ToString();
            //cliente = dtvTabla.CurrentRow.Cells[1].Value.ToString();
            dtvTabla.Columns[0].Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            cliente = txtBuscar.Text;
            tiempo = dtpTiempo.Value.ToString("dd/M/yyyy");
            dtvTabla.DataSource = cli.muestracliente(cliente, tiempo);
        }

        private void dtvTabla_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ide = dtvTabla.CurrentRow.Cells[0].Value.ToString();
            cliente = dtvTabla.CurrentRow.Cells[1].Value.ToString();
            //tiempo = dtpTiempo.Value.ToString("dd/M/yy");
            tiempo = dtvTabla.CurrentRow.Cells[2].Value.ToString();
            mostrar_cotizacion mc = new mostrar_cotizacion();
            mc.ShowDialog();
        }
    }
}
