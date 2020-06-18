using FirstAppMVC.DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace FirstAppMVC.Services.Brands
{
    public static class BrandFilterExtensions
    {
        public static IEnumerable<Brand> ByName(this IEnumerable<Brand> brands, string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
                return brands.Where(b => b.Name.Contains(name));
            return brands;
        }
    }
}
