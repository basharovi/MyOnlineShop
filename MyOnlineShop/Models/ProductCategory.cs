using System.ComponentModel.DataAnnotations;

namespace MyOnlineShop.Models
{
    public class ProductCategory
    {
        public int Id { get; set; }

        [Required]
        public string Category { get; set; }
    }
}
