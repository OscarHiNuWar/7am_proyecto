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
<<<<<<< HEAD
        DataTable table = new DataTable();
        clases.addCliente cli = new clases.addCliente();

        public consultar_clientes()
        {
            InitializeComponent();
            dtvTabla.DataSource = cli.muestra();
        }

        private void consultar_clientes_Load(object sender, EventArgs e)
        {

=======
        public consultar_clientes()
        {
            InitializeComponent();
>>>>>>> origin/master
        }
    }
}
