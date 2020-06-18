using FirstAppMVC.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FirstAppMVC.DAL.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetAllWithBrandsCategoriesAndOrders();
    }
}
