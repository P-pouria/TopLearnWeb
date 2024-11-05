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

        #region User

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        #endregion

        #region Wallet

        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<WalletType> WalletTypes { get; set; }

        #endregion

        #region Permission
        public DbSet<Permission> Permission { get; set; }
        public DbSet<RolePermission> RolePermission { get; set; }

        #endregion

        #region Course

        public DbSet<CourseGroup> CourseGroups { get; set; }
        public DbSet<Course> Coueses { get; set; }
        public DbSet<CourseEpisode> CourseEpisodes { get; set; }
        public DbSet<CourseLevel> CourseLevels { get; set; }
        public DbSet<CourseStatus> CourseStatuses { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Course

            modelBuilder.Entity<Course>()
                .HasOne(c => c.CourseGroup)
                .WithMany(g => g.Courses)
                .HasForeignKey(c => c.GroupId)
                .OnDelete(DeleteBehavior.Restrict); 
            
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Group)
                .WithMany(g => g.SubGroup)
                .HasForeignKey(c => c.SubGroup)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Course>()
                .HasOne(c=>c.CourseStatus)
                .WithMany(c=>c.Courses)
                .HasForeignKey(c => c.StatusId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Course>()
                .HasOne(c=>c.CourseLevel)
                .WithMany(c=>c.Courses)
                .HasForeignKey(c=>c.LevelId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Course>()
                .HasMany(c => c.CourseEpisodes)          
                .WithOne(e => e.Course)                  
                .HasForeignKey(e => e.CourseId)         
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<Course>()
                .HasOne(c=>c.User)
                .WithMany(c=>c.Courses)
                .HasForeignKey(c=>c.TeacherId)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region CourseGroup

            modelBuilder.Entity<CourseGroup>(entity =>
            {
                entity.HasOne(g => g.ParentGroup)
                      .WithMany(g => g.CourseGroups)
                      .HasForeignKey(g => g.ParentId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<CourseGroup>()
                .HasQueryFilter(g => !g.IsDelete);

            #endregion

            #region Wallet

            modelBuilder.Entity<Wallet>(entity =>
            {
                entity.HasOne(w => w.WalletType)
                      .WithMany(wt => wt.Wallets)
                      .HasForeignKey(w => w.TypeId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            #endregion

            #region User

            modelBuilder.Entity<User>()
                .HasQueryFilter(u => !u.IsDelete);

            #endregion

            #region Role

            modelBuilder.Entity<Role>()
                .HasQueryFilter(r => !r.IsDelete);

            #endregion
        }
    }
}
