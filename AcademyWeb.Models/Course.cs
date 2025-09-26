using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcademyWeb.Models
{
    public class Course
    {
        [Key]
        public Guid Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SrNo { get; set; }   // Auto-increment column

        [Required(ErrorMessage = "Course Name is required")]
        [StringLength(100, ErrorMessage = "Course Name cannot exceed 100 characters")]
        public string CourseName { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string CourseDescription { get; set; } = string.Empty;

        [Required(ErrorMessage = "Accreditation is required")]
        public AccreditationType Accreditation { get; set; }  // Enum Dropdown


        [StringLength(50, ErrorMessage = "SAQA ID cannot exceed 50 characters")]
        public string SAQAId { get; set; } = string.Empty; // free text alphanumeric

        [Required(ErrorMessage = "Level is required")]
        public int Level { get; set; }  // integer

        [StringLength(20, ErrorMessage = "Number of Credits cannot exceed 20 characters")]
        public string NumberOfCredits { get; set; } = string.Empty; // free text alphanumeric

        public bool IsActive { get; set; } = true; // Course is Active
    }
    public enum AccreditationType
    {
        SAQA,
        SAIOSH
    }
}
