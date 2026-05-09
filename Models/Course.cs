using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PZ3.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Subject { get; set; } = string.Empty;

        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "date")]
        public DateOnly StartDate { get; set; }

        [StringLength(100)]
        public string TeacherName { get; set; } = string.Empty;

        public virtual ICollection<Enrollment>? Enrollments { get; set; }
    }
}