using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MySql;
using System.Windows.Forms;

namespace proyectoAM
{

   public class conDB
    {
       public static MySqlConnection conecta()
    {
            try
            {
                string server = "localhost";
                string db = "7am_proyecto";
                //string uid = "root";
                string pass = "llego_el_pavo01";

                MySqlConnection conectar = new MySqlConnection("Server=" + server + "; database=" + db + "; User=root ; Password=" + pass + ";");

                conectar.Open();
               // return conectar;
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show("Error en: "+ex.Message);

            }
            return null;
        }
}

     
   
}
