using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyOnlineShop.Data;
using MyOnlineShop.Data.Repository;

namespace MyOnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductCategoryController : Controller
    {
        private readonly ILogger<ProductCategoryController> _logger;
        private readonly ProductRepository _repository;
        private readonly ApplicationDbContext _db;


        public ProductCategoryController(
            ILogger<ProductCategoryController> logger,
            ApplicationDbContext db)
        {
            _logger = logger;
            //_repository = repository;
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.ProductCategories);
        }
    }
}
