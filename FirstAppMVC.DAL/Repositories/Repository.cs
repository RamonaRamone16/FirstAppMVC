using FirstAppMVC.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirstAppMVC.DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private ApplicationDbContext _context;
        protected DbSet<T> entities;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public T Create(T entity)
        {
            var entryEntity = entities.Add(entity);
            _context.SaveChanges();
            return entryEntity.Entity;
        }
        public T GetById(int id)
        {
            return entities.FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<T> GetAll()
        {
            return entities.ToList();
        }

        public void Remove(T entity)
        {
            entities.Remove(entity);
            _context.SaveChanges();
        }

        public T Update(T entity)
        {
            var entryEntity = entities.Update(entity);
            _context.SaveChanges();
            return entryEntity.Entity;
        }
    }
}
