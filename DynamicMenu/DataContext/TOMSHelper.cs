using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace DynamicMenu.DataContext
{
    public class TOMSHelper : SQLHelper
    {
        public override string ConnectionString
        {
            get
            {
                string connString = ConfigurationManager.AppSettings["TOMS_DB"];
                return connString;
            }
        }
    }
}