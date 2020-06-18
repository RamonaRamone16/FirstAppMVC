using System;
using System.Collections.Generic;
using FirstAppMVC.Models;
using FirstAppMVC.Services.Brands;
using Microsoft.AspNetCore.Mvc;

namespace FirstAppMVC.Controllers
{
    public class BrandController : Controller
    {
        private IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            if (brandService == null)
                throw new ArgumentNullException(nameof(brandService));
            _brandService = brandService;
        }

        public IActionResult Index(BrandModel brandModel)
        {
            try
            {
                List<BrandModel> brandModels = _brandService.SearchBrand(brandModel);
                return View(brandModels);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }

        [HttpGet]
        public IActionResult CreateBrand()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateBrand(BrandCreateModel model)
        {
            _brandService.CreateBrand(model);
            return RedirectToAction("Index");
        }
    }
}