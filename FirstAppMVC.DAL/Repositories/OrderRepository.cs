using FirstAppMVC.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FirstAppMVC.DAL.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context):base(context)
        {
            entities = context.Orders;
        }

        public IEnumerable<Order> GetAllWithProducts()
        {
            return entities.Include(o => o.Product).ThenInclude(o => o.Brand);
        }
    }
}
