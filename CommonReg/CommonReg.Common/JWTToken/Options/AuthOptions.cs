using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonReg.Common.JWTToken.Options
{
    public static class AuthOptions
    {
        public const string ISSUER = "RPJ";
        public const string AUDIENCE = "RPJ";
        public const string JWT_QUERY_PARAMETER_NAME = "token";
        public const string KEY = "ZVotrwx6WCXyYT0xGTxPojMRKjoKmsdClLdYL2qT17uOYmba9vRdf4k6ZpiftpQ5";
        public const int SESSION_LIFE_TIME_DAYS = 5;
        public const int TOKEN_LIFE_TIME_MINUTES = 120;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Convert.FromBase64String(KEY));
        }
    }
}
