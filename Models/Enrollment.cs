using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PZ3.Models
{
    public class Enrollment
    {
        [Key]
        public int EnrollmentId { get; set; }

        public int StudentId { get; set; }
        public int CourseId { get; set; }

        [Column(TypeName = "date")]
        public DateTime EnrollmentDate { get; set; }

        [StringLength(20)]
        public string Status { get; set; } = "Active";

        public virtual Student? Student { get; set; }
        public virtual Course? Course { get; set; }
    }
}