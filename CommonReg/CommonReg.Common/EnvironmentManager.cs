using CommonReg.Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonReg.Common
{

    public static class EnvironmentManager
    {
        static EnvironmentManager()
        {
            DbHost = Environment.GetEnvironmentVariable(OptionsKeys.DB_HOST);
            DbPort = Environment.GetEnvironmentVariable(OptionsKeys.DB_PORT);
            DbUser = Environment.GetEnvironmentVariable(OptionsKeys.DB_USER);
            DbPassword = Environment.GetEnvironmentVariable(OptionsKeys.DB_PASSWORD);
            DbName = Environment.GetEnvironmentVariable(OptionsKeys.DB_DATABASE);
            FrontServiceUrl = Environment.GetEnvironmentVariable(OptionsKeys.FRONT_SERVICE_URL);
            AwsAccessKeyId = Environment.GetEnvironmentVariable(OptionsKeys.AWS_ACCESS_KEY_ID);
            AwsSecretAccessKey = Environment.GetEnvironmentVariable(OptionsKeys.AWS_SECRET_ACCESS_KEY);

        }

        public static string DbHost { get; }
        public static string DbPort { get; }
        public static string DbUser { get; }
        public static string DbPassword { get; }
        public static string DbName { get; }
        public static string FrontServiceUrl { get; }
        public static string AwsAccessKeyId { get; }
        public static string AwsSecretAccessKey { get; }

    }

}
