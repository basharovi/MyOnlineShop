using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

            return View(product);
        }

        [HttpPost]
        public IActionResult Show(int id)
        {
            var product = _db.Products
                .Include(p => p.Category)
                .Include(p => p.Tag)
                .FirstOrDefault(p => p.Id == id);

            if (product == null)  
                return NotFound();


            var products = HttpContext?.Session.GetString("SessionProducts") == null ? new List<Product>() 
                : JsonConvert.DeserializeObject<List<Product>>(HttpContext.Session.GetString("SessionProducts"));

            products.Add(product);

            HttpContext?.Session.SetString("SessionProducts", JsonConvert.SerializeObject(products, Formatting.Indented,
                new JsonSerializerSettings() {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }));


            return View(product);
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
