using System;
using System.Collections.Generic;

#nullable disable

namespace MVCBartenderApp.Models
{
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
        }

        public int UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public string Role { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
