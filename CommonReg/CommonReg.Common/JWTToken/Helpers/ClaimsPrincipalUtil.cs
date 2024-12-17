using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace CommonReg.Common.JWTToken.Helpers
{
    public static class ClaimsPrincipalUtil
    {
        public static Guid GetUserId(this ClaimsPrincipal userPrincipal)
        {
            Claim userIdClaim = userPrincipal.FindFirst(x => x.Type == ClaimTypes.NameIdentifier);

            if (userIdClaim == null
                || string.IsNullOrWhiteSpace(userIdClaim.Value)
                || !Guid.TryParse(userIdClaim.Value, out Guid userId)
                || userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(ClaimTypes.NameIdentifier));
            }

            return userId;
        }

        public static int GetSessionId(this ClaimsPrincipal userPrincipal)
        {
            Claim claim = userPrincipal.FindFirst(x => JwtRegisteredClaimNames.Sid.Equals(x.Type, StringComparison.OrdinalIgnoreCase));

            if (claim == null
                || string.IsNullOrWhiteSpace(claim.Value)
                || !int.TryParse(claim.Value, out int sessionId)
                || sessionId == 0)
            {
                throw new ArgumentNullException(nameof(ClaimTypes.Sid));
            }

            return sessionId;
        }
    }
}
