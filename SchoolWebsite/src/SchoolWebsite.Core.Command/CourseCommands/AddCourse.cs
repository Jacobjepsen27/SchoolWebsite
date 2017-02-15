using Microsoft.Extensions.Logging;
using SchoolWebsite.Core.Entities;
using SchoolWebsite.Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolWebsite.Core.Command.CourseCommands
{
    public class AddCourse : BaseCommand
    {
        private readonly ApplicationDbContext dbContext;
        public AddCourse(ILogger<AddCourse> logger, ApplicationDbContext dbContext) : base(logger, dbContext)
        {
            this.dbContext = dbContext;
        }

        public int CourseId { get; set; }
        public string Name { get; set; }
        public int? InstructorId { get; set; }
        public bool status { get; set; }

        protected override void RunCommand()
        {
            var course = new Course();
            course.CourseId = CourseId;
            course.Name = Name;
            course.InstructorId = InstructorId;
            dbContext.Courses.Add(course);
            dbContext.SaveChanges();
            if (course.CourseId != 0)
                status = true;
            else
                status = false;
        }
    }
}
