using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace AttendenceApp.Models
{
    public class audit
    {
        [Key]
        [Column("audit_id")]
        public Guid audit_id { get; set; } = Guid.NewGuid();

        [Required]
        [Column("table_name")]
        public string table_name { get; set; } = string.Empty;

        [Required]
        [Column("action")]
        public string action { get; set; } = string.Empty; // INSERT, UPDATE, DELETE

        [Required]
        [Column("record_id")]
        public Guid record_id { get; set; }

        [Column("detail")]
        public string? detail { get; set; }

        [Column("changed_data", TypeName = "jsonb")]
        public object? ChangedData { get; set; } // Use object to handle JSON

        [Column("changed_by")]
        public Guid? ChangedBy { get; set; }

        [Required]
        [Column("changed_at")]
        public DateTime ChangedAt { get; set; } = DateTime.UtcNow;
    }
}
