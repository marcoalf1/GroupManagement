using Microsoft.EntityFrameworkCore;
using PlayBall.GroupManagement.Data.Configurations;
using PlayBall.GroupManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlayBall.GroupManagement.Data
{
    public class GroupManagementDbContext : DbContext
    {
        public DbSet<GroupEntity> Groups { get; set; }

        public GroupManagementDbContext(DbContextOptions<GroupManagementDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");

            // Configure entities ...
            modelBuilder.ApplyConfiguration(new GroupEntityConfiguration());
        }
    }
}
