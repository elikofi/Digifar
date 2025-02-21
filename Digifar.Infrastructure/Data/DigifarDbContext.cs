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

            // Wallet & User Relationship
            modelBuilder.Entity<Wallet>()
                .HasOne(w => w.User)
                .WithMany(u => u.Wallets)
                .HasForeignKey(w => w.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Agent & User Relationship
            modelBuilder.Entity<Agent>()
                .HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Merchant & User Relationship
            modelBuilder.Entity<Merchant>()
                .HasOne(m => m.User)
                .WithMany()
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Business & Merchants Relationship
            modelBuilder.Entity<Business>()
                .HasMany(b => b.Merchants)
                .WithMany(m => m.Businesses);

            // Agent & Business Relationship
            modelBuilder.Entity<Agent>()
                .HasOne(a => a.Business)
                .WithMany()
                .HasForeignKey(a => a.BusinessId)
                .OnDelete(DeleteBehavior.SetNull);

            // User - KYC Verification Relationship (One-to-One)
            modelBuilder.Entity<KycVerification>()
                .HasOne(k => k.User)
                .WithMany()
                .HasForeignKey(k => k.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<OtpRecord> Otps { get; set; }
        public DbSet<EmailVerificationCode> VerificationCodes { get; set; }
        public DbSet<VerifiedEmail> VerifiedEmails { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Agent> Agents { get; set; }
        public DbSet<Business> Businesses { get; set; }
        public DbSet<Merchant> Merchants { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<KycVerification> KycVerifications { get; set; }
    }
}
