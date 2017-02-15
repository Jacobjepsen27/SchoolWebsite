using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolWebsite.Core.Entities;
using SchoolWebsite.Infrastructure.DataAccess;

namespace SchoolWebsite.Core.Query
{
    public class QueryDb
    {
        private readonly ApplicationDbContext _dbContext;

        public QueryDb(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public IQueryable<ApplicationUser> Users => _dbContext.Users.AsQueryable();
        public IQueryable<Course> Courses => _dbContext.Courses.AsQueryable();
        public IQueryable<Course> CoursesWithInstructors => _dbContext.Courses.AsQueryable().Include(i => i.Instructor);
        public IQueryable<Student> Students => _dbContext.Students.AsQueryable();
        public IQueryable<Instructor> Instructors => _dbContext.Instructors.AsQueryable();
        public IQueryable<Instructor> InstructorsWithCourses => _dbContext.Instructors.AsQueryable().Include(c => c.Courses);
    }
}
