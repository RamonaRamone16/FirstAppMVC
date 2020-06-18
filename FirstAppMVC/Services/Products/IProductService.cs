using FirstAppMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace FirstAppMVC.Services.Products
{
    public interface IProductService
    {
        List<ProductModel> SearchProducts(ProductFilter model);
        ProductCreateModel GetProductCreateModel();
        void CreateProduct(ProductCreateModel model);
        SelectList GetSelectListCategories();
        SelectList GetSelectListBrands();
        ProductEditModel GetProductEditModel(int id);
        void UpdateProduct(ProductEditModel product);
    }
}
