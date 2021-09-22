using CourseManagementSystem.Models.Categories;
using System;
using System.Collections.Generic;

namespace CourseManagementSystem.Models.Courses
{
    public class CourseDetailsViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime PublishDate { get; set; }

        public double Rating { get; set; }

        public List<CategoryDetailsViewModel> Categories { get; set; }
    }
}
