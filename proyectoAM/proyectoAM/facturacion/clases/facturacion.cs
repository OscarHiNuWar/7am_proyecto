using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Santana_Version_Final.factura.clases
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

    public int agregar(string[] data)
    {
        conectame();
        try
        {



            string sql = "INSERT INTO [nombre](datos) VALUES('" + data[0] + "','" + data[1] + "','" + data[2] + "');SELECT last_insert_rowid()";
            cmd = new SQLiteCommand(sql, cn);

            id = Convert.ToInt32(cmd.ExecuteScalar());
            cn.Close();
            return id;

        }
        catch (SQLiteException ex)
        {
            MessageBox.Show(ex.ToString());
        }
        return 0;



        }
    }

}
