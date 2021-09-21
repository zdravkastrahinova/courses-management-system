using CourseManagementSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourseManagementSystem.Repositories
{
    public class CoursesManagementSystemContext : DbContext
    {
        public CoursesManagementSystemContext(DbContextOptions<CoursesManagementSystemContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<CategoryCourse> CategoryCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Category>()
                .HasMany(category => category.CategoryCourses)
                .WithOne()
                .HasForeignKey(cc => cc.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Course>()
                .HasMany(course => course.CategoryCourses)
                .WithOne()
                .HasForeignKey(cc => cc.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<CategoryCourse>()
                .HasOne(cc => cc.Category)
                .WithMany(category => category.CategoryCourses);

            builder.Entity<CategoryCourse>()
                .HasOne(cc => cc.Course)
                .WithMany(course => course.CategoryCourses);

            builder.Entity<Course>()
                .HasMany(course => course.Ratings)
                .WithOne()
                .HasForeignKey(rating => rating.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Rating>()
                .HasOne(rating => rating.Course)
                .WithMany(course => course.Ratings);
        }
    }
}
