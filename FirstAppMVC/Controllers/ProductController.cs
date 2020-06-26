using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using FirstAppMVC.Services.Products;
using FirstAppMVC.Models;

namespace FirstAppMVC.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            if (productService == null)
                throw new ArgumentNullException(nameof(productService));
            _productService = productService;
        }

        
        public IActionResult Index(ProductFilter model)
        {
            try
            {
                List<ProductModel> products = _productService.SearchProducts(model);
                model.Products = products;
                model.Categories = _productService.GetSelectListCategories();
                model.Brands = _productService.GetSelectListBrands();

                return View(model);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                ViewBag.BadRequestMessage = ex.Message;
                return View("BadRequest");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public IActionResult CreateProduct()
        {
            ProductCreateModel model = _productService.GetProductCreateModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult CreateProduct(ProductCreateModel model)
        {
            _productService.CreateProduct(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("Edit/{id}")]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                ViewBag.BadRequestMessage = "Product Id is Null";
                return View("BadRequest");
            }


            ProductEditModel product = _productService.GetProductEditModel(id.Value);
            return View(product);
        }

        [HttpPost]
        public IActionResult EditAnother(ProductEditModel product)
        {
            _productService.UpdateProduct(product);
            return RedirectToAction("Index");
        }

    }
}