using FirstAppMVC.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FirstAppMVC.DAL.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        IEnumerable<Order> GetAllWithProducts();
    }
}
