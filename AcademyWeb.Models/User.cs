using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AcademyWeb.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SrNo { get; set; }   // Auto-increment column

        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, ErrorMessage = "Username cannot exceed 50 characters")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "PasswordHash is required")]
        [StringLength(500, ErrorMessage = "Password hash cannot exceed 500 characters")]
        [JsonIgnore]
        public string PasswordHash { get; set; } = string.Empty;

        [Required(ErrorMessage = "RoleId is required")]
        public Guid RoleId { get; set; }

        [ForeignKey(nameof(RoleId))]
        [JsonIgnore] // prevents binding during POST
        public Role Role { get; set; } = null!;

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
