using FirstAppMVC.DAL.Entities;
using FirstAppMVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FirstAppMVC.Services.Orders
{
    public interface IOrderService
    {
        OrderCreateModel GetOrderCreateModel(int id, int basketsCount);
        void CreateOrder(OrderCreateModel order, User user);
        List<OrderModel> SearchOrderModels(OrdersFilter ordersFilter, User user);
    }
}
