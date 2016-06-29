using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;


namespace proyectoAM.clases
{
    class comprobante
    {
        
        MySqlDataReader reader;
        MySqlConnection cn;
        MySqlCommand cmd;

        public void conecta() { cn = conDB.conecta(); cn.Open(); }

        public bool agregarCompro(string[] data)
        {
            conecta();

            string tipo = data[0].ToString();
            string valor = data[1].ToString();
            string inicio = data[2].ToString();
            string actual = data[2].ToString();
            string fin = data[4].ToString();


            try
            {
                //id_cliente,

                string sql = "INSERT INTO `comprobante`(`tipo`, `valor`, `inicio`, actual, `fin`) VALUES ('" + tipo + "','" + valor+ "','" + inicio + "','" + actual + "','" + fin + "')";
                cmd = new MySqlCommand(sql, cn);
                cmd.ExecuteNonQuery();
                //id = Convert.ToInt32(cmd.ExecuteScalar());
                cn.Close();
                return true;

            }
            catch (MySqlException ex)
            {
               // MessageBox.Show(ex.ToString());
            }
            return false;
        }

        public bool agregarActu(string[] data)
        {
            conecta();

            string tipo = data[0].ToString();
            string valor = data[1].ToString();
            string inicio = data[2].ToString();
            string actual = data[3].ToString();
            string fin = data[4].ToString();


            try
            {
                //id_cliente,

                string sql = "INSERT INTO `comprobante`(`tipo`, `valor`, `inicio`, actual, `fin`) VALUES ('" + tipo + "','" + valor + "','" + inicio + "','" + actual + "','" + fin + "')";
                cmd = new MySqlCommand(sql, cn);
                cmd.ExecuteNonQuery();
                //id = Convert.ToInt32(cmd.ExecuteScalar());
                
                cn.Close();
                return true;

            }
            catch (MySqlException ex)
            {
              //  MessageBox.Show(ex.ToString());
            }
            return false;
        }

        public string traeractual(string date)
        {
            conecta();
            string tipo = date;
            try
            {
                //id_cliente,

                string sql = "SELECT max(actual)as actual FROM comprobante WHERE tipo LIKE '%"+tipo+"%'";
                cmd = new MySqlCommand(sql, cn);
                reader = cmd.ExecuteReader();
                reader.Read();
                //string data = reader["valor"].ToString();
                string data2 =  reader["actual"].ToString();
                reader.Close();
                //id = Convert.ToInt32(cmd.ExecuteScalar());
                cn.Close();
                
                return data2;

            }
            catch (MySqlException ex)
            {
               // MessageBox.Show(ex.ToString());
            }
            return null;
        }

        public string traertipo(string data)
        {
            conecta();


            try
            {
                //id_cliente,

                string sql = "SELECT tipo FROM comprobante WHERE tipo like '"+data+"'";
                cmd = new MySqlCommand(sql, cn);
                reader = cmd.ExecuteReader();
                reader.Read();
                //string data = reader["valor"].ToString();
                string data2 = reader["tipo"].ToString();
                reader.Close();
                //id = Convert.ToInt32(cmd.ExecuteScalar());
                cn.Close();

                return data2;

            }
            catch (MySqlException ex)
            {
              //  MessageBox.Show(ex.ToString());
            }
            return null;
        }

        public int traerid()
        {
            conecta();

            try
            {
                //id_cliente,

                string sql = "Select id from comprobante ORDER BY valor ASC; ";
                cmd = new MySqlCommand(sql, cn);
                reader = cmd.ExecuteReader();
                reader.Read();
                //string data = reader["valor"].ToString();
                int data2 = int.Parse(reader["id"].ToString());
                reader.Close();
                //id = Convert.ToInt32(cmd.ExecuteScalar());
                cn.Close();

                return data2;

            }
            catch (MySqlException ex)
            {
               // MessageBox.Show(ex.ToString());
            }
            return 0;
        }

        public int traermax(string data)
        {
            conecta();

            try
            {
                //id_cliente,

                string sql = "Select distinct fin from comprobante WHERE  tipo LIKE '%"+data+"%' ; ";
                cmd = new MySqlCommand(sql, cn);
                reader = cmd.ExecuteReader();
                reader.Read();
                //string data = reader["valor"].ToString();
                int data2 = int.Parse(reader["fin"].ToString());
                reader.Close();
                //id = Convert.ToInt32(cmd.ExecuteScalar());
                cn.Close();

                return data2;

            }
            catch (MySqlException ex)
            {
               // MessageBox.Show(ex.ToString());
            }
            return 0;
        }


        public string traervalor(string date)
        {
            conecta();

            try
            {
                //id_cliente,
                string valor = date;
                string sql = "SELECT valor FROM  `comprobante` WHERE tipo LIKE '%"+valor+"%';";
                cmd = new MySqlCommand(sql, cn);
                reader = cmd.ExecuteReader();
                reader.Read();
                string data = reader["valor"].ToString();
                //string data2 = reader["actual"].ToString();
                reader.Close();
                //id = Convert.ToInt32(cmd.ExecuteScalar());
                cn.Close();

                return data;

            }
            catch (MySqlException ex)
            {
                //MessageBox.Show(ex.ToString());
            }
            return null;
        }

        public string traerinicio(string tipo)
        {
            conecta();
            

            try
            {
                //id_cliente,
                string data = tipo;
                string sql = "SELECT inicio FROM  `comprobante` where tipo like '%"+data+"%' ;";
                cmd = new MySqlCommand(sql, cn);
                reader = cmd.ExecuteReader();
                reader.Read();
                string inicio = reader["inicio"].ToString();
                //string data2 = reader["actual"].ToString();
                reader.Close();
                //id = Convert.ToInt32(cmd.ExecuteScalar());
                cn.Close();

                return inicio;

            }
            catch (MySqlException ex)
            {
               // MessageBox.Show(ex.ToString());
            }
            return null;
        }

        public bool agregarcomprobantetxt(string[] data)
        {
            conecta();

            string actual = data[0].ToString();
            try
            {
                //id_cliente,

                string sql = "INSERT INTO comprobante (actual) VALUES('" + actual + "'); SELECT max(actual) FROM comprobante";
                cmd = new MySqlCommand(sql, cn);
                cmd.ExecuteNonQuery();
                //id = Convert.ToInt32(cmd.ExecuteScalar());
                cn.Close();
                return true;

            }
            catch (MySqlException ex)
            {
              //  MessageBox.Show(ex.ToString());
            }
            return false;
        }

        //
    }
}
