using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcademyWeb.Models
{
    public class Enrollment
    {
        [Key]
        public Guid Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SrNo { get; set; }   // Auto-increment column

        [Required(ErrorMessage = "StudentId is required")]
        public Guid StudentId { get; set; }

        [Required(ErrorMessage = "CourseOfferingId is required")]
        public Guid CourseOfferingId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime EnrolledOn { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey(nameof(StudentId))]
        public Student Student { get; set; } = null!;

        [ForeignKey(nameof(CourseOfferingId))]
        public CourseOffering CourseOffering { get; set; } = null!;
    }
}
