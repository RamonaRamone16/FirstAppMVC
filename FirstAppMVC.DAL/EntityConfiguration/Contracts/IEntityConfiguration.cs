using FirstAppMVC.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FirstAppMVC.DAL.EntityConfiguration
{
    public interface IEntityConfiguration<T> where T : Entity
    {
        Action<EntityTypeBuilder<T>> ProvideConfigurationAction();
    }
}
