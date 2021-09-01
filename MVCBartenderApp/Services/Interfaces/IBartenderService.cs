using MVCBartenderApp.Models;
using System.Collections.Generic;

namespace MVCBartenderApp.Services.Interfaces
{
    public interface IBartenderService
    {
        List<Cocktail> GetMenu();
        List<Order> GetOrderQueue();

        bool AddOrder(int cocktailID);
    }
}
