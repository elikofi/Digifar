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
        }

        public DbSet<OtpRecord> Otps { get; set; }
    }
}
