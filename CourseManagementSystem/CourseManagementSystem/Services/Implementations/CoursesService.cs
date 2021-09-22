using CourseManagementSystem.Entities;
using CourseManagementSystem.Models.Categories;
using CourseManagementSystem.Models.Courses;
using CourseManagementSystem.Repositories.Abstractions;
using CourseManagementSystem.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseManagementSystem.Services.Implementations
{
    public class CoursesService : ICoursesService
    {
        private readonly ICoursesRepository repository;
        private readonly ICategoriesRepository categoriesRepository;

        public CoursesService(ICoursesRepository repository, ICategoriesRepository categoriesRepository)
        {
            this.repository = repository;
            this.categoriesRepository = categoriesRepository;
        }

        public List<CourseDetailsViewModel> GetCourses()
        {
            return repository
                .GetCourses()
                .Include(course => course.Ratings)
                .Select(course => new CourseDetailsViewModel
                {
                    Id = course.Id,
                    Title = course.Title,
                    Description = course.Description,
                    PublishDate = course.PublishDate,
                    Rating = course.Ratings.Count == 0 ? 0 : course.Ratings.Select(rating => rating.Rate).Average()
                })
                .ToList();
        }

        public CourseDetailsViewModel GetCourse(int id)
        {
            Course course = repository
                .GetCourses()
                .Include(course => course.Ratings)
                .Include(course => course.CategoryCourses)
                .ThenInclude(cc => cc.Category)
                .FirstOrDefault(course => course.Id == id);

            if (course == null)
            {
                return null;
            }

            CourseDetailsViewModel model = new CourseDetailsViewModel
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                PublishDate = course.PublishDate,
                Rating = course.Ratings.Count == 0 ? 0 : course.Ratings.Select(rating => rating.Rate).Average(),
                Categories = course.CategoryCourses
                    .Select(cc => new CategoryDetailsViewModel
                    {
                        Id = cc.Category.Id,
                        Name = cc.Category.Name
                    })
                    .ToList()

            };

            return model;
        }

        public CourseEditViewModel GetById(int id)
        {
            Course course = repository
                .GetCourses()
                .Include(course => course.CategoryCourses)
                .ThenInclude(cc => cc.Category)
                .FirstOrDefault(course => course.Id == id);

            if (course == null)
            {
                return null;
            }

            CourseEditViewModel model = new CourseEditViewModel
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                PublishDate = course.PublishDate,
                SelectedCategories = new List<SelectableCategoryViewModel>()
            };

            List<Category> categories = categoriesRepository.GetCategories();

            model.SelectedCategories = categories
                .Select(category => new SelectableCategoryViewModel
                {
                    Id = category.Id,
                    Name = category.Name,
                    IsSelected = course.CategoryCourses.Select(cc => cc.CategoryId).Contains(category.Id)
                })
                .ToList();

            return model;
        }

        public void Insert(CourseEditViewModel model)
        {
            // 1. Create course with simple properties
            Course course = new Course
            {
                Title = model.Title,
                Description = model.Description,
                PublishDate = DateTime.Now
            };

            repository.Insert(course);

            // 2. After course creation, add related properties
            Course newlyCreatedCourse = repository.GetCourses().FirstOrDefault(c => c.Title == model.Title);
            newlyCreatedCourse.CategoryCourses = model.SelectedCategories
                .Where(category => category.IsSelected)
                .Select(category => new CategoryCourse
                {
                    CourseId = newlyCreatedCourse.Id,
                    CategoryId = category.Id
                })
                .ToList();

            repository.Update(newlyCreatedCourse);
        }

        public void Update(CourseEditViewModel model)
        {
            Course course = new Course
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                PublishDate = model.PublishDate,
                CategoryCourses = model.SelectedCategories
                    .Where(category => category.IsSelected)
                    .Select(category => new CategoryCourse
                    {
                        CourseId = model.Id,
                        CategoryId = category.Id
                    })
                    .ToList()
            };

            repository.Update(course);
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }

        public bool Exists(int id)
        {
            Course course = repository.GetById(id);

            if (course == null)
            {
                return false;
            }

            return true;
        }
    }
}
