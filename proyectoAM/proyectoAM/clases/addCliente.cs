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

    public void mostrarNombre(string id)
    {
        conecta();

        try
        {
            string sql = "SELECT nombre FROM cliente";
            cmd = new MySqlCommand(sql, cn);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                    
            }
        }
        catch (MySqlException ex)
        {
            MessageBox.Show("Error en: " + ex);
        }
    }
        public string mostrarrnc(string nombre)
        {
            conecta();

            try
            {
                string sql = "SELECT rnc FROM cliente WHERE nombre='" + nombre + "'";
                cmd = new MySqlCommand(sql, cn);
                reader = cmd.ExecuteReader();
                string rc = reader.GetString(nombre);
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
            tabla.Columns.Add("id_cliente");
            tabla.Columns.Add("id_item");
            tabla.Columns.Add("tipo_pago");
            tabla.Columns.Add("fech_venc");

            return tabla;
        }

        public DataTable muestra()
        {
        try
        {
            addColumns();
            conecta();
            //addColumns();
            string sql = "SELECT * FROM factura";
            cmd = new MySqlCommand(sql, cn);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                tabla.Rows.Add(reader["id"].ToString(),  reader["id_cliente"].ToString(), reader["id_item"].ToString(), reader["tipo_pago"].ToString(), reader["fech_venc"].ToString());
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
