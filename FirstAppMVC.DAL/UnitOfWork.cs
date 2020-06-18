using FirstAppMVC.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace FirstAppMVC.DAL
{
    public class UnitOfWork : IDisposable
    {
        private readonly ApplicationDbContext _context;
        public IProductRepository Products { get; }
        public ICategoryRepository Categories { get; }
        public IBrandRepository Brands { get; }
        public IOrderRepository Orders { get; }

        private bool disposed;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Products = new ProductRepository(context);
            Categories = new CategoryRepository(context);
            Brands = new BrandRepository(context);
            Orders = new OrderRepository(context);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
                _context.Dispose();

            disposed = true;
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}
