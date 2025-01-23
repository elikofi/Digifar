using Digifar.API.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Digifar.API.Data
{
    public class DigifarDbContext(DbContextOptions<DigifarDbContext> dbContextOptions) : IdentityDbContext<User>(dbContextOptions)
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }


    }

}
