using System.Diagnostics;
using System.Threading.Tasks;
using MyOnlineShop.Data;
using MyOnlineShop.Models;
using Microsoft.AspNetCore.Mvc;


namespace MyOnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductCategoryController : Controller
    {
        private readonly ApplicationDbContext _db;


        public ProductCategoryController(ApplicationDbContext db) => _db = db;

        public IActionResult Index()
        {
            return View(_db.ProductCategories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCategory productCategory)
        {
            if(!ModelState.IsValid)
                return View();


            _db.Add(productCategory);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Show(int? id)
        {
            var productCategory = _db.ProductCategories.Find(id);
            if (productCategory == null)
            {
                return NotFound();
            }

            return View(productCategory);
        }

        [HttpGet]
        public IActionResult Update(int? id)
        {
            var productCategory = _db.ProductCategories.Find(id);
            if (productCategory == null)
            {
                return NotFound();
            }

            return View(productCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ProductCategory productCategory)
        {
            if (!ModelState.IsValid)
                return View();


            _db.Update(productCategory);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            var productCategory = _db.ProductCategories.Find(id);
            if (productCategory == null)
            {
                return NotFound();
            }

            _db.ProductCategories.Remove(productCategory);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
