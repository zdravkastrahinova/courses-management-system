using CourseManagementSystem.Entities;
using CourseManagementSystem.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CourseManagementSystem.Repositories.Implementations
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly CoursesManagementSystemContext context;
        private readonly DbSet<Category> dbSet;

        public CategoriesRepository(CoursesManagementSystemContext context)
        {
            this.context = context;
            dbSet = context.Set<Category>();
        }

        public List<Category> GetCategories()
        {
            return dbSet.ToList();
        }

        public Category GetById(int id)
        {
            return dbSet.Find(id);
        }

        public void Insert(Category category)
        {
            dbSet.Add(category);
            context.SaveChanges();
        }

        public void Update(Category category)
        {
            // 1. Find element from database
            Category element = GetById(category.Id);

            // 2. Detach state (stop tracking)
            if (element != null)
            {
                context.Entry<Category>(element).State = EntityState.Detached;
            }

            // 3. Change state
            context.Entry<Category>(category).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            Category category = dbSet.Include(c => c.CategoryCourses).FirstOrDefault(c => c.Id == id);

            context.CategoryCourses.RemoveRange(category.CategoryCourses);
            dbSet.Remove(category);

            context.SaveChanges();
        }
    }
}
