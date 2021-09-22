using System.ComponentModel.DataAnnotations;

namespace CourseManagementSystem.Models.Ratings
{
    public class RatingEditViewModel
    {
        [Required]
        public int CourseId { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rate { get; set; }
    }
}
