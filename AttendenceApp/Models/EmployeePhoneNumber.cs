using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AttendenceApp.Models
{
    public class EmployeePhoneNumber
    {
        [Key]
        [Column("phone_id")]
        public Guid phone_id { get; set; } = Guid.NewGuid();

        [Column("employee_id")]
        public Guid employee_id { get; set; }

        [Column("phone_number")]
        public string phone_number { get; set; } = string.Empty;

        [Column("phone_type")]
        public string phone_type { get; set; } = string.Empty;

        [Column("is_primary")]
        public bool is_primary { get; set; } = false;

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
