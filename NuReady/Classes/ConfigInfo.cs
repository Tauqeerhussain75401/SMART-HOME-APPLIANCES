using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NuReady
{
    class ConfigInfo
    {
        private static string _ThermalPrinterName;

        public static string ThermalPrinterName
        {
            get { return ConfigInfo._ThermalPrinterName; }
            set { ConfigInfo._ThermalPrinterName = value; }
        }
    }
}
