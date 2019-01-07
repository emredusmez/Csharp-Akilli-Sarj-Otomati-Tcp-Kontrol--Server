using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chargedoctor_server
{
   static class Ayarlar
    {
     public static  class SqlServer
        {
           static string sqlconnection = "Data Source = DESKTOP-NESJ0QP\\SQLEXPRESS; Initial Catalog = cihazlar; Persist Security Info=True;User ID = sa; Password=sqlserver123.";
            public static string CihazlarConnectionString { get { return sqlconnection; } set { sqlconnection = value; } }
        }
    }
}
