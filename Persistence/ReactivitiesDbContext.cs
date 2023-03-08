using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class ReactivitiesDbContext:DbContext
    {
        public ReactivitiesDbContext(DbContextOptions<ReactivitiesDbContext> options) : base(options) { }

        public DbSet<Activity> Activities { get; set; }
    }
}
