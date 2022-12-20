using System.Security.Claims;

namespace WebApp.HelperService
{
    public interface ILoginClaims
    {
        string UserName { get; }
        string FullName { get; }
        string OrganizationCode { get; }
    }
    public class LoginClaims : ILoginClaims
    {
        private readonly IHttpContextAccessor _context;
        public LoginClaims(IHttpContextAccessor context)
        {
            _context = context;
        }
        public string UserName
        {
            get
            {

                return _context?
                              .HttpContext?
                              .User?
                              .Claims?.ToList()?
                              .Where(f => f.Type == ClaimTypes.Name.ToString())?
                              .First()?
                              .Value ?? "";


            }
        }

        public string FullName
        {
            get
            {

                return _context?
                              .HttpContext?
                              .User?
                              .Claims?.ToList()?
                              .Where(f => f.Type == ClaimTypes.GivenName.ToString())?
                              .First()?
                              .Value ?? "";


            }
        }

        public string OrganizationCode
        {
            get
            {

                return _context?
                              .HttpContext?
                              .User?
                              .Claims?.ToList()?
                              .Where(f => f.Type == "OrganizationCode")?
                              .First()?
                              .Value ?? "";


            }
        }
    }
}
