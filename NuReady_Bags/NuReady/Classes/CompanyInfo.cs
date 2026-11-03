using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace NuReady
{
    class CompanyInfo

    {
        private static string _CompanyName;
        public static string CompanyName
        {
            get { return CompanyInfo._CompanyName; }
            set { CompanyInfo._CompanyName = value; }
        }

        private static string _UrCompanyName;
        public static string UrCompanyName
        {
            get { return CompanyInfo._UrCompanyName; }
            set { CompanyInfo._UrCompanyName = value; }
        }

        private static string _Description;
        public static string Description
        {
            get { return CompanyInfo._Description; }
            set { CompanyInfo._Description = value; }
        }
        
        private static string _Address;
        public static string Address
        {
            get { return CompanyInfo._Address; }
            set { CompanyInfo._Address = value; }
        }
       
        private static string _Cell;
        public static string Cell
        {
            get { return CompanyInfo._Cell; }
            set { CompanyInfo._Cell = value; }
        }
      
        private static string _Cell2;
        public static string Cell2
        {
            get { return CompanyInfo._Cell2; }
            set { CompanyInfo._Cell2 = value; }
        }
        public static string _ntn;
        public static string ntn
        {
            get { return CompanyInfo._ntn; }
            set { CompanyInfo._ntn = value; }
        }
        public static string _strn;
        public static string strn
        {
            get { return CompanyInfo._strn; }
            set { CompanyInfo._strn = value; }
        }



    }
}
