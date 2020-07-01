using System.ComponentModel.DataAnnotations;

namespace MyOnlineShop.Models
{
    public class SpecialTag
    {
        public int Id { get; set; }

        [Required]
        public string Tag { get; set; }
    }
}
