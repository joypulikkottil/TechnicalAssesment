using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;
        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }        
        //public IActionResult Index()
        //{
        //    return View();
        //}
        //public async Task<IActionResult> Login()
        //{

        //    return;
        }
}
