using DbFirst.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using System;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;
using WebApp.Models;
using System.Diagnostics;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            LoginModel model = new();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User? user = new User();
                if (_configuration != null)
                {
                    using HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(AuthenticationSchemes.Basic.ToString(), _configuration["basicAuth"]);

                    client.BaseAddress = new Uri(_configuration["ServiceBaseUri"]?.ToString() ?? "");
                    var response = await client.PostAsync(requestUri: "LoginService/Login", content: new StringContent(
                        content: $"Username={model.UserName}&Password={model.Password}",
                        encoding: Encoding.UTF8,
                        mediaType: "application/x-www-form-urlencoded"
                    ));
                    if (response.IsSuccessStatusCode)
                    {
                        if (response.Content != null)
                        {
                            user = await response.Content.ReadFromJsonAsync<User>();
                            if (user != null && user.Status == true)
                            {
                                var loginClaims = new List<Claim>
                                                    {
                                                        new Claim(ClaimTypes.Name, user.Name),
                                                        new Claim("OrganizationCode", user.Organization.Code),
                                                        new Claim(ClaimTypes.GivenName, user.Username)
                                                    };
                                ClaimsIdentity identity = new(authenticationType: CookieAuthenticationDefaults.AuthenticationScheme, claims: loginClaims);
                                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                                return RedirectToAction("Index", "Order");
                            }
                            else
                            {
                                ModelState.AddModelError("UserName", "Invalid Credentials");
                            }
                        }
                        else
                        { 
                            ModelState.AddModelError("UserName", "Unable to login!");
                        }
                    }
                    else
                    {
                        //hanle response failure
                        ModelState.AddModelError("UserName", "Unable to login!");
                    }
                }
               
            }
            return View(model);
            //return Redirect("/Order/Index");
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Home");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}