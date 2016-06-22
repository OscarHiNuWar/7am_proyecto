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
    public partial class add_cliente : Form
    {
        clases.addCliente cliente = new clases.addCliente();

        public add_cliente()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (cliente.agregarCliente(new string[] { txtNombre.Text, txtRnc.Text, txtTel.Text, txtEmail.Text }))
            {
                MessageBox.Show("Cliente Agregado con exito.");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
