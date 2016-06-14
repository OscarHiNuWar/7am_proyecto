using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using proyectoAM.clases;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace proyectoAM.clases
{
    class addFactura
    {
        int id = 0;
        DataTable tabla;
        MySqlDataReader reader;
        MySqlConnection cn;
        MySqlCommand cmd;

        public void conecta() { cn = conDB.conecta();  cn.Open(); }

        void addColumns()
        {
            tabla = new DataTable();
            tabla.Columns.Add("Cantidad");
            tabla.Columns.Add("Descripcion");
            tabla.Columns.Add("Total");

        }


        public bool agregar(string[] data)
         {
            conecta();
       
        try
        {
            string sql = "INSERT INTO factura (id, id_detalle, fecha, moneda, trabajo, conpago, vence, total) VALUES('" + data[0] + "','" + data[1] + "','" + data[2] + "','" + data[3] + "','" + data[4] + "','" + data[5] + "','" + data[6] + "','" + data[7] + "');SELECT last_insert_rowid()";
            cmd = new MySqlCommand(sql, cn);

            id = Convert.ToInt32(cmd.ExecuteScalar());
            cn.Close();
            return true;

        }
        catch (MySqlException ex)
        {
            MessageBox.Show(ex.ToString());
        }
        return false;
    }

        public bool modifica(string[] data)
        {
            conecta();
            try
            {
                string sql = "UPDATE FROM factura (id, id_detalle, fecha, moneda, trabajo, conpago, vence, total) VALUES('" + data[0] + "','" + data[1] + "','" + data[2] + "','" + data[3] + "','" + data[4] + "','" + data[5] + "','" + data[6] + "','" + data[7] + "');SELECT last_insert_rowid()";
                cmd = new MySqlCommand(sql, cn);

                id = Convert.ToInt32(cmd.ExecuteScalar());
                cn.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error en: "+ex);
            }

            return false;
        }

        public void delete(string id)
        {
            conecta();
            try
            {
                string sql = "DELETE FROM factura WHERE id_detalle="+id+"; DELETE from detalle WHERE id="+id+";";
                cmd = new MySqlCommand(sql, cn);

                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch(MySqlException ex)
            {
                MessageBox.Show("Error en: "+ex);
            }

        }

       /* public void mostrar(string id)
        {
            conecta();

            try
            {
                string sql = "SELECT * FROM factura WHERE id_detalle = "+id+";";
                cmd = new MySqlCommand(sql, cn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                     tabla.Rows.Add(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), reader[7].ToString());
                }
            }
            catch(MySqlException ex)
            {
                MessageBox.Show("Error en: "+ex);
            }
        }*/

        public DataTable muestra()
        {
            try
            {
                conecta();
                addColumns();
                string sql = "SELECT * FROM factura ORDER BY nombre ASC";
                cmd = new MySqlCommand(sql, cn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    tabla.Rows.Add(reader["Cantidad"].ToString(), reader["Descripcion"].ToString(), reader["Total"].ToString());
                }
                cn.Close();
                return tabla;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error en: " + ex);
            }
            return tabla;
        }

    }
}
