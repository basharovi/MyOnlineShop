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

        public async Task<IActionResult> Update(string id)
        {
            var user = await _db.Users.FindAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ApplicationUser user)
        {
            var userInfo = await _db.Users.FindAsync(user.Id);

            if (userInfo is null)
            {
                return NotFound();
            }

            userInfo.FirstName = user.FirstName;
            userInfo.LastName = user.LastName;
            userInfo.Email = user.Email;
            userInfo.PhoneNumber = user.PhoneNumber;

            var result = await _userManager.UpdateAsync(userInfo);

            if (result.Succeeded)
            {
                TempData["crudMessage"] = "User has been Updated Successfully!";

                return Redirect("/User/List");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }

            return View(user);
        }

        public IActionResult Delete(string id)
        {
            var userInfo = _db.Users.Find(id);
            if (userInfo == null)
            {
                return NotFound();
            }

            _db.Users.Remove(userInfo);
            _db.SaveChanges();

            TempData["crudMessage"] = "User has been Deleted Successfully!";

            return Redirect("/User/List");
        }
    }
}
