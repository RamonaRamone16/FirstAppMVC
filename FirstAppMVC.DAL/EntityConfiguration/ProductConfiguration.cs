using FirstAppMVC.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FirstAppMVC.DAL.EntityConfiguration
{
    public class ProductConfiguration : BaseEntityConfiguration<Product>
    {
        protected override void ConfigureProperties(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(p => p.Price)
                .HasDefaultValue(0.0M)
                .IsRequired();

            builder.HasIndex(p => p.Name)
                .IsUnique(false);
        }

        protected override void ConfigureForeignKey(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(p => p.Category)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.CategoryId)
                .IsRequired();

            builder.HasOne(p => p.Brand)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.BrandId);

            builder.HasMany(p => p.Orders)
                .WithOne(p => p.Product)
                .HasForeignKey(p => p.ProductId)
                .IsRequired();
        }
    }
}
