using MVCBartenderApp.Context;
using MVCBartenderApp.Models;
using MVCBartenderApp.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using MVCBartenderApp.Models.Enums;
using System;
using Microsoft.EntityFrameworkCore;

namespace MVCBartenderApp.Services
{
    public class BartenderService : IBartenderService
    {
        private readonly MVCBartenderContext _context;
        public BartenderService(MVCBartenderContext context)
        {
            _context = context;
        }

        public bool AddOrder(int cocktailID)
        {
            Cocktail c = _context.Cocktails.Where(x => x.CocktailId == cocktailID).FirstOrDefault();

            if (c is null) { return false; }

            Order order = new Order()
            {
                OrderId = Guid.NewGuid().ToString(),
                CocktailId = cocktailID,
                UserId = 1,
                Status = OrderStatus.Pending
            };

            try
            {
                _context.Add(order);
                _context.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public List<Cocktail> GetMenu()
        {
            return _context.Cocktails.ToList();
        }

        public List<Order> GetOrderQueue()
        {
            List<Order> orders = _context.Orders
                .Include(x => x.Cocktail)
                .Include(x => x.User)
                .ToList();

            return orders;
        }
    }
}
