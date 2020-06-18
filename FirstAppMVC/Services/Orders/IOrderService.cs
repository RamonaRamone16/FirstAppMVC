﻿using FirstAppMVC.Models;
using System.Collections.Generic;

namespace FirstAppMVC.Services.Orders
{
    public interface IOrderService
    {
        OrderCreateModel GetOrderCreateModel(int id);
        void CreateOrder(OrderCreateModel order);
        List<OrderModel> SearchOrderModels(OrdersFilter ordersFilter);
    }
}