using MyOnlineShop.Data;
using System.Diagnostics;
using System.IO;
using MyOnlineShop.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;


namespace MyOnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly PostgresDbContext _db;
        private readonly IHostEnvironment _host;

        public ProductController(PostgresDbContext db, IHostEnvironment host)
        {
            _db = db;
            _host = host;
        }

        public IActionResult Index()
        {
            var products = _db.Products.
                Include(p => p.Category).
                Include(p=> p.Tag);
            
            return View(products);
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
                var imagePath = Path.Combine($"{_host.ContentRootPath}/wwwroot/images", product.ImageFile.FileName);
                await product.ImageFile.CopyToAsync(new FileStream(imagePath,FileMode.Create));
                
                product.Image = product.ImageFile.FileName;
            }

            _db.Add(product);
            await _db.SaveChangesAsync();

            TempData["crudMessage"] = "Product has been Created Successfully!";

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Show(int? id)
        {
            var product = _db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            ViewData["ProductCategories"] = new SelectList(_db.ProductCategories, "Id", "Category");
            ViewData["SpecialTags"] = new SelectList(_db.SpecialTags, "Id", "Tag");

            return View(product);
        }

        [HttpGet]
        public IActionResult Update(int? id)
        {
            var product = _db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }


            ViewData["ProductCategories"] = new SelectList(_db.ProductCategories, "Id", "Category");
            ViewData["SpecialTags"] = new SelectList(_db.SpecialTags, "Id", "Tag");

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Product product)
        {
            if (!ModelState.IsValid)
                return View();

            if (product.ImageFile != null)
            {
                var imagePath = Path.Combine($"{_host.ContentRootPath}/wwwroot/images", product.ImageFile.FileName);
                await product.ImageFile.CopyToAsync(new FileStream(imagePath, FileMode.Create));

                product.Image = product.ImageFile.FileName;
            }

             
            _db.Update(product);
            await _db.SaveChangesAsync();

            TempData["crudMessage"] = "Product has been Updated Successfully!";

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            var product = _db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            _db.Products.Remove(product);
            _db.SaveChanges();

            TempData["crudMessage"] = "Product has been Deleted Successfully!";

            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
