using CourseManagementSystem.Models.Categories;
using System.Collections.Generic;

namespace CourseManagementSystem.Services.Abstractions
{
    public interface ICategoriesService
    {
        List<CategoryDetailsViewModel> GetCategories();
        CategoryEditViewModel GetById(int id);
        void Insert(CategoryEditViewModel model);
        void Update(CategoryEditViewModel model);
        void Delete(int id);

        bool Exists(int id);
    }
}
