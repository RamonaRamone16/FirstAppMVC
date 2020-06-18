using FirstAppMVC.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FirstAppMVC.DAL.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            entities = context.Categories;
        }
    }
}
