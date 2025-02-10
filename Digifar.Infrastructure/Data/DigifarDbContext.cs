using Digifar.Application.Authentication.Common;
using Digifar.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Digifar.Infrastructure.Data
{
    public class DigifarDbContext : IdentityDbContext<User>
    {
        public DigifarDbContext(DbContextOptions<DigifarDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<OtpRecord>()
                .HasIndex(o => o.PhoneNumber)
                .IsUnique();

            modelBuilder.Entity<EmailVerificationCode>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Email).IsRequired();
                entity.Property(e => e.Code).IsRequired();
                entity.HasIndex(e => new { e.Email, e.Code });
            });

            modelBuilder.Entity<VerifiedEmail>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Email).IsRequired();
                entity.HasIndex(e => e.Email).IsUnique();
            });
        }

        public DbSet<OtpRecord> Otps { get; set; }
        public DbSet<EmailVerificationCode> VerificationCodes { get; set; }
        public DbSet<VerifiedEmail> VerifiedEmails { get; set; }
    }
}
