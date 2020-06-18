using System;
using System.Collections.Generic;
using System.Text;

namespace FirstAppMVC.DAL
{
    public interface IApplicationDbContextFactory
    {
        ApplicationDbContext Create();
    }
}
