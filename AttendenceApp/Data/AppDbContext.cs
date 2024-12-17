using AttendenceApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AttendenceApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeAddress> EmployeeAddresses { get; set; }
        public DbSet<EmployeePhoneNumber> EmployeePhoneNumbers { get; set; }
        public DbSet<EmployeeTaxDetail> EmployeeTaxDetails { get; set; }
        public DbSet<employeeaadhardetails> employeeaadhardetails { get; set; }
        public DbSet<attendance> Attendance { get; set; }
        public DbSet<audit> Audit { get; set; }
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Roles
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(r => r.role_id); // Primary Key
                entity.Property(r => r.role_name).IsRequired().HasMaxLength(50);
            });

            // UserRoles
            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(ur => ur.user_role_id); // Primary Key
                entity.HasOne<Role>()
                      .WithMany()
                      .HasForeignKey(ur => ur.role_id)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne<Employee>()
                      .WithMany()
                      .HasForeignKey(ur => ur.employee_id)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Employees
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.employee_id); // Primary Key
                entity.Property(e => e.first_name).IsRequired().HasMaxLength(50);
                entity.Property(e => e.last_name).IsRequired().HasMaxLength(50);
                entity.Property(e => e.email).IsRequired().HasMaxLength(100);
            });

            // EmployeeAddress
            modelBuilder.Entity<EmployeeAddress>(entity =>
            {
                entity.HasKey(ea => ea.address_id); // Primary Key
                entity.HasOne<Employee>()
                      .WithMany()
                      .HasForeignKey(ea => ea.employee_id)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // EmployeePhoneNumber
            modelBuilder.Entity<EmployeePhoneNumber>(entity =>
            {
                entity.HasKey(ep => ep.phone_id); // Primary Key
                entity.HasOne<Employee>()
                      .WithMany()
                      .HasForeignKey(ep => ep.employee_id)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // EmployeeTaxDetail
            modelBuilder.Entity<EmployeeTaxDetail>(entity =>
            {
                entity.HasKey(et => et.tax_id); // Primary Key
                entity.HasOne<Employee>()
                      .WithMany()
                      .HasForeignKey(et => et.employee_id)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // EmployeeAadharDetail
            modelBuilder.Entity<employeeaadhardetails>(entity =>
            {
                entity.HasKey(ead => ead.aadhar_id); // Primary Key
                entity.HasOne<Employee>()
                      .WithMany()
                      .HasForeignKey(ead => ead.employee_id)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Attendance
            modelBuilder.Entity<attendance>(entity =>
            {
                entity.HasKey(a => a.attendance_id); // Primary Key
                entity.Property(a => a.event_date).IsRequired();
                entity.Property(a => a.event_time).IsRequired();
                entity.Property(a => a.event_type_id).IsRequired().HasMaxLength(50);

                entity.HasOne<Employee>()
                      .WithMany()
                      .HasForeignKey(a => a.employee_id)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Audit
            modelBuilder.Entity<audit>(entity =>
            {
                entity.HasKey(a => a.audit_id); // Primary Key
                entity.Property(a => a.table_name).IsRequired().HasMaxLength(100);
                entity.Property(a => a.action).IsRequired().HasMaxLength(50); // Insert, Update, Delete
                entity.Property(a => a.detail).HasMaxLength(500);
            });

            // EventType
            modelBuilder.Entity<EventType>(entity =>
            {
                entity.HasKey(et => et.event_type_id); // Primary Key
                entity.Property(et => et.event_type_name).IsRequired().HasMaxLength(50);
            });
        }
    }
}
