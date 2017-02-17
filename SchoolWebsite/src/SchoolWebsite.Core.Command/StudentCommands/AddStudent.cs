using Microsoft.Extensions.Logging;
using SchoolWebsite.Core.Entities;
using SchoolWebsite.Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolWebsite.Core.Command.StudentCommands
{
    public class AddStudent : BaseCommand
    {
        private readonly ApplicationDbContext dbContext;

        public AddStudent(ILogger<AddStudent> logger, ApplicationDbContext dbContext) : base(logger, dbContext)
        {
            this.dbContext = dbContext;
        }

        public Student Student { get; set; }
        protected override void RunCommand()
        {
            if (Student != null)
            {
                dbContext.Students.Add(Student);
                dbContext.SaveChanges();
            }
        }
    }
}
