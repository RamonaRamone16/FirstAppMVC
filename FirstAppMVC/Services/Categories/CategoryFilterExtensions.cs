using FirstAppMVC.DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace FirstAppMVC.Services.Categories
{
    public static class CategoryFilterExtensions
    {
        public static IEnumerable<Category> ByName(this IEnumerable<Category> categories, string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
                return categories.Where(c => c.Name.Contains(name));
            return categories;
        }
    }
}
