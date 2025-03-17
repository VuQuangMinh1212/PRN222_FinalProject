using MedicalSearchingPlatform.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalSearchingPlatform.Data.DataContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.UserId);

                entity.Property(u => u.FullName)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(u => u.Email)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(u => u.Phone)
                      .HasMaxLength(15);

                entity.Property(u => u.Role)
                      .IsRequired()
                      .HasMaxLength(10);

                entity.Property(u => u.Password)
                      .IsRequired()
                      .HasMaxLength(255);

                entity.Property(u => u.IsActive)
                      .HasDefaultValue(true);
            });
        }
    }
}
