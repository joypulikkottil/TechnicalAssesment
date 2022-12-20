using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Net.Http.Headers;

namespace WebService.Filters
{

    public class BasicAuthFilter : IAuthorizationFilter
    {
        private readonly string _realm;
        private readonly IConfiguration _configuration;
        public BasicAuthFilter(string realm, IConfiguration configuration)
        {
            _realm = realm;
            _configuration = configuration;

        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                string authHeader = context.HttpContext.Request.Headers["Authorization"];
                if (authHeader != null)
                {
                    var hValue = AuthenticationHeaderValue.Parse(authHeader);
                    if (hValue.Scheme.Equals(AuthenticationSchemes.Basic.ToString(), StringComparison.OrdinalIgnoreCase))
                    {
                        if (_configuration["basicAuth"] == hValue.Parameter)
                        {
                            return;
                        }
                    }
                }
                context.Result = new UnauthorizedResult();
            }
            catch (Exception ex)
            {
                context.Result = new UnauthorizedResult();
            }

        }
    }
}
