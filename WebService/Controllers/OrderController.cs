using DbFirst;
using DbFirst.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebService.Filters;
using static NuGet.Packaging.PackagingConstants;

namespace WebService.Controllers
{

    [ApiController]
    [Route(template: "OrderService")]
    public class OrderController : ControllerBase
    {
        private readonly AppDbContext _db;
        public OrderController(AppDbContext db)
        {
            _db = db;
        }

        [BasicAuthAttirbute]
        [HttpPost]
        [Route(template: "GetOrders")]
        public async Task<List<Order>> GetOrders([FromBody] string organizationCode)
        {
            List<Order> orders = await _db.Orders.Where(f => f.Organization.Code.Equals(organizationCode))
                                       .Include(f => f.Currency)
                                       .Include(f => f.Vendor)
                                       .Include(f => f.Organization)
                                       .ToListAsync();
            return orders;
        }
        [BasicAuthAttirbute]
        [HttpPost]
        [Route(template: "GetOrderDetails")]
        public async Task<List<Order>> GetOrderDetails([FromForm] string Guid, [FromForm] string OrganizationCode)
        {
            List<Order> orders = await _db.Orders
                                       .Where(f => f.Guid != null && f.Guid.ToString().Trim().Equals(Guid) &&
                                                             f.Organization.Code.Equals(OrganizationCode))
                                       .Include(f => f.OrderItems).ThenInclude(f => f.Product)
                                       .Include(f => f.Currency)
                                       .Include(f => f.Vendor)
                                       .Include(f => f.Organization)
                                       .ToListAsync();

            return orders;
        }
    }
}
