using FirstAppMVC.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FirstAppMVC.DAL.EntityConfiguration
{
    public class BrandConfiguration : BaseEntityConfiguration<Brand>
    {
        protected override void ConfigureProperties(EntityTypeBuilder<Brand> builder)
        {
            builder.Property(c => c.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasIndex(c => c.Name)
                .IsUnique();
        }

        protected override void ConfigureForeignKey(EntityTypeBuilder<Brand> builder)
        {
            builder.HasMany(p => p.Products)
               .WithOne(p => p.Brand)
               .HasForeignKey(p => p.BrandId);
        }
    }
}
