using System.Collections.Generic;
using MyOnlineShop.Models;

namespace MyOnlineShop.Data.Repository
{
    public class ProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public ProductCategory Get(int id)
        {
            return _db.ProductCategories.Find(id);
        }

        public IEnumerable<ProductCategory> GetAll()
        {
            return _db.ProductCategories;
        }
    }
}
