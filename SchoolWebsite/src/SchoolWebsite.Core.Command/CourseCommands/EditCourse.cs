using Microsoft.Extensions.Logging;
using SchoolWebsite.Core.Entities;
using SchoolWebsite.Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolWebsite.Core.Command.CourseCommands
{
    public class EditCourse : BaseCommand
    {

        private readonly ILogger logger;
        private readonly ApplicationDbContext dbContext;

        public EditCourse(ILogger<EditCourse> logger, ApplicationDbContext dbContext) : base(logger, dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }

        public Course Course { get; set; }

        public bool Status { get; set; }

        protected override void RunCommand()
        {
            dbContext.Courses.Update(Course);
            int status = dbContext.SaveChanges();
            if (status != 0)
                Status = true;
            else
                Status = false;
        }
    }
}
