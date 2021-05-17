using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantLibrary
{
    public static class GlobalConfig
    {
        public static string ConnString(string name)
        {
            // This uses the name in the app.config to return the connectionstring.
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
