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
    //Login behøves ikke at følge CQRS designpattern
    public class Login : BaseCommand
    {
        private readonly ApplicationDbContext dbContext;
        private readonly SignInManager<ApplicationUser> signInManager;
        public Login(ILogger<Login> logger, ApplicationDbContext dbContext, SignInManager<ApplicationUser> signInManager) : base(logger,dbContext)
        {
            this.dbContext = dbContext;
            this.signInManager = signInManager;
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }

        protected override void RunCommand()
        {
            //var result = signInManager.PasswordSignInAsync(Username, Password, RememberMe, lockoutOnFailure: false);
            //if (result.Succeeded)
            //{
            //    _logger.LogInformation(1, "User logged in.");
                
            //}
            //else
            //{
            //    _logger.LogInformation(1, "Invalid login attempt.");
            //}
        }
    }
}
