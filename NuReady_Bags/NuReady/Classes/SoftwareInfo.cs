using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NuReady
{
    class SoftwareInfo
    {
        private static string _Terminal;

        internal  static string Terminal
        {
            get { return SoftwareInfo._Terminal; }
            set { SoftwareInfo._Terminal = value; }
        }
        //BAGSPK-954574
        // surmrawala 138331
        public static string POSID { get; set; } = "130754";
        //public static string POSID { get; set; } = "932054";
      //  public static string POSID { get; set; } = "1122";
    }
}
