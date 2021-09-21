using CourseManagementSystem.Entities;
using System.Collections.Generic;

namespace CourseManagementSystem.Repositories.Abstractions
{
    public interface ICategoriesRepository
    {
        List<Category> GetCategories();
        Category GetById(int id);
        void Insert(Category category);
        void Update(Category category);
        void Delete(int id);
    }
}
