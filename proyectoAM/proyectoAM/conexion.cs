using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.IO;

namespace Santana_Version_Final.factura
{
    class conexion
    {
        public SQLiteConnection getConexion()
        {
            string path = Directory.GetCurrentDirectory();

            SQLiteConnection conectar = new SQLiteConnection("Data Source= " + path + "/7amFactura.sqlite");
            //System.Windows.Forms.MessageBox.Show(path);
            //SQLiteConnection conectar = new SQLiteConnection("Data Source= C:/santana/santana.sqlite");
            return conectar;
        }

    }
}
