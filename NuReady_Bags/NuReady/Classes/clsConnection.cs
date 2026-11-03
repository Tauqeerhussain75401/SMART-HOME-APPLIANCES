using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
namespace NuReady
{   
    class clsConnection
    {
        public static void Con()
        {

            //string connection = @"Data Source=localhost;Initial Catalog=NuReady_BagsPK;integrated security=true;MultipleActiveResultSets=true;WSID=Admin| " + "";
            //  string connection = @"Data Source= .;Initial Catalog=NuReady_BagsPK;User ID=erp_nuready;password=YOUR_PASSWORD;MultipleActiveResultSets=true;WSID=" + UserInfo.UserId + " | " + "";
            //string connection = @"Data Source= .;Initial Catalog=NuReady;User ID=erp;password=YOUR_PASSWORD;MultipleActiveResultSets=true;WSID=Admin| " + "";
            string connection = @"Data Source=192.168.2.11;Initial Catalog=NuReady_Surmawala_upd;User ID=erp_nuready;password=YOUR_PASSWORD;MultipleActiveResultSets=true;WSID=Admin | " + "";

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
 