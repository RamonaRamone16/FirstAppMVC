using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;

namespace FirstAppMVC.Models
{
    public class OrderModel
    {
        public string ProductName { get; set; }
        public string Brand { get; set; }
        public decimal ProductPrice { get; set; }
        public string CustomerName { get; set; }
        public int Count { get; set; }
        public decimal Amount { get; set; }
        public DateTime Data { get; set; }
    }
}
