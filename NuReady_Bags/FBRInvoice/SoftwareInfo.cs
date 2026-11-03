using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FBRInvoice
{
    class SoftwareInfo
    {
        private static string _Terminal;

        internal  static string Terminal
        {
            get { return SoftwareInfo._Terminal; }
            set { SoftwareInfo._Terminal = value; }
        }
        //public static string POSID { get; set; } = "954564";
        public static string POSID { get; set; } = "130754";
    }
}
