using FirstAppMVC.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FirstAppMVC.DAL.EntityConfiguration
{
    public interface IEntityConfigurationContainer
    {
        IEntityConfiguration<Product> ProductConfiguration { get; }
        IEntityConfiguration<Category> CategoryConfiguration { get; }
        IEntityConfiguration<Brand> BrandConfiguration { get; }
        IEntityConfiguration<Order> OrderConfiguration { get; }
    }
}
