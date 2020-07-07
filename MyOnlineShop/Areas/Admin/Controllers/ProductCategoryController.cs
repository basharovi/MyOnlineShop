using MyOnlineShop.Data;
using System.Diagnostics;
using System.Linq;
using MyOnlineShop.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace MyOnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductCategoryController : Controller
    {
        private readonly ApplicationDbContext _db;


        public ProductCategoryController(ApplicationDbContext db) => _db = db;

        public IActionResult Index()
        {
            var category = _db.ProductCategories.ToList();

            foreach (var c in category)
            {
                _db.Entry(c).Collection(c=>c.Products)
                    .Query()
                    .Where(m=>m.IsAvailable)
                    .Load();

                var products = c.Products;
            }

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
            //TempData["isCreated"] = await _db.SaveChangesAsync() > 0;
            await _db.SaveChangesAsync();

            TempData["crudMessage"] = "Product Category has been Created Successfully!";

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

            TempData["crudMessage"] = "Product Category has been Updated Successfully!";

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

            TempData["crudMessage"] = "Product Category has been Deleted Successfully!";

            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
