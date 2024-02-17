using Microsoft.EntityFrameworkCore;
using TA.Data.Contracts.Entities;

namespace TA.Data
{
    public class AssignedTestsContext : DbContext
    {
        public virtual DbSet<AssignedTestReview> Review { get; set; }
        public virtual DbSet<AssignedTests> AssignedTests { get; set; }
        public virtual DbSet<StudentAnswer> StudentAnswers { get; set; }

        public AssignedTestsContext(DbContextOptions<AssignedTestsContext> options)
        : base(options)
        { }

        public AssignedTestsContext() { }
    }
}
