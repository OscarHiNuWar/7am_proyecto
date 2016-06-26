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
        bool check;

        public void conecta() { cn = conDB.conecta();  cn.Open(); }

      

        public bool agregarFactura(string[] data)
        {
            conecta();

            try
            {
                //id_cliente,

                string sql = "INSERT INTO factura (id_item, tipo_pago, fech_venc) VALUES('" + data[0] + "','" + data[1] + "','" + data[2] + "')";
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

       public void mostrarfactura(string id)
        {
            conecta();

            try
            {
                string sql = "SELECT * FROM factura WHERE id_cliente = "+id+";";
                cmd = new MySqlCommand(sql, cn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                     tabla.Rows.Add(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString());
                }
            }
            catch(MySqlException ex)
            {
                MessageBox.Show("Error en: "+ex);
            }
        }

        DataTable addColumns()
        {
            tabla = new DataTable();
            //Parte Inferior de la Tabla
            tabla.Columns.Add("Cantidad");
            tabla.Columns.Add("Descripcion");
            tabla.Columns.Add("");
            tabla.Columns.Add("Precio");
            return tabla;
        }

        DataTable addFact()
        {
            tabla = new DataTable();
            //Parte Inferior de la Tabla
            tabla.Columns.Add("ID");
            tabla.Columns.Add("Cliente");
            tabla.Columns.Add("Tipo Pago");
            tabla.Columns.Add("Fecha Vencimineto");
            

            return tabla;
        }

        public DataTable muestra()
        {
            try
            {
                addFact();
                conecta();
                //addColumns();
                // string ide = "SELECT id_cliente FROM cliente;";
                string sql = "SELECT cliente.id, cliente.nombre, tipo_pago, fech_venc FROM `factura` FULL JOIN cliente ON cliente.id = id_cliente";

                cmd = new MySqlCommand(sql, cn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    
                    tabla.Rows.Add(reader["id"].ToString(), reader["nombre"].ToString(), reader["tipo_pago"].ToString(), reader["fech_venc"].ToString());
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
