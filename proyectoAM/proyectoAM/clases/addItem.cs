using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyectoAM.clases
{
    class addItem
    {
        int id = 0;
        //DataTable tabla;
        MySqlDataReader reader;
        MySqlConnection cn;
        MySqlCommand cmd;
        bool check;
        public void conecta() { cn = conDB.conecta(); cn.Open(); }

        ///Area de Factura
        ///Ojo con los cambios


        public bool agregarItems(string[] data)
        {
            conecta();

            try
            {
                //id_cliente,

                string sql = "INSERT INTO items (cantidad, descripcion, precio) VALUES('" + data[0] + "','" + data[1] + "','" + data[2] + "');";
                cmd = new MySqlCommand(sql, cn);

                id = Convert.ToInt32(cmd.ExecuteScalar());
                cn.Close();
                return true;

            }
            catch (MySqlException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
            return false;
        }
    }
}
