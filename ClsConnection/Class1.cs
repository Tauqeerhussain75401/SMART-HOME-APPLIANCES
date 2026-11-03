using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ClsConnection
{
    public class Class1
    {

        public static void Con()
        {

            //string connection = @"Data Source=192.168.2.11;Initial Catalog=NuReady_Surmawala_upd;User ID=erp_nuready;password=YOUR_PASSWORD;MultipleActiveResultSets=true;WSID=Admin | " + "";
              
            string connection = @"Data Source=DESKTOP-8K5L56K;Initial Catalog=NuReady_SMARTHOMEAPPLIANCES;Integrated Security=True;";
            if (con == null)
                con = new SqlConnection(connection);
            else
            {
                con.Close();
                SqlConnection.ClearPool(con);
                con.ConnectionString = connection;
            }
            con.StatisticsEnabled = true;
            con.Open();
            //con.ConnectionString = connection;
            //   return con;
        }
        public static SqlConnection con;
    }
}
