using FirstAppMVC.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FirstAppMVC.DAL.EntityConfiguration
{
    public class CategoryConfiguration : BaseEntityConfiguration<Category>
    {
        protected override void ConfigureProperties(EntityTypeBuilder<Category> builder)
        {
            builder.Property(c => c.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.HasIndex(c => c.Name)
                .IsUnique();
        }
        protected override void ConfigureForeignKey(EntityTypeBuilder<Category> builder)
        {
            builder.HasMany(p => p.Products)
               .WithOne(p => p.Category)
               .HasForeignKey(p => p.CategoryId)
               .IsRequired();
        }

    }
}
