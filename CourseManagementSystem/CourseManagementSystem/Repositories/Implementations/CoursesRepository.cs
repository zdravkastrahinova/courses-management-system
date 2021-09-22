using CourseManagementSystem.Entities;
using CourseManagementSystem.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CourseManagementSystem.Repositories.Implementations
{
    public class CoursesRepository : ICoursesRepository
    {
        private readonly CoursesManagementSystemContext context;
        private readonly DbSet<Course> dbSet;

        public CoursesRepository(CoursesManagementSystemContext context)
        {
            this.context = context;
            dbSet = context.Set<Course>();
        }

        public IQueryable<Course> GetCourses()
        {
            return dbSet;
        }

        public Course GetById(int id)
        {
            return dbSet.Find(id);
        }

        public void Insert(Course course)
        {
            dbSet.Add(course);
            context.SaveChanges();
        }

        public void Update(Course course)
        {
            // 1. Get course from database, including related data
            Course element = dbSet.Include(c => c.CategoryCourses).FirstOrDefault(c => c.Id == course.Id);

            // 2. Modify state of course entity
            if (element != null)
            {
                context.Entry<Course>(element).State = EntityState.Detached;
            }

            context.Entry<Course>(course).State = EntityState.Modified;

            // 3. Remove all related categories from database
            context.CategoryCourses.RemoveRange(element.CategoryCourses);
            context.SaveChanges();

            // 4. Insert newly assigned categories
            context.CategoryCourses.AddRange(course.CategoryCourses);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            Course course = dbSet
                .Include(c => c.Ratings)
                .Include(c => c.CategoryCourses)
                .FirstOrDefault(c => c.Id == id);

            context.Ratings.RemoveRange(course.Ratings);
            context.CategoryCourses.RemoveRange(course.CategoryCourses);

            dbSet.Remove(course);

            context.SaveChanges();
        }
    }
}
