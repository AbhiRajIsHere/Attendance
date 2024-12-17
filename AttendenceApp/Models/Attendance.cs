using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AttendenceApp.Models
{
    public class attendance
    {
        [Key]
        [Column("attendance_id")]
        public Guid attendance_id { get; set; } = Guid.NewGuid();

        [Column("employee_id")]
        public Guid employee_id { get; set; }

        [Column("event_date")]
        public DateTime event_date { get; set; }

        [Column("event_time")]
        public TimeSpan event_time { get; set; }

        [Column("event_type_id")]
        public Guid? event_type_id { get; set; }

        [Column("is_deleted")]
        public bool is_deleted { get; set; } = false;

        [Column("created_by")]
        public Guid? created_by { get; set; }

        [Column("created_at")]
        public DateTime created_at { get; set; } = DateTime.UtcNow;

        [NotMapped]
        public DateTime EventDateUtc
        {
            get => DateTime.SpecifyKind(event_date, DateTimeKind.Utc);
            set => event_date = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }

        [NotMapped]
        public DateTime CreatedAtUtc
        {
            get => DateTime.SpecifyKind(created_at, DateTimeKind.Utc);
            set => created_at = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }
    }
}
