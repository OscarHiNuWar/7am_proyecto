using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proyectoAM.Formularios.Alerta
{
    public partial class alerta : Form
    {
        string te;

        public alerta()
        {
            InitializeComponent();
            
        }

        private void responderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void alerta_Move(object sender, EventArgs e)
        {
            te = textBox1.Text;
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                notifyIcon2.ShowBalloonTip(3000, "Hello!", te, ToolTipIcon.Info);
            }
        }

        private void alerta_Load(object sender, EventArgs e)
        {
            //this.Hide();
        }

        private void contextMenuStrip1_DoubleClick(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();

        }
    }
}
