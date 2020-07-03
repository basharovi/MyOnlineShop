using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyOnlineShop.Models
{
    public class ProductCategory
    {
        public int Id { get; set; }

        [Required]
        public string Category { get; set; }

    }
}
