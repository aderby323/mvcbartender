using System;
using System.Collections.Generic;

#nullable disable

namespace MVCBartenderApp.Models
{
    public partial class Cocktail
    {
        public Cocktail()
        {
            Orders = new HashSet<Order>();
        }

        public int CocktailId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
