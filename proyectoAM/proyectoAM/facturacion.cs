using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace proyectoAM
{
    class facturacion
    {
        conexion con = new conexion();
        DataTable tabla;
        SQLiteDataReader reader;
        SQLiteConnection cn;
        SQLiteCommand cmd;
        int id = 0;
    
    public void conectame() { cn = con.getConexion(); cn.Open(); }

    public bool agregaruser(string[] data)
    {
        conectame();
        try
        {
            string sql = "INSERT INTO cliente(compania,nfc,rnc,email,telefono) VALUES('" + data[0] + "','" + data[1] + "','" + data[2] + "','" + data[3] + "','" + data[4] + "');SELECT last_insert_rowid()";
            cmd = new SQLiteCommand(sql, cn);

            id = Convert.ToInt32(cmd.ExecuteScalar());
            cn.Close();
            return true;

        }
        catch (SQLiteException ex)
        {
            MessageBox.Show(ex.ToString());
        }
        return false;

        }

        public bool agregardescripcion(string[] data)
        {
            conectame();
            try
            {
                string sql = "INSERT INTO [nombre](datos) VALUES('" + data[0] + "','" + data[1] + "','" + data[2] + "');SELECT last_insert_rowid()";
                cmd = new SQLiteCommand(sql, cn);

                id = Convert.ToInt32(cmd.ExecuteScalar());
                cn.Close();
                return true;

            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return false;

        }

        public bool agregardetalle(string[] data)
        {
            conectame();
            try
            {
                string sql = "INSERT INTO [nombre](datos) VALUES('" + data[0] + "','" + data[1] + "','" + data[2] + "');SELECT last_insert_rowid()";
                cmd = new SQLiteCommand(sql, cn);

                id = Convert.ToInt32(cmd.ExecuteScalar());
                cn.Close();
                return true;

            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return false;

        }

        public bool agregarfacturacion(string[] data)
        {
            conectame();
            try
            {
                string sql = "INSERT INTO [nombre](datos) VALUES('" + data[0] + "','" + data[1] + "','" + data[2] + "');SELECT last_insert_rowid()";
                cmd = new SQLiteCommand(sql, cn);

                id = Convert.ToInt32(cmd.ExecuteScalar());
                cn.Close();
                return true;

            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return false;

        }

        public bool agregarfactura(string[] data)
        {
            conectame();
            try
            {
                string sql = "INSERT INTO [nombre](datos) VALUES('" + data[0] + "','" + data[1] + "','" + data[2] + "');SELECT last_insert_rowid()";
                cmd = new SQLiteCommand(sql, cn);

                id = Convert.ToInt32(cmd.ExecuteScalar());
                cn.Close();
                return true;

            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return false;

        }
    }

}
