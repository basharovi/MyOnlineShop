using System.ComponentModel.DataAnnotations;

namespace my_online_shop.Models
{
    public class SpecialTag
    {
        public int Id { get; set; }

        [Required]
        public string Tag { get; set; }
    }
}
