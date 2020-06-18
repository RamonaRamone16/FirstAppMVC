using FirstAppMVC.Models;
using System.Collections.Generic;

namespace FirstAppMVC.Services.Brands
{
    public interface IBrandService
    {
        List<BrandModel> SearchBrand(BrandModel brandModel);
        void CreateBrand(BrandCreateModel brandModel);
    }
}
