using Microsoft.Extensions.Logging;
using SchoolWebsite.Core.Entities;
using SchoolWebsite.Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolWebsite.Core.Command.InstructorCommands
{
    public class CreateInstructor : BaseCommand
    {
        private readonly ApplicationDbContext dbContext;
        public CreateInstructor(ILogger<CreateInstructor> logger, ApplicationDbContext dbContext) : base(logger, dbContext)
        {
            this.dbContext = dbContext;
        }

        public Instructor Instructor { get; set; }
        //Related to instructor
        //public Course Course { get; set; }

        public IList<Course> Courses { get; set; }

        public bool Status { get; set; }

        protected override void RunCommand()
        {
            try
            {
                dbContext.Instructors.Add(Instructor);
                dbContext.SaveChanges();
                if (Courses != null)
                {
                    foreach (var course in Courses)
                    {
                        course.InstructorId = Instructor.Id;
                        dbContext.Courses.Update(course);
                        dbContext.SaveChanges();
                    }
                }
                //if (Course!=null)
                //{
                //    Course.InstructorId = Instructor.Id;
                //    dbContext.Courses.Update(Course);
                //    dbContext.SaveChanges();
                //}
                Status = true;
            } catch (Exception ex)
            {
                Status = false;
            }
            

        }
    }
}
