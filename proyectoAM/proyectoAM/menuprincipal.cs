using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proyectoAM
{
    public partial class menuprincipal : Form
    {
        private int childFormNumber = 0;
       

        public menuprincipal()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            /*Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;*/
            
            Factura fact = new Factura();
            fact.MdiParent = this;
            try
            {
                fact.Show();
            }
            catch { }
            
        }

        private void OpenFile(object sender, EventArgs e)
        {
            Cotizacion cotc = new Cotizacion();
            cotc.MdiParent = this;
            cotc.Show();
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Formularios.Clientes.consultar_clientes consulta = new Formularios.Clientes.consultar_clientes();
            consulta.MdiParent = this;
            consulta.Show();
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Formularios.Clientes.add_cliente adcli = new Formularios.Clientes.add_cliente();
            adcli.MdiParent = this;
            adcli.Show();
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Formularios.Alerta.alerta al = new Formularios.Alerta.alerta();
           // al.MdiParent = this;
            //al.Show();
            al.Hide();
        }

        private void menuprincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Formularios.Comprobante.Comprobante com = new Formularios.Comprobante.Comprobante();
            com.Show();
        }

        private void printSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Formularios.cotizacion.consultar_cotizacion conco = new Formularios.cotizacion.consultar_cotizacion();
            conco.Show();
        }

        private void consultarFacturaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Formularios.Clientes.ver_clientes ver = new Formularios.Clientes.ver_clientes();
            ver.MdiParent = this;
            ver.Show();
        }
    }
}
