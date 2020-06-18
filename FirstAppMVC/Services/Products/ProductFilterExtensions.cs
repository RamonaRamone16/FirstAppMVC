using FirstAppMVC.DAL;
using FirstAppMVC.DAL.Entities;
using FirstAppMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FirstAppMVC.UI.Services.Products
{

    public static class ProductFilterExtensions
    {
        public static IEnumerable<Product> ByPriceFrom(this IEnumerable<Product> products, decimal? priceFrom)
        {
            if (priceFrom.HasValue)
                return products.Where(p => p.Price >= priceFrom.Value);
            return products;
        }

        public static IEnumerable<Product> ByPriceTo(this IEnumerable<Product> products, decimal? priceTo)
        {
            if (priceTo.HasValue)
                return products.Where(p => p.Price <= priceTo.Value);
            return products;
        }

        public static IEnumerable<Product> ByName(this IEnumerable<Product> products, string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
                return products.Where(p => p.Name.Contains(name));
            return products;
        }

        public static IEnumerable<Product> ByCategoryId(this IEnumerable<Product> products,
            UnitOfWork unitOfWork, int? categoryId)
        {
            if (categoryId.HasValue)
            {
                var category = unitOfWork.Categories.GetById(categoryId.Value);
                if (category == null)
                    throw new ArgumentOutOfRangeException(nameof(category),
                        $"No category with Id {categoryId}");
                return products.Where(p => p.CategoryId == categoryId.Value);
            }

            return products;
        }
        public static IEnumerable<Product> ByBrandId(this IEnumerable<Product> products,
            UnitOfWork unitOfWork, int? brandId)
        {
            if (brandId.HasValue)
            {
                var category = unitOfWork.Brands.GetById(brandId.Value);
                if (category == null)
                    throw new ArgumentOutOfRangeException(nameof(category),
                        $"No category with Id {brandId}");
                return products.Where(p => p.BrandId == brandId.Value);
            }

            return products;
        }

        public static IEnumerable<ProductModel> Sorting(this IEnumerable<ProductModel> models,
            SortType type,
            SortDirection direction)
        {
            switch (type)
            {
                case SortType.Name:
                    models = models.SortingByDirection(direction, p => p.Name);
                    break;
                case SortType.Brand:
                    models = models.SortingByDirection(direction, p => p.Brand); 
                    break;
                case SortType.Category:
                    models = models.SortingByDirection(direction, p => p.Category);
                    break;
                case SortType.Price:
                    models = models.SortingByDirection(direction, p => p.Price);
                    break;
                case SortType.OrdersCount:
                    models = models.SortingByDirection(direction, p => p.OrdersCount);
                    break;
            }
            return models;
        }

        public static IEnumerable<ProductModel> SortingByDirection<T>(this IEnumerable<ProductModel> models,
            SortDirection direction,
            Func<ProductModel, T> func)
        {
            return direction == SortDirection.Asc ? models.OrderBy(func) : models.OrderByDescending(func);
        }
    }
}
