using DbFirst.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using System;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Text;
using WebApp.HelperService;

namespace WebApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ILoginClaims _loginClaims;
        public OrderController(IConfiguration configuration , ILoginClaims loginClaims)
        {
            _configuration = configuration;
            _loginClaims = loginClaims;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            string organizationCode = _loginClaims.OrganizationCode;
            List<Order>? Orders = new List<Order>();
            if (_configuration != null)
            {
                using HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(AuthenticationSchemes.Basic.ToString(), _configuration["basicAuth"]);

                client.BaseAddress = new Uri(_configuration["ServiceBaseUri"]?.ToString() ?? "");
                var response = await client.PostAsJsonAsync("OrderService/GetOrders", value: organizationCode);
                if (response.IsSuccessStatusCode)
                {
                    if (response.Content != null)
                    {
                        Orders = await response.Content.ReadFromJsonAsync<List<Order>>();
                    }
                }
                else
                {
                    //hanle response failure
                    return NotFound();
                }
            }
            return View(Orders);
        }
        [Authorize]
        public async Task<IActionResult> Details(string id)
        {
            string organizationCode = _loginClaims.OrganizationCode;
            List<Order>? Orders = new List<Order>();
            if (_configuration != null)
            {
                using HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(AuthenticationSchemes.Basic.ToString(), _configuration["basicAuth"]);

                client.BaseAddress = new Uri(_configuration["ServiceBaseUri"]?.ToString() ?? "");
                var response = await client.PostAsync(requestUri: "OrderService/GetOrderDetails", content: new StringContent(
                        content: $"Guid={id}&OrganizationCode={organizationCode}",
                        encoding: Encoding.UTF8,
                        mediaType: "application/x-www-form-urlencoded"
                    ));
                if (response.IsSuccessStatusCode)
                {
                    if (response.Content != null)
                    {
                        Orders = await response.Content.ReadFromJsonAsync<List<Order>>();
                    }
                }
                else
                {
                    //hanle response failure
                    return NotFound();
                }
            }
            return View(Orders);
        }
    }
}
