using CourseManagementSystem.Entities;
using System.Linq;

namespace CourseManagementSystem.Repositories.Abstractions
{
    public interface ICoursesRepository
    {
        IQueryable<Course> GetCourses();
        Course GetById(int id);
        void Insert(Course course);
        void Update(Course course);
        void Delete(int id);
    }
}
