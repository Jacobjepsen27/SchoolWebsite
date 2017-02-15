using Microsoft.Extensions.Logging;
using SchoolWebsite.Core.Entities;
using SchoolWebsite.Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolWebsite.Core.Command.InstructorCommands
{
    public class DeleteInstructor : BaseCommand
    {
        private readonly ApplicationDbContext dbContext;
        public DeleteInstructor(ILogger<DeleteInstructor> logger, ApplicationDbContext dbContext) : base(logger, dbContext)
        {
            this.dbContext = dbContext;
        }

        public Instructor Instructor { get; set; }

        public bool Status { get; set; }

        protected override void RunCommand()
        {
            try
            {
                dbContext.Instructors.Remove(Instructor);
                dbContext.SaveChanges();
                Status = true;
            }
            catch (Exception ex)
            {
                Status = false;
            }


        }
    }
}
