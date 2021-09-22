using CourseManagementSystem.Models.Categories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CourseManagementSystem.Models.Courses
{
    public class CourseEditViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string Description{ get; set; }

        public DateTime PublishDate { get; set; }

        public List<SelectableCategoryViewModel> SelectedCategories { get; set; }
    }
}
