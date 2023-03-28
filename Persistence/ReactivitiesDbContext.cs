using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class ReactivitiesDbContext:IdentityDbContext<AppUser>
    {
        public ReactivitiesDbContext(DbContextOptions<ReactivitiesDbContext> options) : base(options) { }

        public DbSet<Activity> Activities { get; set; }
    }
}
