using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyOnlineShop.Models
{
    public class ProductCategory
    {
        public int Id { get; set; }

        [Required]
        public string Category { get; set; }

        public List<Product> Products { get; set; }

        public ProductCategory()
        {
            Products = new List<Product>();
        }
    }
}
