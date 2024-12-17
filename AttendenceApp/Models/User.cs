using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AttendenceApp.Models
{
    public class User
    {
        [Key]
        [Column("employee_id")]
        public Guid employee_id { get; set; } = Guid.NewGuid();

        [Required]
        [Column("username")]
        public string username { get; set; } = string.Empty;

        [Required]
        [Column("password")]
        public string password { get; set; } = string.Empty;

        [Required]
        [Column("email")]
        public string email { get; set; } = string.Empty;

        [Column("is_active")]
        public bool isActive { get; set; } = true;

        [Column("created_at")]
        public DateTime createdAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime updatedAt { get; set; } = DateTime.UtcNow;
    }
}
