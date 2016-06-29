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
    public partial class ver_clientes : Form
    {
        clases.addCliente cli = new clases.addCliente();

     

        public ver_clientes()
        {
            InitializeComponent();
            dgvClient.DataSource = cli.mostrarCliente();
            dgvClient.Columns[0].Visible = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dgvClient.DataSource = cli.buscarCliente(textBox1.Text);

        }
    }
}
