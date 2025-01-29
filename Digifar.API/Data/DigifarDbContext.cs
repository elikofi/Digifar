using Digifar.API.Models.DTOs;
using Digifar.API.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Digifar.API.Data
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
            modelBuilder.Entity<OtpRecord>()
                .HasIndex(o => o.PhoneNumber)
                .IsUnique();
        }

        public DbSet<OtpRecord> Otps {  get; set; }
    }
}
