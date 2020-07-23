using System;
using System.ComponentModel;
using Microsoft.AspNetCore.Identity;

namespace MyOnlineShop.Models
{
    public class ApplicationUser : IdentityUser
    {
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
