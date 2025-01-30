using CommonReg.Common.JWTToken.Constants;
using CommonReg.Common.JWTToken.Models;
using CommonReg.Common.JWTToken.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CommonReg.Common;
public static class TokenResultHelper
{
    private const string SECRET = AuthOptions.KEY;

    private static TokenResult GenerateToken(SecurityTokenDescriptor tokenDescriptor, DateTime expireDate)
    {
        JwtSecurityTokenHandler tokenHandler = new();

        SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
        string token = tokenHandler.WriteToken(securityToken);

        return new TokenResult
        {
            TokenModel = new TokenResponse
            {
                Token = token,
                ExpireDate = expireDate,
            }
        };
    }

    public static TokenResult InternalGenerateToken(
        int sessionId,
        Guid userId,
        Guid refreshToken,
        DateTime expireRefreshDate,
        string userName,
        List<string> userRoles,
        List<int> userPermissions
        )
    {
        HashSet<Claim> claims = new HashSet<Claim>();
        claims.Add(new Claim(JwtRegisteredClaimNames.Sid, sessionId.ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, refreshToken.ToString()));
        claims.Add(new Claim(ClaimTypes.NameIdentifier, userId.ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Aud, AuthOptions.AUDIENCE));


        if (!string.IsNullOrWhiteSpace(userName))
        {
            claims.Add(new Claim(ClaimTypes.Name, userName));
        }

        if (userRoles.Count > 0)
        {
            foreach (string userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }
        }

        if (userPermissions.Count > 0)
        {
            foreach (int userPermission in userPermissions)
            {
                claims.Add(new Claim(JWTClaims.Permission, userPermission.ToString(), ClaimValueTypes.Integer32));
            }
        }

        Byte[] symmetricKey = Convert.FromBase64String(SECRET);

        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Issuer = AuthOptions.ISSUER,
            Subject = new ClaimsIdentity(claims),
            Expires = expireRefreshDate,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
        };

        return GenerateToken(tokenDescriptor, expireRefreshDate);
    }

    public static TokenResult InvalidAccessTokenFormat => new()
    {
        Error = "Invalid access token format",
        ErrorDescription = "Invalid access token format"
    };

    public static TokenResult InvalidAccessTokenBody => new()
    {
        Error = "Invalid access token body",
        ErrorDescription = "Invalid access token body"
    };

    public static TokenResult ExpiredAccessToken => new()
    {
        Error = "Access token has expired or session not exists",
        ErrorDescription = "Access token has expired or session not exists"
    };

    public static TokenResult InvalidEmailOrPassword => new()
    {
        Error = "Email or password is incorrect.",
        ErrorDescription = "Email or password is incorrect."
    };

    public static TokenResult ActivationRequired => new()
    {
        Error = "Account requires activation. Please find an email with the details in your inbox.",
        ErrorDescription = "Account requires activation. Please find an email with the details in your inbox."
    };
   }
}
