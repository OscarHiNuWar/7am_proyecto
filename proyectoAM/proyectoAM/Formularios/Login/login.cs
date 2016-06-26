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

namespace proyectoAM.Formularios.Login
{
    public partial class login : Form
    {
        MySqlConnection cn;
        MySqlCommand cmd;
        DataTable tb;
        public login()
        {
            InitializeComponent();
            
        }

        public void conecta() { cn = conDB.conecta(); cn.Open(); }

        public void logi()
        {
            conecta();
            MySqlDataAdapter sql = new MySqlDataAdapter("SELECT user, pass, class FROM usuarios WHERE user='" + textBox1.Text + "' and pass='" + textBox2.Text + "'", cn);
            /*cmd = new MySqlCommand(sql, cn);
            cmd.ExecuteNonQuery();*/
            tb = new DataTable();
            sql.Fill(tb);
            if (tb.Rows.Count > 0)
            {
                if (tb.Rows[0]["class"].ToString() == "administrador")
                {
                    MessageBox.Show("Bienvenido");
                    this.Hide();
                    menuprincipal me = new menuprincipal();
                    me.Show();
                }
                else
                {
                    MessageBox.Show("Usuario o Contraseña incorrecto");
                }


                cn.Close();
            }
            else
            {
                //MessageBox.Show("Usuario o Contraseña incorrecto");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            logi();

        }
    }
}
