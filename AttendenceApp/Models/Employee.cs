using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AttendenceApp.Models
{
    public class Employee
    {
        [Key]
        [Column("employee_id")]
        public Guid employee_id { get; set; } = Guid.NewGuid();

        [Column("first_name")]
        public string first_name { get; set; } = string.Empty;

        [Column("last_name")]
        public string last_name { get; set; } = string.Empty;

        [Column("email")]
        public string email { get; set; } = string.Empty;

        [Column("password_hash")]
        public string password_hash { get; set; } = string.Empty;

        [Column("is_deleted")]
        public bool is_deleted { get; set; } = false;

        [Column("created_by")]
        public Guid? created_by { get; set; }

        [Column("updated_by")]
        public Guid? updated_by { get; set; }

        [Column("created_at")]
        public DateTime created_at { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime updated_at { get; set; } = DateTime.UtcNow;
    }
}
