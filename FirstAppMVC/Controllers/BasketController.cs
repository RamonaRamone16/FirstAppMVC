using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstAppMVC.DAL.Entities;
using FirstAppMVC.Models;
using FirstAppMVC.Services.Basket;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FirstAppMVC.Controllers
{
    public class BasketController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IBasketService _basketService;

        public BasketController(UserManager<User> userManager, IBasketService basketService)
        {
            if (userManager == null)
                throw new ArgumentNullException(nameof(userManager));
            if (basketService == null)
                throw new ArgumentNullException(nameof(basketService));
            _userManager = userManager;
            _basketService = basketService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                User user = await _userManager.GetUserAsync(User);
                List<BasketItem> basketItems = _basketService.GetBasketItems(user);

                return View(basketItems);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
           
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(int? productId)
        {
            if (!productId.HasValue)
            {
                ViewBag.BadRequestMassage = "Product Id can not be NULL";
                return View("BadRequest");
            }

            try
            {
                User user = await _userManager.GetUserAsync(User);
                await _basketService.AddToBasketAsync(productId.Value, user);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete (int? productId)
        {
            if (!productId.HasValue)
            {
                ViewBag.BadRequestMassage = "Product Id can not be NULL";
                return View("BadRequest");
            }

            User user = await _userManager.GetUserAsync(User);

            await _basketService.DeleteAsync(productId.Value, user);

            return RedirectToAction("Index");
        }
    }
}