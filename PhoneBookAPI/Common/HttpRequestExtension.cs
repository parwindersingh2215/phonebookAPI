using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PhoneBookAPI.Common
{
    public static class HttpRequestExtension
    {
        /// <summary>
        /// Get the token received in the HTTP request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetToken(this HttpRequest request)
        {
            string token = string.Empty;
            if (request != null)
            {
                var authHeader = request.Headers.FirstOrDefault(y => y.Key == "Authorization");
                if (authHeader.Value.FirstOrDefault() != null)
                {
                    var strAuthHeader = authHeader.Value.FirstOrDefault();
                    token = strAuthHeader.Split(' ')[1];
                }
            }
            return token;
        }

        public static string GetUserName(string token)
        {
            Claim userNameClaim = null;
            DateTime? validFrom = null;
            DateTime? validTo = null;
            var jwtSecurityToken = (new JwtSecurityTokenHandler()).ReadToken(token) as JwtSecurityToken;
            validFrom = jwtSecurityToken.ValidFrom;
            validTo = jwtSecurityToken.ValidTo;
            userNameClaim = jwtSecurityToken.Claims.FirstOrDefault(w => w.Type == "username");
            return userNameClaim.Value;
        }
    }
}
