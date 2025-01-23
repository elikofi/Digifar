using Digifar.API.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Digifar.API.Data
{
    public class DigifarDbContext : IdentityDbContext<User>
    {
        public DigifarDbContext(DbContextOptions<DigifarDbContext> dbContextOptions)
            : base(dbContextOptions)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }
    }
}
