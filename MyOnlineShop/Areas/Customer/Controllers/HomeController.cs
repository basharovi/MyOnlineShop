using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyOnlineShop.Data;
using MyOnlineShop.Models;
using Newtonsoft.Json;

namespace MyOnlineShop.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<MyOnlineShop.Controllers.HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<MyOnlineShop.Controllers.HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            var products = _db.Products;

            return View(products);
        }

        [HttpGet]
        public IActionResult Show(int? id)
        {
            var product = _db.Products
                .Include(p => p.Category)
                .Include(p => p.Tag)
                .FirstOrDefault(p=> p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            //if (HttpContext?.Session.GetString("SessionProducts") == null)
            //{
            //    return View(product);
            //}

            //var sessionProducts = JsonConvert.DeserializeObject<List<Product>>(HttpContext.Session.GetString("SessionProducts"));

            //TempData["isAddedToCart"] = sessionProducts.Any(m => m.Id == id);

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Show(int id)
        {
            var product = await _db.Products
                .Include(p => p.Category)
                .Include(p => p.Tag)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)  
                return NotFound();


            var sessionProducts = HttpContext?.Session.GetString("SessionProducts") == null ? new List<Product>() 
                : JsonConvert.DeserializeObject<List<Product>>(HttpContext.Session.GetString("SessionProducts"));

            var sProduct = sessionProducts.Find(p=> p.Id == id); // sProduct = session Product

            if (sProduct is null)
                sessionProducts.Add(product);
            else
                sessionProducts.Remove(sProduct);


            HttpContext?.Session.SetString("SessionProducts", JsonConvert.SerializeObject(sessionProducts, Formatting.Indented,
                new JsonSerializerSettings() {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }));


            return View(product);
        }

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
