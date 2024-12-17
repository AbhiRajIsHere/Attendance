using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AttendenceApp.Models
{
    public class EmployeeAddress
    {
        [Key]
        [Column("address_id")]
        public Guid address_id { get; set; } = Guid.NewGuid();

        [Column("employee_id")]
        public Guid employee_id { get; set; }

        [Column("address_line1")]
        public string address_line1 { get; set; } = string.Empty;

        [Column("address_line2")]
        public string? address_line2 { get; set; }

        [Column("address_line3")]
        public string? address_line3 { get; set; }

        [Column("address_line4")]
        public string? address_line4 { get; set; }

        [Column("city")]
        public string city { get; set; } = string.Empty;

        [Column("state")]
        public string state { get; set; } = string.Empty;

        [Column("postal_code")]
        public string postal_code { get; set; } = string.Empty;

        [Column("country")]
        public string country { get; set; } = string.Empty;

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
