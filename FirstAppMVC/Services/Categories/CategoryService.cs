using FirstAppMVC.DAL;
using FirstAppMVC.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FirstAppMVC.Models;

namespace FirstAppMVC.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        public CategoryService(IUnitOfWorkFactory unitOfWorkFactory)
        {
            if (unitOfWorkFactory == null)
                throw new ArgumentNullException(nameof(unitOfWorkFactory));
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public List<CategoryModel> SearchCategories(CategoryModel model)
        {
            using (UnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                List<Category> categories = unitOfWork.Categories.GetAll()
                    .ByName(model.Name).ToList();
                return Mapper.Map<List<CategoryModel>>(categories);
            }
        }
        public void CreateCategory(CategoryCreateModel model)
        {
            using (UnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                unitOfWork.Categories.Create(Mapper.Map<Category>(model));
            }
        }

    }
}
