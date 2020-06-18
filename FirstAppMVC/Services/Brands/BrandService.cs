using AutoMapper;
using FirstAppMVC.DAL;
using FirstAppMVC.DAL.Entities;
using FirstAppMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FirstAppMVC.Services.Brands
{
    public class BrandService : IBrandService
    {
        private IUnitOfWorkFactory _unitOfWorkFactory;

        public BrandService(IUnitOfWorkFactory unitOfWorkFactory)
        {
            if (unitOfWorkFactory == null)
                throw new ArgumentNullException(nameof(unitOfWorkFactory));
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public List<BrandModel> SearchBrand(BrandModel brandModel)
        {
            using (UnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                List<Brand> brands = unitOfWork.Brands.GetAll().ByName(brandModel.Name).ToList();
                return Mapper.Map<List<BrandModel>>(brands);
            }
        }

        public void CreateBrand(BrandCreateModel model)
        {
            using (UnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                unitOfWork.Brands.Create(Mapper.Map<Brand>(model));
            }
        }
    }
}
