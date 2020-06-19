using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstAppMVC.Models
{
    public class BasketItem
    {
        public const string NoBrand = "NoName";
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        public int BasketsCount { get; set; }
    }
}
