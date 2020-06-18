using FirstAppMVC.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FirstAppMVC.DAL.EntityConfiguration
{
    public class OrderConfiguration : BaseEntityConfiguration<Order>
    {

        protected override void ConfigureProperties(EntityTypeBuilder<Order> builder)
        {
            builder.Property(o => o.Count)
                .HasDefaultValue(1)
                .IsRequired();

            builder.Property(o => o.Description)
                .HasMaxLength(500);

            builder.Property(o => o.Data)
                .IsRequired();

            builder.Property(o => o.Amount)
                .IsRequired();

            builder.HasIndex(o => o.Data)
                .IsUnique(false);
        }

        protected override void ConfigureForeignKey(EntityTypeBuilder<Order> builder)
        {
            builder.HasOne(o => o.Product)
                .WithMany(o => o.Orders)
                .HasForeignKey(o => o.ProductId)
                .IsRequired();
        }
    }
}
