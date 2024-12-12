using CommonReg.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonReg.DAL
{
    internal class ConnectionStringResolver : IConnectionStringResolver
    {
        public string Resolve => ConnectionStringHelper.GenerateConnectionString();

    }
}
