using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AttendenceApp.Models
{
    public class Role
    {
        [Key]
        [Column("role_id")]
        public Guid role_id { get; set; } = Guid.NewGuid();

        [Column("role_name")]
        public string role_name { get; set; } = string.Empty;

        [Column("is_deleted")]
        public bool is_deleted { get; set; } = false;

        [Column("created_at")]
        public DateTime created_at { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime updated_at { get; set; } = DateTime.UtcNow;
    }
}
