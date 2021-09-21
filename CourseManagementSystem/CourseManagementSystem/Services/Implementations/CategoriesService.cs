using CourseManagementSystem.Entities;
using CourseManagementSystem.Models.Categories;
using CourseManagementSystem.Repositories.Abstractions;
using CourseManagementSystem.Services.Abstractions;
using System.Collections.Generic;
using System.Linq;

namespace CourseManagementSystem.Services.Implementations
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ICategoriesRepository repository;

        public CategoriesService(ICategoriesRepository repository)
        {
            this.repository = repository;
        }

        public List<CategoryDetailsViewModel> GetCategories()
        {
            return repository
                .GetCategories()
                .Select(category => new CategoryDetailsViewModel
                {
                    Id = category.Id,
                    Name = category.Name
                })
                .ToList();
        }

        public CategoryEditViewModel GetById(int id)
        {
            Category category = repository.GetById(id);

            if (category == null)
            {
                return null;
            }

            CategoryEditViewModel model = new CategoryEditViewModel
            {
                Id = category.Id,
                Name = category.Name
            };

            return model;
        }

        public void Insert(CategoryEditViewModel model)
        {
            Category category = new Category
            {
                Name = model.Name
            };

            repository.Insert(category);
        }

        public void Update(CategoryEditViewModel model)
        {
            Category category = new Category
            {
                Id = model.Id,
                Name = model.Name
            };

            repository.Update(category);
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }

        public bool Exists(int id)
        {
            Category category = repository.GetById(id);

            if (category == null)
            {
                return false;
            }

            return true;
        }
    }
}
