using System;
using System.Collections.Generic;
using System.Text;

namespace FirstAppMVC.DAL.Entities
{
    public class Category : Entity
    {
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
