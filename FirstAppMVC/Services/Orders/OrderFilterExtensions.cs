using FirstAppMVC.DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace FirstAppMVC.Services.Orders
{
    public static class OrderFilterExtensions
    {
        public static IEnumerable<Order> ByPriceFrom(this IEnumerable<Order> productOrderModels,
            decimal? priceFrom)
        {
            if (priceFrom.HasValue)
                return productOrderModels.Where(o => o.Amount >= priceFrom);
            return productOrderModels;
        }

        public static IEnumerable<Order> ByPriceTo(this IEnumerable<Order> productOrderModels,
            decimal? priceTo)
        {
            if (priceTo.HasValue)
                return productOrderModels.Where(o => o.Amount <= priceTo);
            return productOrderModels;
        }
    }
}
