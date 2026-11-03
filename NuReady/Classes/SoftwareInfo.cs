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

        //Smart Home Appliances BMCHS postId test
        public static string POSID { get; set; } = "819562";
    }
}
