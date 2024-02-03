using Common.Constants;
using Common.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Common.Web.Helpers
{
    public static class JwtTokenHelpers
    {
        public static Guid GetUserIdFromToken(HttpRequest request)
        {
            string initialToken = request.Headers.Authorization.FirstOrDefault(h => h.StartsWith("Bearer"))
                ?? throw new AuthException("token cannot be empty");
            string token = initialToken.Replace("Bearer", "").Trim();
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtSecurityToken = handler.ReadJwtToken(token);
            Claim userIdClaim = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == JwtClaimsConstants.CLaimUserId)
                ?? throw new AuthException("no user claim in token");

            return Guid.Parse(userIdClaim.Value);
        }
    }
}
