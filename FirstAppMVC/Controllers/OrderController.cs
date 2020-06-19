using System;
using System.Collections.Generic;
using FirstAppMVC.Models;
using FirstAppMVC.Services.Orders;
using Microsoft.AspNetCore.Mvc;

namespace FirstAppMVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            if (orderService == null)
                throw new ArgumentNullException(nameof(orderService));
            _orderService = orderService;
        }


        [HttpGet]
        public IActionResult Index(OrdersFilter ordersFilter)
        {
            try
            {
                List<OrderModel> orders = _orderService
                    .SearchOrderModels(ordersFilter);
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
        public IActionResult OrderCreate(OrderCreateModel order)
        {
            _orderService.CreateOrder(order);
            return RedirectToAction("Index");
        }
    }
}