using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseManagementSystem.Entities
{
    public class CategoryCourse
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
    }
}
