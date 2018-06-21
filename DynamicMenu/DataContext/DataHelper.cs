using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DynamicMenu.DataContext
{
    public abstract class DataHelper
    {
        string ConnectionString { get; }
        public abstract DataTable QueryTable(string sqlScript);
        public abstract void ExecNonQuery(string sqlScript);
    }
}