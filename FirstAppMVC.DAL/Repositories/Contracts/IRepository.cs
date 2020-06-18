using FirstAppMVC.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FirstAppMVC.DAL.Repositories
{
    public interface IRepository<T> where T : Entity
    {
        T Create(T entity);
        T GetById(int id);
        IEnumerable<T> GetAll();
        T Update(T entity);
        void Remove(T entity);
    }
}
