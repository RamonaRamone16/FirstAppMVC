using AutoMapper;
using FirstAppMVC.DAL;
using FirstAppMVC.DAL.Entities;
using FirstAppMVC.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FirstAppMVC.Services.Basket
{
    public class BasketService : IBasketService
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly UserManager<User> _userManager;

        public BasketService(IUnitOfWorkFactory unitOfWorkFactory, UserManager<User> userManager)
        {
            if (unitOfWorkFactory == null)
                throw new ArgumentNullException(nameof(unitOfWorkFactory));
            if (userManager == null)
                throw new ArgumentNullException(nameof(userManager));
            _unitOfWorkFactory = unitOfWorkFactory;
            _userManager = userManager;
        }

        public async Task AddToBasketAsync(int productId, User user)
        {
            using (UnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                Product product = unitOfWork.Products.GetById(productId);

                if (product == null)
                    throw new ArgumentOutOfRangeException(nameof(productId), $"No product with {productId} Id was found");

                List<BasketItemCreate> basketItems = string.IsNullOrWhiteSpace(user.Basket) ?
                    CreateBasket(productId) : UpdateBasket(productId, user.Basket);

                user.Basket = JsonConvert.SerializeObject(basketItems);

                await _userManager.UpdateAsync(user);
            }
        }

        public async Task DeleteAsync(int productId, User user)
        {
            List<BasketItemCreate> basketItemCreates =
                    JsonConvert.DeserializeObject<List<BasketItemCreate>>(user.Basket);

            BasketItemCreate basketItemCreate = basketItemCreates.FirstOrDefault(p => p.ProductId == productId);

            basketItemCreates.Remove(basketItemCreate);

            user.Basket = JsonConvert.SerializeObject(basketItemCreates);
            await _userManager.UpdateAsync(user);
        }

        public List<BasketItem> GetBasketItems(User user)
        {
            using (UnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                List<BasketItemCreate> basketItemCreates = 
                    JsonConvert.DeserializeObject<List<BasketItemCreate>>(user.Basket);

                List<BasketItem> basketItems = new List<BasketItem>();
                Product product;
                BasketItem basketItem;

                if (basketItemCreates != null)
                {
                    foreach (BasketItemCreate bIC in basketItemCreates)
                    {
                        product = unitOfWork.Products.GetById(bIC.ProductId);

                        if (product.BrandId.HasValue) 
                            product.Brand = unitOfWork.Brands.GetById(product.BrandId.Value);

                        basketItem = Mapper.Map<BasketItem>(product);
                        basketItem.BasketsCount = bIC.Count;
                        basketItem.Amount = basketItem.Price * basketItem.BasketsCount;

                        basketItems.Add(basketItem);
                    }
                }

                return basketItems;
            }
        }


        private List<BasketItemCreate> UpdateBasket(int productId, string basket)
        {
            try
            {
                List<BasketItemCreate> basketItems =
                    JsonConvert.DeserializeObject<List<BasketItemCreate>>(basket);

                BasketItemCreate existingItem = basketItems.FirstOrDefault(p => p.ProductId == productId);
                if (existingItem == null)
                {
                    basketItems.Add(new BasketItemCreate(productId, 1));
                }
                else
                {
                    existingItem.Count++;
                }
                return basketItems;
            }
            catch (Exception)
            {
                return CreateBasket(productId);
            }
        }

        private List<BasketItemCreate> CreateBasket(int productId)
        {
            return new List<BasketItemCreate>() { new BasketItemCreate(productId, 1) };
        }

    }
}
