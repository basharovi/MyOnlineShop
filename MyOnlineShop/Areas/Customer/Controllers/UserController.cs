using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyOnlineShop.Data;
using MyOnlineShop.Models;

namespace MyOnlineShop.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _db;

        public UserController
        (
            UserManager<IdentityUser> userManager,
            ApplicationDbContext db
        )
        {
            _userManager = userManager;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(ApplicationUser user)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _userManager.CreateAsync(user);

            if (result.Succeeded)
            {
                return Redirect("/Identity/Account/Login");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }

            return View(user);
        }

        public IActionResult List()
        {
            return View(_db.Users);
        }

    }
}
