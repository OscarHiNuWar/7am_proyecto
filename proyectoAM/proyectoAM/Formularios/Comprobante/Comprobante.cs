using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proyectoAM.Formularios.Comprobante
{
    public partial class Comprobante : Form
    {
        clases.comprobante com = new clases.comprobante();
        public Comprobante()
        {
            InitializeComponent();
            addCompro();
            comboBox1.SelectedItem = "Negocio";
        }

        void addCompro()
        {
            comboBox1.Items.Add("Negocio");
            comboBox1.Items.Add("Gubernamental");
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            int valor = int.Parse( numericUpDown1.Value.ToString());
            string tip = comboBox1.Text;
            int fin = int.Parse(textBox2.Text);
            if (com.agregarCompro(new string[] { tip, textBox1.Text, valor.ToString("D8"),valor.ToString("D8"), fin.ToString("D8") }))
            {
                MessageBox.Show("Comprobante guardado.");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
