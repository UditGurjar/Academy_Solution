using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyWeb.Models
{
    public class Role
    {
        public Guid Id { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SrNo { get; set; }   // Auto-increment column
        public string RoleName { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public ICollection<User> Users { get; set; } = new List<User>();

    }
}
