using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcademyWeb.Models
{
    public class CourseOffering
    {
        [Key]
        public Guid Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SrNo { get; set; }   // Auto-increment column

        [Required(ErrorMessage = "CourseId is required")]
        public Guid CourseId { get; set; }

        // Facilitators (list of existing users with role Admin/Facilitator)
        public ICollection<Guid> FacilitatorIds { get; set; } = new List<Guid>();

        [Required(ErrorMessage = "Delivery Mode is required")]
        [StringLength(50)]
        public string DeliveryMode { get; set; } = string.Empty;     //inperson,online

        // Spread (From – To date)
        [Required(ErrorMessage = "Start date is required")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(CourseOffering), nameof(ValidateEndDate))]
        public DateTime EndDate { get; set; }

        // Duration with unit (Days, Weeks, Months, Years)
        [Required(ErrorMessage = "Duration value is required")]
        public int DurationValue { get; set; }

        [Required(ErrorMessage = "Duration unit is required")]
        [StringLength(20)]
        public string DurationUnit { get; set; } = "Days"; // Days/Weeks/Months/Years

        [Required(ErrorMessage = "Commencement date is required")]
        [DataType(DataType.Date)]
        public DateTime CommencementDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? CompletionDate { get; set; }

        [StringLength(200)]
        public string Venue { get; set; } = string.Empty;

        [Required]
        public OfferingStatus Status { get; set; } = OfferingStatus.Scheduled;

        [StringLength(1000)]
        public string Notes { get; set; } = string.Empty;

        // For file uploads (store file path or name)
        [StringLength(500)]
        public string? FilePath { get; set; }

        // Navigation properties
        [ForeignKey(nameof(CourseId))]
        public Course Course { get; set; } = null!;

        public ICollection<Application> Applications { get; set; } = new List<Application>();
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

        // Custom validation to ensure EndDate >= StartDate
        public static ValidationResult? ValidateEndDate(DateTime endDate, ValidationContext context)
        {
            var instance = (CourseOffering)context.ObjectInstance;
            if (endDate < instance.StartDate)
            {
                return new ValidationResult("EndDate cannot be earlier than StartDate");
            }
            return ValidationResult.Success;
        }
    }

    public enum OfferingStatus
    {
        Scheduled,
        Open,
        Closed,
        Cancelled
    }
}
