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
<<<<<<< HEAD
                 string server = "localhost";
                 string db = "7am_proyecto";
                 string uid = "root";
                 string pass = "";

                // MySqlConnection conectar = new MySqlConnection("Server=74.208.205.141; Port=3306; User=testoscar; Password=testoscar; database=testoscar;");
                MySqlConnection conectar = new MySqlConnection("Server="+ server + "; Port=3306; User="+ uid + "; Password="+ pass + "; database="+ db + ";");

                //conectar.Open();
                //MessageBox.Show("Conectado");
                return conectar;

=======
               /* string server = "localhost";
                string db = "7am_proyecto";
                string uid = "root";
                string pass = "llego_el_pavo01";*/

                MySqlConnection conectar = new MySqlConnection("Server=localhost; User=root; Password=llego_el_pavo01; database=7am_proyecto;");

                
                conectar.Open();
               // return conectar;
>>>>>>> origin/master
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show("Error en: "+ex.Message);

            }
            return null;
        }
}

     
   
}
