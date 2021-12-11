﻿using DailyRoutines.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DailyRoutines.Infrastructure.Context
{
    public class DailyRoutinesDbContext : DbContext
    {
        public DailyRoutinesDbContext(DbContextOptions<DailyRoutinesDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserCategory> UserCategories { get; set; }
        public DbSet<Action> Actions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>()
                .HasKey(c => new { c.UserId, c.RoleId });

            base.OnModelCreating(modelBuilder);
        }
    }
}