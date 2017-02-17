using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using AutoMapper;
using SchoolWebsite.Infrastructure.DataAccess;
using SchoolWebsite.Infrastructure.DataAccess.CsvSeeder;
using SchoolWebsite.Core.Entities;
using SchoolWebsite.Application.Web.Services;
using Microsoft.AspNetCore.Identity;
using SchoolWebsite.Core.Command.AccountCommands;
using SchoolWebsite.Core.Query;
using SchoolWebsite.Core.Command.CourseCommands;
using SchoolWebsite.Core.Command.InstructorCommands;
using SchoolWebsite.Application.Web.ConfigurationObjects;

namespace SchoolWebsite.Application.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see https://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();

            Log.Logger = new LoggerConfiguration()
              .Enrich.FromLogContext()
              .ReadFrom.Configuration(Configuration)
              .CreateLogger();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("SchoolWebsite.Infrastructure.DataAccess")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            services.AddScoped<QueryDb, QueryDb>();
            services.AddTransient<Login, Login>();
            services.AddTransient<AddCourse, AddCourse>();
            services.AddTransient<DeleteCourse,DeleteCourse>();
            services.AddTransient<EditCourse, EditCourse>();
            services.AddTransient<CreateInstructor, CreateInstructor>();
            services.AddTransient<DeleteInstructor, DeleteInstructor>();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>(); 
            services.AddTransient<ISmsSender, AuthMessageSender>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperConfig());
            });
            services.AddSingleton<IMapper>(sp => config.CreateMapper());

            //Option pattern
            services.AddOptions();
            services.Configure<RoleOptions>(Configuration.GetSection("roles"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, ApplicationDbContext context,
            RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            loggerFactory.AddDebug();
            loggerFactory.AddSerilog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseIdentity();

            // Add external authentication middleware below. To configure them please see https://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            context.Database.Migrate();

            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                roleManager.CreateAsync(role).Wait();
            }
            if (!roleManager.RoleExistsAsync("Instructor").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Instructor";
                roleManager.CreateAsync(role).Wait();
            }
            if (!roleManager.RoleExistsAsync("Student").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Student";
                roleManager.CreateAsync(role).Wait();
            }
            if (!context.Students.Any())
            {
                context.Students.Add(new Student { Name = "Jacob", Email = "Student@gmail.com", Birth = new DateTime(1994, 06, 24) });
                //context.Students.SeedFromFile("SeedData/students.csv"); TODO
            }
            context.SaveChanges();
            if (!context.Courses.Any())
            {
                context.Courses.Add(new Course {CourseId = 1234, Name = "Math" });
                //context.Courses.SeedFromFile("SeedData/courses.csv");
            }
            context.SaveChanges();
            if (!context.Instructors.Any())
            {
                context.Instructors.Add(new Instructor { Name = "Jon", Email = "Instructor@gmail.com", Birth = new DateTime(1990, 04, 14) });
                //context.Instructors.SeedFromFile("SeedData/instructors.csv");
            }
            context.SaveChanges();
            //foreach (var item in roleManager.Roles)
            //{
            IList<ApplicationUser> adminList = await userManager.GetUsersInRoleAsync("Admin");
            if (!adminList.Any())
            {
                ApplicationUser appUser = new ApplicationUser();
                appUser.Email = "Admin@gmail.com";
                appUser.UserName = "Admin";
                //Trying to create IdentityUser
                //IdentityResult result = 
                await userManager.CreateAsync(appUser, "43Polser!");
                //If success, assign role to user
                //if (result.Succeeded)
                //{
                    userManager.AddToRoleAsync(appUser, "Admin").Wait();
                //}
            }
            IList<ApplicationUser> insList = await userManager.GetUsersInRoleAsync("Instructor");
            if (!insList.Any())
            {
                ApplicationUser appUser = new ApplicationUser();
                appUser.Email = "Instructor@gmail.com";
                appUser.UserName = "Instructor";
                //Trying to create IdentityUser
                IdentityResult result = userManager.CreateAsync
                (appUser, "43Polser!").Result;
                //If success, assign role to user
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(appUser, "Instructor").Wait();
                    foreach (var ins in context.Instructors.ToList())
                    {
                        if (ins.Email == appUser.Email)
                        {
                            appUser.InstructorId = ins.Id;
                        }
                    }
                }
            }
            IList<ApplicationUser> stuList = await userManager.GetUsersInRoleAsync("Student");
            if (!stuList.Any())
            {
                ApplicationUser appUser = new ApplicationUser();
                appUser.Email = "Student@gmail.com";
                appUser.UserName = "Student";
                //Trying to create IdentityUser
                IdentityResult result = userManager.CreateAsync
                (appUser, "43Polser!").Result;
                //If success, assign role to user
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(appUser, "Student").Wait();
                    foreach (var stu in context.Students.ToList())
                    {
                        if (stu.Email == appUser.Email)
                        {
                            appUser.StudentId = stu.Id;
                        }
                    }
                }
            }
            context.SaveChanges(); //VIGTIG: Ellers tilføjes password ikke
        }
    }
}
