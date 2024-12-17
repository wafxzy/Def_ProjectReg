using CommonReg.Common.JWTToken.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CommonReg.Common.JWTToken.Helpers
{
    public static class UserPrincipalHelper
    {
        public static ClaimsPrincipal InternalGetPrincipal(string token)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new();

                if (!(tokenHandler.ReadToken(token) is JwtSecurityToken)) return null;

                Byte[] symmetricKey = Convert.FromBase64String(AuthOptions.KEY);

                TokenValidationParameters validationParameters = new()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,

                    RequireExpirationTime = true,
                    ValidateLifetime = false,

                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey),
                    ValidateIssuerSigningKey = true,
                    ClockSkew = new TimeSpan(0)
                };

                ClaimsPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken _);

                return principal;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
