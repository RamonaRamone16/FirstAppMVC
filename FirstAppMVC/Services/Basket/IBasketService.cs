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
        Task AddToBasketAsync(int productId, User user);
        List<BasketItem> GetBasketItems(User user);
        Task DeleteAsync(int productId, User user);
    }
}
