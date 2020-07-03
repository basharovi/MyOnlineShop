using MyOnlineShop.Data;
using System.Diagnostics;
using System.IO;
using MyOnlineShop.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;


namespace MyOnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHostEnvironment _host;

        public ProductController(ApplicationDbContext db, IHostEnvironment host)
        {
            _db = db;
            _host = host;
        }

        public IActionResult Index()
        {
            //var products = _db.Products.
            return View(_db.Products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["ProductCategories"] = new SelectList(_db.ProductCategories, "Id", "Category");
            ViewData["SpecialTags"] = new SelectList(_db.SpecialTags, "Id", "Tag");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if(!ModelState.IsValid)
                return View();

            if (product.ImageFile != null)
            {
                var imagePath = Path.Combine($"{_host.ContentRootPath}/images", product.ImageFile.FileName);
                await product.ImageFile.CopyToAsync(new MemoryStream());
            }


            _db.Add(product);
            await _db.SaveChangesAsync();

            TempData["crudMessage"] = "Product has been Created Successfully!";

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
