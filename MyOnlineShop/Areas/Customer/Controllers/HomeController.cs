using System;
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
using X.PagedList;

namespace MyOnlineShop.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public async Task<IActionResult> Index(int? page)
        {
            return View(await _db.Products.ToPagedListAsync(page ?? 1, 4));
        }

        [HttpGet]
        public IActionResult Show(int? id)
        {
            if (id == null)
            {
                _logger.LogInformation("Id is Null!");

                return NotFound("Id is Null");
            }
            try
            {
                var product = _db.Products
                .Include(p => p.Category)
                .Include(p => p.Tag)
                .FirstOrDefault(p => p.Id == id);

                _logger.LogDebug(product.Name);

                return View(product);
            }
            catch (NullReferenceException)
            {
                _logger.LogInformation("There is no product with this Id : {0}", id);

                return NotFound($"There is no product with this Id : {id}");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured!");
            }

            return View();
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

            var sProduct = sessionProducts.Find(p => p.Id == id); // sProduct = session Product

            if (sProduct is null)
                sessionProducts.Add(product);
            else
                sessionProducts.Remove(sProduct);


            HttpContext?.Session.SetString("SessionProducts", JsonConvert.SerializeObject(sessionProducts, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }));


            return View(product);
        }

        public IActionResult Privacy() => View();

        [HttpGet]
        public IActionResult MyCart() => View();

        [HttpPost]
        public IActionResult MyCart(int id)
        {
            var sessionProducts = HttpContext?.Session.GetString("SessionProducts") == null ? new List<Product>()
                : JsonConvert.DeserializeObject<List<Product>>(HttpContext.Session.GetString("SessionProducts"));

            var sProduct = sessionProducts.Find(p => p.Id == id); // sProduct = session Product

            if (sProduct != null)
                sessionProducts.Remove(sProduct);

            HttpContext?.Session.SetString("SessionProducts", JsonConvert.SerializeObject(sessionProducts, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }));

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
