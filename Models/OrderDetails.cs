using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace my_online_shop.Models
{
    public class OrderDetails
    {
        public int Id {get; set; }


        [Required]
        [DisplayName("Order")]
        public int OrderId { get; set; }


        [Required]
        [DisplayName("Product")]
        public int ProductId { get; set; }


        [ForeignKey("OrderId")]
        public Order Order { get; set; }


        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
