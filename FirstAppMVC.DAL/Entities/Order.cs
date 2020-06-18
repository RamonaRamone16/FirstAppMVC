using System;
using System.Collections.Generic;
using System.Text;

namespace FirstAppMVC.DAL.Entities
{
    public class Order : Entity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Count { get; set; }
        public decimal Amount { get; set; }
        public DateTime Data { get; set; }
        public string Description { get; set; }
    }
}
