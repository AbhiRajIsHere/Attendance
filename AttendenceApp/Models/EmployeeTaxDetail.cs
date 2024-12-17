using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AttendenceApp.Models
{
    public class EmployeeTaxDetail
    {
        [Key]
        [Column("tax_id")]
        public Guid tax_id { get; set; } = Guid.NewGuid();

        [Column("employee_id")]
        public Guid employee_id { get; set; }

        [Column("tax_identification_number")]
        public string tax_identification_number { get; set; } = string.Empty;

        [Column("pan_number")]
        public string? pan_number { get; set; }

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
