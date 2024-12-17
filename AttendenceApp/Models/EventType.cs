using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AttendenceApp.Models
{
    public class EventType
    {
        [Key]
        [Column("event_type_id")]
        public Guid event_type_id { get; set; } = Guid.NewGuid();

        [Column("event_type_name")]
        public string event_type_name { get; set; } = string.Empty;

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
