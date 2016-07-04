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
                string vence = data[4].ToString();
                string sql = "INSERT INTO items (id_cliente, cantidad, descripcion, precio, fech_venc) VALUES('" + idcli + "','" + cantidad + "','" + descripcion + "','" + precio + "', '"+vence+"')";
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

        public bool llevaraFact(string[] data)
        {
            conecta();

            try
            {
                //id_cliente,
                string idcli;
                string cantidad;
                string descripcion;
                string precio;
                string vence;
                string sql = "";
                 idcli = data[0].ToString();
                 cantidad = data[1].ToString();
                 descripcion = data[2].ToString();
                 precio = data[3].ToString();
                 vence = data[4].ToString();
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
            table.Columns.Add("Fecha Vencimiento");
            return table;
        }

        


        public DataTable muestra(string id, string nombre, string fecha)
        {
            try
            {
                addItems();
                conecta();
                //addColumns();
                // string sql = "SELECT * FROM cliente;";
                string sql = "SELECT DISTINCT factura.id, cliente.nombre, items.cantidad, items.descripcion, items.precio, factura.fech_venc FROM items, cliente, factura WHERE factura.id = '"+id+"' AND factura.id_item=factura.id AND items.id_cliente = cliente.id AND cliente.nombre = '"+nombre+"' AND items.fech_venc = '"+fecha+"'";
           
                cmd = new MySqlCommand(sql, cn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    table.Rows.Add(reader["id"].ToString(), reader["nombre"].ToString(), reader["cantidad"].ToString(), reader["descripcion"].ToString(), reader["precio"].ToString(), reader["fech_venc"].ToString());
                }
                reader.Close();
                cn.Close();
                return table;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error en: " + ex);
            }
            return table;
        }

        public DataTable muestraCo(string id, string nombre, string fecha)
        {
            try
            {
                addItems();
                conecta();
                //addColumns();
                // string sql = "SELECT * FROM cliente;";
                 string sql = "SELECT DISTINCT cotizacion.id, cliente.nombre, items.cantidad, items.descripcion, items.precio, cotizacion.fech_venc FROM items, cliente, cotizacion WHERE cotizacion.id = '" + id + "' AND cotizacion.id_item=cotizacion.id AND items.id_cliente = cliente.id AND cliente.nombre = '" + nombre + "' AND items.fech_venc = '" + fecha + "'";
                
                cmd = new MySqlCommand(sql, cn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    table.Rows.Add(reader["id"].ToString(), reader["nombre"].ToString(), reader["cantidad"].ToString(), reader["descripcion"].ToString(), reader["precio"].ToString(), reader["fech_venc"].ToString());
                }
                reader.Close();
                cn.Close();
                return table;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error en: " + ex);
            }
            return table;
        }

        DataTable addColumns()
        {
            table = new DataTable();
            //Parte Inferior de la Tabla
            table.Columns.Add("Cantidad");
            table.Columns.Add("Descripcion");
            table.Columns.Add("");
            table.Columns.Add("Precio");
            table.Columns.Add("Precio-SinMoneda");
            /*  tabla.Columns.Add("Sub-Total");
             tabla.Columns.Add("ITBIS");
             tabla.Columns.Add("Total");*/

            return table;
        }

        public DataTable PasaCo(string id, string nombre, string fecha)
        {
            try
            {
                //addItems();
                conecta();
                addColumns();
                // string sql = "SELECT * FROM cliente;";
                string sql = "SELECT DISTINCT  items.cantidad, items.descripcion, items.precio FROM items, cliente, cotizacion WHERE cotizacion.id = '" + id + "' AND cotizacion.id_item=cotizacion.id AND items.id_cliente = cliente.id AND cliente.nombre = '" + nombre + "' AND items.fech_venc = '" + fecha + "'";

                cmd = new MySqlCommand(sql, cn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string cantidad = reader["cantidad"].ToString(), descripcion = reader["descripcion"].ToString(), precio = reader["precio"].ToString();
                    table.Rows.Add( reader["cantidad"].ToString(), reader["descripcion"].ToString(), "", reader["precio"].ToString());
                }
                reader.Close();
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
