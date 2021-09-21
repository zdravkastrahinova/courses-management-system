using System.ComponentModel.DataAnnotations;

namespace CourseManagementSystem.Models.Categories
{
    public class CategoryEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [MaxLength(250)]
        public string Name { get; set; }
    }
}
