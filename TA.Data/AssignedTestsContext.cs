using Microsoft.EntityFrameworkCore;
using TA.Data.Contracts.Entities;

namespace TA.Data
{
    public class AssignedTestsContext : DbContext
    {
        public DbSet<AssignedTestReview> Review { get; set; }
        public DbSet<AssignedTests> AssignedTests { get; set; }
        public DbSet<StudentAnswer> StudentAnswers { get; set; }

        public AssignedTestsContext(DbContextOptions<AssignedTestsContext> options)
        : base(options)
        { }
    }
}
