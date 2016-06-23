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
    public partial class mostrar_factura : Form
    {
        DataTable table = new DataTable();
        clases.addCliente cli = new clases.addCliente();
        clases.addItem ite = new clases.addItem();
        public string id = consultar_clientes.ide;
        public string nombre = consultar_clientes.cliente;

        public mostrar_factura()
        {
            InitializeComponent();
            dgvFact.DataSource = ite.muestra(id, nombre);
            dgvFact.Columns[0].Visible = false;
        }
    }
}
