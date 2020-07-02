using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyOnlineShop.Models
{
    public class Product
    {
        public int  Id { get; set; }

        [Required]
        public string Name { get; set; }


        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Color { get; set; }

        public string Image { get; set; }


        [Required]
        [DisplayName("Available")]
        public bool IsAvailable { get; set; }


        [Required]
        [DisplayName("Product Category")]
        public int CategoryId { get; set; }


        [ForeignKey("CategoryId")]
        public ProductCategory Category { get; set; }


        [Required]
        [DisplayName("Special Tag")]
        public int TagId { get; set; }

        
        [ForeignKey("TagId")]
        public SpecialTag Tag { get; set; }


    }
}
