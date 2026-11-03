using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
namespace FBRInvoice
{   
   public  class clsConnection
    {
        public static void Con()
        {
            
            ///string connection = @"Data Source= .;Initial Catalog=NuReady;User ID=erp;password=YOUR_PASSWORD;MultipleActiveResultSets=true;WSID=Admin| " + "";
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
        }
        public static SqlConnection con;
    }
}
 