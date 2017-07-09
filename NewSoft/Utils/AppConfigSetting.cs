using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Utils
{
    public class AppConfigSetting
    {
        public static string GetDBConnection
        {
            get
            {
                //return System.Configuration.ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
                return System.Configuration.ConfigurationManager.AppSettings["connStr"];
            }
        }
    }
}
