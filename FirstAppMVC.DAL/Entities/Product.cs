using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirstAppMVC.DAL.Entities
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int? BrandId { get; set; }
        public Brand Brand { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
