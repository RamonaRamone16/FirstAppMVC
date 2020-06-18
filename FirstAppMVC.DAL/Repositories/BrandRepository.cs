using FirstAppMVC.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirstAppMVC.DAL.Repositories
{
    public class BrandRepository : Repository<Brand>, IBrandRepository
    {
        public BrandRepository(ApplicationDbContext context) : base(context)
        {
            entities = context.Brands;
        }

        public IEnumerable<Brand> GetAllBySorting()
        {
            return entities.OrderBy(n => n.Name);
        }
    }
}
