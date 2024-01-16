using Microsoft.EntityFrameworkCore;

namespace TS.Data
{
    public class TestsContext : DbContext
    {
        public TestsContext(DbContextOptions<TestsContext> options)
        : base(options)
        {
        }

        public DbSet<TestDescriptions> TestDescriptions { get; set; }
        public DbSet<TestsContent> TestsContent { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}