using FirstAppMVC.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirstAppMVC.DAL.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            entities = context.Products;
        }

        public IEnumerable<Product> GetAllWithBrandsCategoriesAndOrders()
        {
            return entities.Include(p => p.Brand).Include(p => p.Category).Include(p => p.Orders).ToList();
        }

    }
}
