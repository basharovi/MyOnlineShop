using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyOnlineShop.Models
{
    public class Order
    {
        public int Id { get; set; }

        [DisplayName("Order No")]
        public string OrderNo { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Phone]
        [DisplayName("Phone No")]
        public string PhoneNo { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }

        public DateTime OrderDate { get; set; }

        public List<OrderDetails> OrderDetails { get; set; }
    }
}
