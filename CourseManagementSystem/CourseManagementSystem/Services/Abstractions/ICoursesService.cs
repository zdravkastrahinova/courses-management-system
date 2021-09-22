using CourseManagementSystem.Models.Courses;
using System.Collections.Generic;

namespace CourseManagementSystem.Services.Abstractions
{
    public interface ICoursesService
    {
        List<CourseDetailsViewModel> GetCourses();

        CourseDetailsViewModel GetCourse(int id);

        CourseEditViewModel GetById(int id);

        void Insert(CourseEditViewModel model);

        void Update(CourseEditViewModel model);

        void Delete(int id);

        bool Exists(int id);
    }
}
