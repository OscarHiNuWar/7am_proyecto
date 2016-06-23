using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proyectoAM.clases
{
    class addItem
    {
        public static string id;
        int ide = 0;
        DataTable table;
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
                string idcli = data[0].ToString();
                string cantidad = data[1].ToString();
                string descripcion = data[2].ToString();
                string precio = data[3].ToString();
                string sql = "INSERT INTO items (id_cliente, cantidad, descripcion, precio) VALUES('" + idcli + "','" + cantidad + "','" + descripcion + "','" + precio + "')";
                cmd = new MySqlCommand(sql, cn);
                cmd.ExecuteNonQuery();

               // ide = Convert.ToInt32(cmd.ExecuteScalar());
                cn.Close();
                return true;

            }
            catch (MySqlException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
            return false;
        }

        DataTable addItems()
        {
            table = new DataTable();
            table.Columns.Add("ID");
            table.Columns.Add("Cliente");
            table.Columns.Add("Cantidad");
            table.Columns.Add("Descripcion");
            table.Columns.Add("Precio");
            return table;
        }

        public DataTable client()
        {

            return table;
        }


        public DataTable muestra(string id, string nombre)
        {
            try
            {
                addItems();
                conecta();
                //addColumns();
                // string ide = "SELECT id_cliente FROM cliente;";
                string sql = "SELECT cliente.id, cliente.nombre, items.cantidad, items.descripcion, items.precio FROM items JOIN cliente WHERE cliente.id='"+id+"' and cliente.nombre='"+nombre+ "' and items.id_cliente ='" + id +"'";

                cmd = new MySqlCommand(sql, cn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    table.Rows.Add(reader["id"].ToString(), reader["nombre"].ToString(), reader["cantidad"].ToString(), reader["descripcion"].ToString(), reader["precio"].ToString());
                }
                cn.Close();
                return table;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error en: " + ex);
            }
            return table;
        }
    }
}
