using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace my_online_shop.Models
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
