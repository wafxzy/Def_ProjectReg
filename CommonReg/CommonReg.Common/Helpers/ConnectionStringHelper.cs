using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonReg.Common.Helpers
{

    public static class ConnectionStringHelper
    {
        public static string GenerateConnectionString()
        {
            string host = EnvironmentManager.DbHost;
            string port = EnvironmentManager.DbPort;
            string user = EnvironmentManager.DbUser;
            string password = EnvironmentManager.DbPassword;
            string database = EnvironmentManager.DbName;

            string result =
                $"Host={host};port={port};User ID={user};Password={password};Database={database};";

            return result;
        }
    }
}
