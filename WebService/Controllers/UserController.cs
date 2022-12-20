using DbFirst;
using DbFirst.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebService.Filters;

namespace WebService.Controllers
{
    [ApiController]
    [Route(template: "LoginService")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _db;
        public UserController(AppDbContext db)
        {
            _db = db;
        }
        [BasicAuthAttirbute]
        [Route(template: "Login")]
        public async Task<User> Login([FromForm] string Username, [FromForm] string Password)
        {
            var user = await _db.Users.Where(f => f.Username.Equals(Username) && f.Password.Equals(Password))
                                       .Include(f => f.Organization)
                                       .FirstOrDefaultAsync();
            if (user != null)
            {
                return user;
            }
            else
            {
                return new User();
            }
        }
    }
}
