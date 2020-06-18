using FirstAppMVC.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FirstAppMVC.DAL.EntityConfiguration
{
    public class EntityConfigurationContainer : IEntityConfigurationContainer
    {
        public IEntityConfiguration<Product> ProductConfiguration { get; }
        public IEntityConfiguration<Category> CategoryConfiguration { get; }
        public IEntityConfiguration<Brand> BrandConfiguration { get; }
        public IEntityConfiguration<Order> OrderConfiguration { get; }

        public EntityConfigurationContainer()
        {
            ProductConfiguration = new ProductConfiguration();
            CategoryConfiguration = new CategoryConfiguration();
            BrandConfiguration = new BrandConfiguration();
            OrderConfiguration = new OrderConfiguration();
        }
    }
}
