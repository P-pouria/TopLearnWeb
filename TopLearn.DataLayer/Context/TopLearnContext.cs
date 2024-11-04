using Microsoft.EntityFrameworkCore;
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
        public DbSet<CourseLevel> CourseLevels { get; set; }
        public DbSet<CourseStatus> CourseStatuses { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseEpisode> CourseEpisodes { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // تنظیمات Fluent API برای Course
            modelBuilder.Entity<Course>(entity =>
            {
                // رابطه اصلی با CourseGroup برای GroupId
                entity.HasOne(c => c.CourseGroup)
                      .WithMany(g => g.Courses)
                      .HasForeignKey(c => c.GroupId)
                      .OnDelete(DeleteBehavior.Restrict);

                // رابطه ثانویه با SubCourseGroup برای SubGroupId
                entity.HasOne(c => c.SubCourseGroup)
                      .WithMany(g => g.SubGroupCourses)
                      .HasForeignKey(c => c.SubGroupId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // تنظیمات Fluent API برای CourseGroup
            modelBuilder.Entity<CourseGroup>(entity =>
            {
                // رابطه خودارجاعی برای ParentGroup
                entity.HasOne(g => g.ParentGroup)
                      .WithMany(g => g.CourseGroups)
                      .HasForeignKey(g => g.ParentId)
                      .OnDelete(DeleteBehavior.Restrict);

                // فیلتر حذف نرم برای CourseGroup
                entity.HasQueryFilter(g => !g.IsDelete);

                // رابطه CourseGroup با Course (گروه اصلی و زیرگروه‌ها در Course)
                entity.HasMany(g => g.Courses)
                      .WithOne(c => c.CourseGroup)
                      .HasForeignKey(c => c.GroupId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(g => g.SubGroupCourses)
                      .WithOne(c => c.SubCourseGroup)
                      .HasForeignKey(c => c.SubGroupId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // تنظیمات Fluent API برای Wallet
            modelBuilder.Entity<Wallet>(entity =>
            {
                entity.HasOne(w => w.WalletType)
                      .WithMany(wt => wt.Wallets)
                      .HasForeignKey(w => w.TypeId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // فیلترهای حذف نرم (soft delete) برای User و Role
            modelBuilder.Entity<User>()
                .HasQueryFilter(u => !u.IsDelete);

            modelBuilder.Entity<Role>()
                .HasQueryFilter(r => !r.IsDelete);

            base.OnModelCreating(modelBuilder);
        }
    }
}
