using MedicalSearchingPlatform.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalSearchingPlatform.Data.DataContext
{
    public class ApplicationDbContext : DbContext
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
            modelBuilder.Entity<User>()
                .HasKey(u => u.UserId);
            modelBuilder.Entity<User>()
                .Property(u => u.UserId)
                .HasMaxLength(50);
            modelBuilder.Entity<User>()
                .Property(u => u.FullName)
                .IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<User>()
                .Property(u => u.Phone)
                .HasMaxLength(15);
            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .IsRequired()
                .HasMaxLength(10);
            modelBuilder.Entity<User>()
                .Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(255);
            modelBuilder.Entity<User>()
                .Property(u => u.IsActive)
                .HasDefaultValue(true);

            modelBuilder.Entity<MedicalFacility>()
                .HasKey(f => f.FacilityId);
            modelBuilder.Entity<MedicalFacility>()
                .Property(f => f.FacilityId)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<MedicalFacility>()
                .Property(f => f.FacilityName)
                .IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<MedicalFacility>()
                .Property(f => f.Address)
                .IsRequired()
                .HasMaxLength(255);
            modelBuilder.Entity<MedicalFacility>()
                .Property(f => f.PhoneNumber)
                .HasMaxLength(15);

            modelBuilder.Entity<MedicalService>()
                .HasKey(s => s.ServiceId);
            modelBuilder.Entity<MedicalService>()
                .Property(s => s.ServiceId)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<MedicalService>()
                .Property(s => s.ServiceName)
                .IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<MedicalService>()
                .Property(s => s.Description)
                .HasMaxLength(500);
            modelBuilder.Entity<MedicalService>()
                .Property(s => s.Status)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<MedicalFacilityService>()
                .HasKey(mfs => new { mfs.FacilityId, mfs.ServiceId });

            modelBuilder.Entity<Doctor>()
                .HasKey(d => d.DoctorId);
            modelBuilder.Entity<Doctor>()
                .Property(d => d.DoctorId)
                .IsRequired()
                .HasMaxLength(50);
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
            modelBuilder.Entity<Doctor>()
                .Property(d => d.Specialization)
                .IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<Doctor>()
                .Property(d => d.Qualifications)
                .HasMaxLength(255);
            modelBuilder.Entity<Doctor>()
                .Property(d => d.Experience)
                .HasMaxLength(500);

            modelBuilder.Entity<Patient>()
                .HasKey(p => p.PatientId);
            modelBuilder.Entity<Patient>()
                .Property(p => p.PatientId)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<Patient>()
                .HasOne(p => p.User)
                .WithOne()
                .HasForeignKey<Patient>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Patient>()
                .Property(p => p.MedicalHistory)
                .HasMaxLength(500);
            modelBuilder.Entity<Patient>()
                .Property(p => p.ConditionsToNote)
                .HasMaxLength(500);

            modelBuilder.Entity<Appointment>()
                .HasKey(a => a.AppointmentId);
            modelBuilder.Entity<Appointment>()
                .Property(a => a.AppointmentId)
                .IsRequired()
                .HasMaxLength(50);
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
            modelBuilder.Entity<Appointment>()
                .Property(a => a.AppointmentInfo)
                .HasMaxLength(500);
            modelBuilder.Entity<Appointment>()
                .Property(a => a.AppointmentDate)
                .IsRequired();
            modelBuilder.Entity<Appointment>()
                .Property(a => a.Status)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Article>()
                .HasKey(a => a.ArticleId);
            modelBuilder.Entity<Article>()
                .Property(a => a.ArticleId)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<Article>()
                .Property(a => a.Title)
                .IsRequired()
                .HasMaxLength(255);
            modelBuilder.Entity<Article>()
                .Property(a => a.Content)
                .IsRequired();
            modelBuilder.Entity<Article>()
                .Property(a => a.Category)
                .HasMaxLength(100);
            modelBuilder.Entity<Article>()
                .Property(a => a.Status)
                .IsRequired()
                .HasMaxLength(50)
                .HasDefaultValue("Draft");

            modelBuilder.Entity<Review>()
                .HasKey(r => r.ReviewId);
            modelBuilder.Entity<Review>()
                .Property(r => r.ReviewId)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<Review>()
                .Property(r => r.Rating)
                .IsRequired()
                .HasDefaultValue(5);
            modelBuilder.Entity<Review>()
                .Property(r => r.Comment)
                .HasMaxLength(500);
            modelBuilder.Entity<Review>()
                .Property(r => r.CreatedDate)
                .IsRequired();

            modelBuilder.Entity<Payment>()
                .HasKey(p => p.PaymentId);
            modelBuilder.Entity<Payment>()
                .Property(p => p.PaymentId)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .IsRequired()
                .HasColumnType("decimal(10,2)");
            modelBuilder.Entity<Payment>()
                .Property(p => p.PaymentDate)
                .IsRequired();
            modelBuilder.Entity<Payment>()
                .Property(p => p.PaymentMethod)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<Payment>()
                .Property(p => p.TransactionId)
                .HasMaxLength(100);
            modelBuilder.Entity<Payment>()
                .Property(p => p.Status)
                .IsRequired()
                .HasMaxLength(50);
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
