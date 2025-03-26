using MedicalSearchingPlatform.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MedicalSearchingPlatform.Data.DataContext
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<MedicalFacility> MedicalFacilities { get; set; }
        public DbSet<MedicalService> MedicalServices { get; set; }
        public DbSet<MedicalFacilityService> MedicalFacilityServices { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Identity Configuration
            modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(l => new { l.LoginProvider, l.ProviderKey });
            modelBuilder.Entity<IdentityUserRole<string>>().HasKey(r => new { r.UserId, r.RoleId });
            modelBuilder.Entity<IdentityUserToken<string>>().HasKey(t => new { t.UserId, t.LoginProvider, t.Name });

            // User Configuration
            modelBuilder.Entity<User>()
                .Property(u => u.IsActive)
                .HasDefaultValue(true);

            // MedicalFacility Configuration
            modelBuilder.Entity<MedicalFacility>()
                .HasKey(f => f.FacilityId);
            modelBuilder.Entity<MedicalFacility>()
                .Property(f => f.CreatedAt)
                .HasDefaultValueSql("GETDATE()"); // Auto-set creation date

            // MedicalService Configuration
            modelBuilder.Entity<MedicalService>()
                .HasKey(s => s.ServiceId);

            // MedicalFacilityService Composite Key
            modelBuilder.Entity<MedicalFacilityService>()
                .HasKey(mfs => new { mfs.FacilityId, mfs.ServiceId });

            // Doctor Configuration
            modelBuilder.Entity<Doctor>()
                .HasKey(d => d.DoctorId);
            modelBuilder.Entity<Doctor>()
                .Property(d => d.CreatedAt)
                .HasDefaultValueSql("GETDATE()"); // Auto-set creation date
            modelBuilder.Entity<Doctor>()
                .Property(d => d.Fee)
                .HasColumnType("decimal(10,2)")
                .HasDefaultValue(0m);
            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.User)
                .WithOne()
                .HasForeignKey<Doctor>(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.Facility)
                .WithMany()
                .HasForeignKey(d => d.FacilityId)
                .OnDelete(DeleteBehavior.Restrict);

            // Review Configuration
            modelBuilder.Entity<Review>()
                .HasKey(r => r.ReviewId);
            modelBuilder.Entity<Review>()
                .Property(r => r.CreatedDate)
                .HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Patient)
                .WithMany()
                .HasForeignKey(r => r.PatientId)
                .OnDelete(DeleteBehavior.NoAction); // Prevents cascade cycles
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Doctor)
                .WithMany()
                .HasForeignKey(r => r.DoctorId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Review>()
                .HasOne(r => r.MedicalFacility)
                .WithMany()
                .HasForeignKey(r => r.FacilityId)
                .OnDelete(DeleteBehavior.NoAction);

            // Patient Configuration
            modelBuilder.Entity<Patient>()
                .HasKey(p => p.PatientId);
            modelBuilder.Entity<Patient>()
                .HasOne(p => p.User)
                .WithOne()
                .HasForeignKey<Patient>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Appointment Configuration
            modelBuilder.Entity<Appointment>()
                .HasKey(a => a.AppointmentId);
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany()
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany()
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Appointment Configuration
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);


            // Article Configuration
            modelBuilder.Entity<Article>()
                .HasKey(a => a.ArticleId);
            modelBuilder.Entity<Article>()
                .Property(a => a.Status)
                .HasDefaultValue("Draft");

            // Payment Configuration
            modelBuilder.Entity<Payment>()
                .HasKey(p => p.PaymentId);
            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasColumnType("decimal(10,2)");
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Patient)
                .WithMany()
                .HasForeignKey(p => p.PatientId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Appointment)
                .WithOne()
                .HasForeignKey<Payment>(p => p.AppointmentId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
