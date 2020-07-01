using MyOnlineShop.Data;
using System.Diagnostics;
using MyOnlineShop.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;



namespace MyOnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SpecialTagController : Controller
    {
        private readonly ApplicationDbContext _db;


        public SpecialTagController(ApplicationDbContext db) => _db = db;

        public IActionResult Index()
        {
            return View(_db.SpecialTags);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SpecialTag specialTag)
        {
            if(!ModelState.IsValid)
                return View();


            _db.Add(specialTag);
            await _db.SaveChangesAsync();

            TempData["crudMessage"] = "Special Tag has been Created Successfully!";

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Show(int? id)
        {
            var specialTag = _db.SpecialTags.Find(id);
            if (specialTag == null)
            {
                return NotFound();
            }

            return View(specialTag);
        }

        [HttpGet]
        public IActionResult Update(int? id)
        {
            var specialTag = _db.SpecialTags.Find(id);
            if (specialTag == null)
            {
                return NotFound();
            }

            return View(specialTag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(SpecialTag specialTag)
        {
            if (!ModelState.IsValid)
                return View();


            _db.Update(specialTag);
            await _db.SaveChangesAsync();

            TempData["crudMessage"] = "Special Tag has been Updated Successfully!";

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            var specialTag = _db.SpecialTags.Find(id);
            if (specialTag == null)
            {
                return NotFound();
            }

            _db.SpecialTags.Remove(specialTag);
            _db.SaveChanges();

            TempData["crudMessage"] = "Special Tag has been Deleted Successfully!";

            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
