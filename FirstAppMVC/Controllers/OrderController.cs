using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FirstAppMVC.DAL.Entities;
using FirstAppMVC.Models;
using FirstAppMVC.Services.Orders;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FirstAppMVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly UserManager<User> _userManager;

        public OrderController(IOrderService orderService, UserManager<User> userManager)
        {
            if (orderService == null)
                throw new ArgumentNullException(nameof(orderService));
            if (userManager == null)
                throw new ArgumentNullException(nameof(userManager));
            _orderService = orderService;
            _userManager = userManager;
        }


        [HttpGet]
        public async Task<IActionResult> Index(OrdersFilter ordersFilter)
        {
            try
            {
                User user = await _userManager.GetUserAsync(User);
                List<OrderModel> orders = _orderService
                    .SearchOrderModels(ordersFilter, user);
                ordersFilter.Orders = orders;

                return View(ordersFilter);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("Order/{id}")]
        public IActionResult Order(int? productId, int? basketsCount)
        {
            if (!productId.HasValue)
            {
                ViewBag.BadRequestMessage = "Product Id is Null";
                return View("BadRequest");
            }
            OrderCreateModel productOrderModel = _orderService.GetOrderCreateModel(productId.Value, basketsCount.Value);
            return View(productOrderModel);
        }


        [HttpPost]
        public async Task<IActionResult> OrderCreate(OrderCreateModel order)
        {
            User user = await _userManager.GetUserAsync(User);
            _orderService.CreateOrder(order, user);
            return RedirectToAction("Index");
        }
    }
}