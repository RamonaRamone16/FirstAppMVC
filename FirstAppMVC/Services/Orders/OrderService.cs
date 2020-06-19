using AutoMapper;
using FirstAppMVC.DAL;
using FirstAppMVC.DAL.Entities;
using FirstAppMVC.Models;
using FirstAppMVC.Services.Basket;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstAppMVC.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IBasketService _basketService;
        private readonly UserManager<User> _userManager;

        public OrderService(IUnitOfWorkFactory unitOfWorkFactory, IBasketService basketService, 
            UserManager<User> userManager)
        {
            if (unitOfWorkFactory == null)
                throw new ArgumentNullException(nameof(unitOfWorkFactory));
            if (basketService == null)
                throw new ArgumentNullException(nameof(basketService));
            if (userManager == null)
                throw new ArgumentNullException(nameof(userManager));
            _basketService = basketService;
            _unitOfWorkFactory = unitOfWorkFactory;
            _userManager = userManager;
        }

        public OrderCreateModel GetOrderCreateModel(int id, int basketsCount)
        {
            using (UnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                Product product = unitOfWork.Products.GetAllWithBrandsCategoriesAndOrders()
                    .FirstOrDefault(p => p.Id == id);
                OrderCreateModel orderCreateModel = Mapper.Map<OrderCreateModel>(product);
                orderCreateModel.Count = basketsCount;
                return orderCreateModel;
            }
        }

        public void CreateOrder(OrderCreateModel orderCreateModel, User user)
        {
            using (UnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                Product product = unitOfWork.Products.GetById(orderCreateModel.ProductId);
                Order order = Mapper.Map<Order>(orderCreateModel);
                order.UserId = user.Id;
                order.Data = DateTime.Now;
                order.Amount = product.Price * order.Count;

                _basketService.DeleteAsync(orderCreateModel.ProductId, user);

                unitOfWork.Orders.Create(order);
            }
        }

        public  List<OrderModel> SearchOrderModels(OrdersFilter ordersFilter, User user)
        {
            using (UnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                IEnumerable<Order> orders = unitOfWork.Orders.GetAllWithProducts()
                    .ByPriceFrom(ordersFilter.PriceFrom)
                    .ByPriceTo(ordersFilter.PriceTo)
                    .Where(u => u.UserId == user.Id);

                return Mapper.Map<List<OrderModel>>(orders);
            }
        }
    }
}
