using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.IO;

namespace proyectoAM
{
    class conexion
    {
        public SQLiteConnection getConexion()
        {
            string path = Directory.GetCurrentDirectory();

            SQLiteConnection conectar = new SQLiteConnection("Data Source= " + path + "/7am_facturacion.sqlite");
            //System.Windows.Forms.MessageBox.Show(path);
            //SQLiteConnection conectar = new SQLiteConnection("Data Source= C:/santana/santana.sqlite");
            System.Windows.Forms.MessageBox.Show("Conectado");
            return conectar;
        }

    }
}
