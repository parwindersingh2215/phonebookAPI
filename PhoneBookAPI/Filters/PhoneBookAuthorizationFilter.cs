using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using PhoneBookAPI.Common;

namespace PhoneBookAPI.Filters
{

    public class PhoneBookAuthorizationFilter : IAuthorizationFilter
    {

        private readonly Claim claim;
        private readonly ILogger _logger;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _env;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="claim"></param>
        /// <param name="logger"></param>
        /// <param name="env"></param>
        public PhoneBookAuthorizationFilter(Claim claim,
                                            ILogger<PhoneBookAuthorizationFilter> logger,
                                            Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            this.claim = claim;
            this._logger = logger;
            this._env = env;
        }

        /// <summary>
        /// on authorized
        /// </summary>
        /// <param name="context"></param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            Claim userNameClaim = null;
            Claim environmentClaim = null;
            Claim userIdClaim = null;
            Claim userClientIdClaim = null;
            string token = string.Empty;
            DateTime? validFrom = null;
            DateTime? validTo = null;

            try
            {
                token = context.HttpContext.Request.GetToken();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "PhoneBookAuthorizationFilter.OnAuthorization Error in GetToken().");
            }

            if (string.IsNullOrWhiteSpace(token) || token == "Undefined")
            {
                context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Forbidden);
                _logger.LogInformation("PhoneBookAuthorizationFilter.OnAuthorization token is undefined or null.");
                return;
            }

            try
            {
                var jwtSecurityToken = (new JwtSecurityTokenHandler()).ReadToken(token) as JwtSecurityToken;
                validFrom = jwtSecurityToken.ValidFrom;
                validTo = jwtSecurityToken.ValidTo;

                _logger.LogInformation($"Token valid from: {validFrom} valid to: {validTo}");

                userNameClaim = jwtSecurityToken.Claims.FirstOrDefault(w => w.Type == "username");
                //TODO: for future Use
                userIdClaim = jwtSecurityToken.Claims.FirstOrDefault(w => w.Type == "userId");
                userClientIdClaim = jwtSecurityToken.Claims.FirstOrDefault(w => w.Type == "client_id");
                environmentClaim = jwtSecurityToken.Claims.FirstOrDefault(w => w.Type == "environment");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "PhoneBookAuthorizationFilter.OnAuthorization Error in reading token or reading token properties.");
            }


            if (userNameClaim == null || string.IsNullOrWhiteSpace(userNameClaim.Value))
            {
                context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Forbidden);
                _logger.LogInformation("PhoneBookAuthorizationFilter.OnAuthorization user name claim is null.");
                return;
            }

            if (validFrom == null || validTo == null)
            {
                context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Forbidden);
                _logger.LogInformation("PhoneBookAuthorizationFilter.OnAuthorization token is valid from or valid to is null.");
                return;
            }

            if (!(DateTime.UtcNow >= validFrom && DateTime.UtcNow <= validTo))
            {
                context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Forbidden);
                _logger.LogInformation("PhoneBookAuthorizationFilter.OnAuthorization current date/time is outside the valid date range of the token.");
                return;
            }


        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class PhoneBookAuthorizationAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="permissionName"></param>      
        public PhoneBookAuthorizationAttribute(string permissionName) : base(typeof(PhoneBookAuthorizationFilter))
        {
            Arguments = new object[] { new Claim("PermissionName", permissionName) };
        }
    }

}
