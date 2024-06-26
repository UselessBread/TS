﻿using Microsoft.EntityFrameworkCore;
using TS.Data.Contracts.Entities;

namespace TS.Data
{
    public class TestsContext : DbContext
    {
        public TestsContext(DbContextOptions<TestsContext> options)
        : base(options)
        {
        }

        public TestsContext()
        {
        }

        public virtual DbSet<TestDescriptions> TestDescriptions { get; set; }
        public virtual DbSet<TestsContent> TestsContent { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}