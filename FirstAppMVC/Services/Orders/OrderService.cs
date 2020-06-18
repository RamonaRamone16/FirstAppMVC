using AutoMapper;
using FirstAppMVC.DAL;
using FirstAppMVC.DAL.Entities;
using FirstAppMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FirstAppMVC.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public OrderService(IUnitOfWorkFactory unitOfWorkFactory)
        {
            if (unitOfWorkFactory == null)
                throw new ArgumentNullException(nameof(unitOfWorkFactory));
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public OrderCreateModel GetOrderCreateModel(int id)
        {
            using (UnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                Product product = unitOfWork.Products.GetAllWithBrandsCategoriesAndOrders()
                    .FirstOrDefault(p => p.Id == id);
                OrderCreateModel orderCreateModel = Mapper.Map<OrderCreateModel>(product);
                return orderCreateModel;
            }
        }

        public void CreateOrder(OrderCreateModel orderCreateModel)
        {
            using (UnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                Product product = unitOfWork.Products.GetById(orderCreateModel.ProductId);
                Order order = Mapper.Map<Order>(orderCreateModel);
                order.Data = DateTime.Now;
                order.Amount = product.Price * order.Count;

                unitOfWork.Orders.Create(order);
            }
        }

        public List<OrderModel> SearchOrderModels(OrdersFilter ordersFilter)
        {
            using (UnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                IEnumerable<Order> orders = unitOfWork.Orders.GetAllWithProductsAndCustomers()
                    .ByPriceFrom(ordersFilter.PriceFrom)
                    .ByPriceTo(ordersFilter.PriceTo);

                return Mapper.Map<List<OrderModel>>(orders);
            }
        }
    }
}
