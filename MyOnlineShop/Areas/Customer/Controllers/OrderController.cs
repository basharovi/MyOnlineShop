using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyOnlineShop.Data;
using MyOnlineShop.Models;
using Newtonsoft.Json;

namespace MyOnlineShop.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _db;

        public OrderController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(Order anOrder)
        {
            var sessionProducts = HttpContext?.Session.GetString("SessionProducts") == null ? new List<Product>()
                : JsonConvert.DeserializeObject<List<Product>>(HttpContext.Session.GetString("SessionProducts"));

            foreach (var product in sessionProducts)
            {
                anOrder.OrderDetails.Add(new OrderDetails
                {
                    ProductId = product.Id
                });
            }

            await _db.Orders.AddAsync(anOrder);
            var isDone = await _db.SaveChangesAsync();

            // Made Session Null
            HttpContext?.Session.SetString("SessionProducts", JsonConvert.SerializeObject(new List<Product>(), Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }));

            return View();
        }
    }
}
