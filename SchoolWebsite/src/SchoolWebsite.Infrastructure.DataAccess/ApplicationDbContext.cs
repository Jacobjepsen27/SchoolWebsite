using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolWebsite.Core.Entities;

namespace SchoolWebsite.Infrastructure.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Instructor> Instructors { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Course>().Property(p => p.CourseId).ValueGeneratedNever();

            builder.Entity<CourseStudent>()
                .HasKey(x => new { x.CourseId, x.StudentId });

            builder.Entity<CourseStudent>()
                .HasOne(p => p.Course)
                .WithMany(c => c.CourseStudents)
                .HasForeignKey(fk => fk.CourseId);

            builder.Entity<CourseStudent>()
                .HasOne(p => p.Student)
                .WithMany(s => s.CourseStudents)
                .HasForeignKey(fk => fk.StudentId);
        }
    }
}
