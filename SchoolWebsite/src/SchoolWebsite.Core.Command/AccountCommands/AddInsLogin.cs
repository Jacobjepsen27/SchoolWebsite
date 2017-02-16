using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SchoolWebsite.Core.Entities;
using SchoolWebsite.Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolWebsite.Core.Command.AccountCommands
{
    public class AddInsLogin : BaseCommand
    {
        private readonly ApplicationDbContext dbContext;
        private readonly SignInManager<ApplicationUser> signInManager;
        public AddInsLogin(ILogger<AddInsLogin> logger, ApplicationDbContext dbContext, SignInManager<ApplicationUser> signInManager) : base(logger,dbContext)
        {
            this.dbContext = dbContext;
            this.signInManager = signInManager;
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public bool Status { get; set; }

        protected override void RunCommand()
        {
            
        }
    }
}
