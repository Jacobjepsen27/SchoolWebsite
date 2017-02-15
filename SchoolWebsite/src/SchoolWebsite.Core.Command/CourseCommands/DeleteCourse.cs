using Microsoft.Extensions.Logging;
using SchoolWebsite.Core.Entities;
using SchoolWebsite.Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolWebsite.Core.Command.CourseCommands
{
    public class DeleteCourse : BaseCommand
    {
        private readonly ILogger logger;
        private readonly ApplicationDbContext dbContext;
        public DeleteCourse(ILogger<AddCourse> logger, ApplicationDbContext dbContext) : base(logger,dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }

        public bool Status { get; set; }

        public Course Course { get; set; }

        protected override void RunCommand()
        {
            //Finds course and deletes it
            //var course = dbContext.Courses.Where(c => c.CourseId == CourseId).Single();
            dbContext.Courses.Remove(Course);
            int status = dbContext.SaveChanges();
            if (status != 0)
                Status = true;
            else
                Status = false;
        }
    }
}
