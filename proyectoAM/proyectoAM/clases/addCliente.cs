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
    class addCliente
    {
        int id = 0;
        DataTable tabla;
        MySqlDataReader reader;
        MySqlConnection cn;
        MySqlCommand cmd;
        bool check;

        public void conecta() { cn = conDB.conecta(); cn.Open(); }

        DataTable addCli()
        {
            tabla = new DataTable();
            //Parte Inferior de la Tabla
            tabla.Columns.Add("id");
            tabla.Columns.Add("Cliente");
            tabla.Columns.Add("Fecha Vencimiento");

            return tabla;
        }


        public bool agregarCliente(string[] data)
    {
        conecta();

        try
        {
            string sql = "INSERT INTO cliente (nombre, rnc, telefono, correo) VALUES('" + data[0] + "','" + data[1] + "','" + data[2] + "','" + data[3] + "');";
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
            MessageBox.Show("Error en: " + ex);
        }

        return false;
    }

    public void delete(string id)
    {
        conecta();
        try
        {
            string sql = "DELETE FROM factura WHERE id_detalle=" + id + "; DELETE from detalle WHERE id=" + id + ";";
            cmd = new MySqlCommand(sql, cn);

            cmd.ExecuteNonQuery();
            cn.Close();
        }
        catch (MySqlException ex)
        {
            MessageBox.Show("Error en: " + ex);
        }

    }

    

    public DataTable mostrarNombre()
    {
        conecta();

        try
        {
            addCli();
                // string sql = "SELECT factura.id, cliente.nombre, factura.tipo_pago, factura.fech_venc FROM `factura`, cliente, items WHERE cliente.nombre=cliente.nombre and cliente.id='" + id + "' and items.id_cliente=cliente.id and factura.id_item = items.id";
                //SELECT factura.id, cliente.nombre, factura.tipo_pago, factura.fech_venc FROM `factura`, cliente, items WHERE items.id = factura.id_item and cliente.id = items.id_cliente and factura.fech_venc='22/6/16'
<<<<<<< HEAD
                string sql = "SELECT DISTINCT factura.id, cliente.nombre, factura.tipo_pago, factura.fech_venc FROM `factura`, cliente, items WHERE items.id_cliente = cliente.id and factura.fech_venc = items.fech_venc ";
=======
                string sql = "SELECT factura.id, cliente.nombre, factura.tipo_pago, factura.fech_venc FROM `factura`, cliente, items WHERE items.id_cliente = cliente.id and factura.fech_venc = items.fech_venc ";
>>>>>>> origin/master
                cmd = new MySqlCommand(sql, cn);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                    tabla.Rows.Add(reader["id"].ToString(), reader["nombre"].ToString(), reader["fech_venc"].ToString());
            }
                return tabla;
        }
        catch (MySqlException ex)
        {
            MessageBox.Show("Error en: " + ex);
        }
            return tabla;
    }
<<<<<<< HEAD

        public DataTable mostrarCotiza()
        {
            conecta();

            try
            {
                addCli();
                // string sql = "SELECT factura.id, cliente.nombre, factura.tipo_pago, factura.fech_venc FROM `factura`, cliente, items WHERE cliente.nombre=cliente.nombre and cliente.id='" + id + "' and items.id_cliente=cliente.id and factura.id_item = items.id";
                //SELECT factura.id, cliente.nombre, factura.tipo_pago, factura.fech_venc FROM `factura`, cliente, items WHERE items.id = factura.id_item and cliente.id = items.id_cliente and factura.fech_venc='22/6/16'
                string sql = "SELECT DISTINCT cotizacion.id, cliente.nombre, cotizacion.tipo_pago, cotizacion.fech_venc FROM `cotizacion`, cliente, items WHERE items.id_cliente = cliente.id and cotizacion.fech_venc = items.fech_venc ";
                cmd = new MySqlCommand(sql, cn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    tabla.Rows.Add(reader["id"].ToString(), reader["nombre"].ToString(), reader["fech_venc"].ToString());
                }
                return tabla;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error en: " + ex);
            }
            return tabla;
        }
=======
>>>>>>> origin/master
        public string mostrarrnc(string nombre)
        {
            conecta();

            try
            {
                string sql = "SELECT rnc FROM cliente WHERE nombre='" + nombre + "'";
                cmd = new MySqlCommand(sql, cn);
                reader = cmd.ExecuteReader();
<<<<<<< HEAD
                string rc = reader.GetString(sql);
=======
                string rc = reader.GetString(nombre);
>>>>>>> origin/master
                cn.Close();
                return rc;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error en: " + ex);
            }
            return nombre;
        }

        DataTable addColumns()
        {
            tabla = new DataTable();
            //Parte Inferior de la Tabla
            tabla.Columns.Add("id");
            //tabla.Columns.Add("ID Items");
            tabla.Columns.Add("Cliente");
            tabla.Columns.Add("Tipo de Pago");
            tabla.Columns.Add("Fecha Vencimiento");

            /* tabla.Columns.Add("id_item");
             tabla.Columns.Add("tipo_pago");
             */

            return tabla;
        }



        public DataTable muestracliente(string nombre, string fecha)
        {
        try
        {
            addColumns();
            conecta();
                //addColumns();
                // string sql = "SELECT `nombre` FROM `cliente` WHERE 1";
<<<<<<< HEAD
                string sql = "SELECT factura.id, cliente.nombre, factura.tipo_pago, factura.fech_venc FROM `factura`, cliente, items WHERE cliente.nombre LIKE '%"+nombre+"%' or factura.fech_venc LIKE '%"+fecha+"%'";
=======
                string sql = "SELECT factura.id, cliente.nombre, factura.tipo_pago, factura.fech_venc FROM `factura`, cliente, items WHERE cliente.nombre LIKE '%"+nombre+"%' or factura.fech_venc LIKE '%"+fecha+"'%";
>>>>>>> origin/master
                cmd = new MySqlCommand(sql, cn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    id += 1;
                    tabla.Rows.Add(reader["id"].ToString(), reader["nombre"].ToString(), reader["tipo_pago"].ToString(), reader["fech_venc"].ToString());
                }
                reader.Close();
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
