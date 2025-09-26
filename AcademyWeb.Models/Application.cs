using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcademyWeb.Models
{
    public enum ApplicationStatus
    {
        Pending = 0,
        Approved = 1,
        Declined = 2
    }

    public class Application
    {
        [Key]
        public Guid Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SrNo { get; set; }   // Auto-increment column

        [Required]
        public Guid StudentId { get; set; }

        [Required]
        public Guid CourseOfferingId { get; set; }

        [Required]
        [EnumDataType(typeof(ApplicationStatus))]
        public ApplicationStatus Status { get; set; } = ApplicationStatus.Pending;

        // Navigation properties
        [ForeignKey(nameof(StudentId))]
        public Student Student { get; set; } = null!;

        [ForeignKey(nameof(CourseOfferingId))]
        public CourseOffering CourseOffering { get; set; } = null!;
    }
}
