using MVCBartenderApp.Models.Enums;
using System;
using System.Collections.Generic;

#nullable disable

namespace MVCBartenderApp.Models
{
    public partial class Order
    {
        public string OrderId { get; set; }
        public int CocktailId { get; set; }
        public int UserId { get; set; }
        public OrderStatus Status { get; set; }

        public virtual Cocktail Cocktail { get; set; }
        public virtual User User { get; set; }
    }
}
