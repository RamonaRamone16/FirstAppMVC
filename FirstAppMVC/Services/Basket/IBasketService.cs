using FirstAppMVC.DAL.Entities;
using FirstAppMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FirstAppMVC.Services.Basket
{
    public interface IBasketService
    {
        Task AddToBasket(int productId, User user);
        List<BasketItem> GetBasketItems(User user);
    }
}
