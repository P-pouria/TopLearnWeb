﻿using Microsoft.EntityFrameworkCore;
using TopLearn.DataLayer.Entities.Course;
using TopLearn.DataLayer.Entities.Permissions;
using TopLearn.DataLayer.Entities.User;
using TopLearn.DataLayer.Entities.Wallet;

namespace TopLearn.DataLayer.Context
{
    public class TopLearnContext : DbContext
    {
        public TopLearnContext(DbContextOptions<TopLearnContext> options) : base(options)
        {
        }

        #region DbSets

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<WalletType> WalletTypes { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Permission> Permission { get; set; }
        public DbSet<RolePermission> RolePermission { get; set; }
        public DbSet<CourseGroup> CourseGroups { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<CourseGroup>(entity =>
            {
                entity.HasOne(g => g.ParentGroup)
                      .WithMany(g => g.CourseGroups)
                      .HasForeignKey(g => g.ParentId)
                      .OnDelete(DeleteBehavior.Restrict);  
            });

            
            modelBuilder.Entity<Wallet>(entity =>
            {
                entity.HasOne(w => w.WalletType)
                      .WithMany(wt => wt.Wallets)
                      .HasForeignKey(w => w.TypeId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<User>()
                .HasQueryFilter(u => !u.IsDelete);

            modelBuilder.Entity<Role>()
                .HasQueryFilter(r => !r.IsDelete);

            modelBuilder.Entity<CourseGroup>()
                .HasQueryFilter(g => !g.IsDelete);
        }
    }
}
