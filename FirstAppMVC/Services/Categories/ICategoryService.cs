using FirstAppMVC.Models;
using System.Collections.Generic;

namespace FirstAppMVC.Services.Categories
{
    public interface ICategoryService
    {
        List<CategoryModel> SearchCategories(CategoryModel model);
        void CreateCategory(CategoryCreateModel model);
    }
}
