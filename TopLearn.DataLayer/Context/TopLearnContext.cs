﻿using Microsoft.EntityFrameworkCore;
using TopLearn.DataLayer.Entities.Course;
using TopLearn.DataLayer.Entities.Order;
using TopLearn.DataLayer.Entities.Permissions;
using TopLearn.DataLayer.Entities.Question;
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
        public DbSet<UserDiscountCode> UserDiscountCodes { get; set; }

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
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseEpisode> CourseEpisodes { get; set; }
        public DbSet<CourseLevel> CourseLevels { get; set; }
        public DbSet<CourseStatus> CourseStatuses { get; set; }
        public DbSet<UserCourse> UserCourses { get; set; }
        public DbSet<CourseComment> CourseComments { get; set; }
        public DbSet<CourseVote> CourseVotes { get; set; }

        #endregion

        #region Order

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Discount> Discounts { get; set; }

        #endregion

        #region Question

        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }


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
                .HasOne(c => c.CourseStatus)
                .WithMany(c => c.Courses)
                .HasForeignKey(c => c.StatusId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Course>()
                .HasOne(c => c.CourseLevel)
                .WithMany(c => c.Courses)
                .HasForeignKey(c => c.LevelId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Course>()
                .HasMany(c => c.CourseEpisodes)
                .WithOne(e => e.Course)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Course>()
                .HasOne(c => c.User)
                .WithMany(c => c.Courses)
                .HasForeignKey(c => c.TeacherId)
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

            #region Order

            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(o => o.Orders)
                .HasForeignKey(o => o.UserId);


            #endregion

            #region OrderDetail

            modelBuilder.Entity<OrderDetail>()
                .HasOne(o => o.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(o => o.OrderId);


            modelBuilder.Entity<OrderDetail>()
                .HasOne(o => o.Course)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(o => o.CourseId);


            #endregion

            #region UserCourse

            modelBuilder.Entity<UserCourse>()
                .HasOne(uc => uc.Course)
                .WithMany(uc => uc.UserCourses)
                .HasForeignKey(uc => uc.CourseId);

            modelBuilder.Entity<UserCourse>()
               .HasOne(uc => uc.User)
               .WithMany(uc => uc.UserCourses)
               .HasForeignKey(uc => uc.UserId);

            #endregion

            #region UserDiscountCode

            modelBuilder.Entity<UserDiscountCode>()
                .HasOne(u => u.User)
                .WithMany(u => u.UserDiscountCodes)
                .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<UserDiscountCode>()
                .HasOne(u => u.Discount)
                .WithMany(u => u.UserDiscountCodes)
                .HasForeignKey(u => u.DiscountId);

            #endregion

            #region CourseComment

            modelBuilder.Entity<CourseComment>()
                .HasOne(c => c.User)
                .WithMany(c => c.CourseComments)
                .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<CourseComment>()
                .HasOne(c => c.Course)
                .WithMany(c => c.CourseComments)
                .HasForeignKey(c => c.CourseId);


            #endregion

            #region CourseVote

            modelBuilder.Entity<CourseVote>()
                .HasOne(c => c.User)
                .WithMany(c => c.CourseVotes)
                .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<CourseVote>()
                .HasOne(c => c.Course)
                .WithMany(c => c.CourseVotes)
                .HasForeignKey(c => c.CourseId);

            #endregion

            #region Answer

            modelBuilder.Entity<Answer>()
                .HasOne(a => a.User)
               .WithMany(u => u.Answers)
               .HasForeignKey(a => a.UserId);

            modelBuilder.Entity<Answer>()
                .HasOne(a => a.Question)
               .WithMany(q => q.Answers)
               .HasForeignKey(a => a.QuestionId);

            #endregion

            #region Question 

            modelBuilder.Entity<Question>()
                .HasOne(q => q.User)
               .WithMany(u => u.Questions)
               .HasForeignKey(q => q.UserId);

            modelBuilder.Entity<Question>()
                .HasOne(q => q.Course)
               .WithMany(c => c.Questions)
               .HasForeignKey(q => q.CourseId);

            #endregion

        }
    }
}
