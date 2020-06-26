using System;
using System.Collections.Generic;
using FirstAppMVC.DAL;
using System.Linq;
using FirstAppMVC.DAL.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using FirstAppMVC.Models;
using FirstAppMVC.Services.Products;
using Newtonsoft.Json;

namespace FirstAppMVC.UI.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public ProductService(IUnitOfWorkFactory unitOfWorkFactory)
        {
            if (unitOfWorkFactory == null)
                throw new ArgumentNullException(nameof(unitOfWorkFactory));
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public List<ProductModel> SearchProducts(ProductFilter model)
        {
            using (UnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                IEnumerable<Product> products = unitOfWork.Products.GetAllWithBrandsCategoriesAndOrders()
                    .ByPriceFrom(model.PriceFrom)
                    .ByPriceTo(model.PriceTo)
                    .ByName(model.Name)
                    .ByCategoryId(unitOfWork, model.CategoryId)
                    .ByBrandId(unitOfWork, model.BrandId);

                List<ProductModel> productModels = Mapper.Map<List<ProductModel>>(products);
                productModels = productModels.Sorting(model.SortType, model.SortDirection).ToList();

                int pageSize = 3;
                int currentPage = model.CurrentPage.HasValue ? model.CurrentPage.Value : 1;
                int pagesCount = productModels.Count;

                PagingModel pagingModel = new PagingModel(pagesCount, pageSize, currentPage);

                productModels = productModels.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

                model.CurrentPage = currentPage;
                model.Paging = pagingModel;
                model.SortDirection = model.SortDirection == SortDirection.Asc ? SortDirection.Desc :
                    SortDirection.Asc;

                return productModels;
            }
        }
        public ProductCreateModel GetProductCreateModel()
        {
            using (UnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                return new ProductCreateModel()
                {
                    CategorySelect = GetSelectListCategories(),
                    BrandSelect = GetSelectListBrands()
                };
            }   
        }

        public void CreateProduct(ProductCreateModel model)
        {
            using (UnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                unitOfWork.Products.Create(Mapper.Map<Product>(model));
            }
        }

        public SelectList GetSelectListCategories()
        {
            using (UnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                List<Category> categories = unitOfWork.Categories.GetAll().ToList();
                return new SelectList(categories, nameof(Category.Id), nameof(Category.Name));
            }
        }

        public SelectList GetSelectListBrands()
        {
            using (UnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                List<Brand> brands = unitOfWork.Brands.GetAll().ToList();
                return new SelectList(brands, nameof(Brand.Id), nameof(Brand.Name));
            }
        }

        public ProductEditModel GetProductEditModel(int id)
        {
            using (UnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                Product product = unitOfWork.Products.GetById(id);
                ProductEditModel productEditModel = Mapper.Map<ProductEditModel>(product);

                productEditModel.Brands = new SelectList(unitOfWork.Brands.GetAll(),
                    nameof(Brand.Id), nameof(Brand.Name), productEditModel.BrandId);

                productEditModel.Categories = new SelectList(unitOfWork.Categories.GetAll(),
                    nameof(Category.Id), nameof(Category.Name), productEditModel.CategoryId);

                return productEditModel;
            }
        }

        public void UpdateProduct(ProductEditModel productEditModel)
        {
            using (UnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                unitOfWork.Products.Update(Mapper.Map<Product>(productEditModel));
            }
        }

        public List<ProductModel> GetViewedProductsById(string productsIds)
        {
            using (UnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                List<ProductModel> viewedProducts = new List<ProductModel>();
                List<Product> allProducts = unitOfWork.Products.GetAllWithBrandsCategoriesAndOrders().ToList();
                foreach (Product p in allProducts)
                {
                    if (productsIds.Contains(p.Id.ToString()))
                        viewedProducts.Add(Mapper.Map<ProductModel>(p));
                }
                return viewedProducts;
            }
        }
    }
}
