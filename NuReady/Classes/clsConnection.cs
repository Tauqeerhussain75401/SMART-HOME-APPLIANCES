using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace NuReady
{   
    class clsConnection
    {

        public static string ConnectionString = @"Data Source=localhost;Initial Catalog=NuReady_SMARTHOMEAPPLIANCES;Integrated Security=True;";

        //Clint
        //public static string ConnectionString = @"Data Source=192.168.2.11;Initial Catalog=NuReady_Surmawala_upd;User ID=erp_nuready;password=YOUR_PASSWORD;MultipleActiveResultSets=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }


    }
}
 