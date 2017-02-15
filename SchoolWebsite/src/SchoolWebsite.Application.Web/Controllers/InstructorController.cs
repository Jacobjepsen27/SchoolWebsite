using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using SchoolWebsite.Core.Query;
using Microsoft.Extensions.Logging;
using AutoMapper;
using SchoolWebsite.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using SchoolWebsite.Application.Web.Models.InstructorViewModels;
using SchoolWebsite.Core.Command.InstructorCommands;

namespace SchoolWebsite.Application.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class InstructorController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly QueryDb queryDb;
        private readonly ILogger _logger;
        private readonly IMapper mapper;

        public InstructorController(UserManager<ApplicationUser> userManager, ILoggerFactory loggerFactory,
            QueryDb queryDb, IMapper mapper)
        {
            this.userManager = userManager;
            this.queryDb = queryDb;
            _logger = loggerFactory.CreateLogger<InstructorController>();
            this.mapper = mapper;
        }
        
        public IActionResult Index()
        {
            var viewModel = new InstructorViewModel();
            viewModel.Instructors = queryDb.Instructors.ToList();
            return View(viewModel);
        }

        public IActionResult Create()
        {
            var viewModel = new InstructorViewModel();
            viewModel.Courses = queryDb.Courses.ToList();
            viewModel.Filters = CheckBoxInitialization(viewModel);
            return View(viewModel);
        }
        

        [HttpPost]
        public IActionResult Create(InstructorViewModel viewModel, [FromServices] CreateInstructor command)
        {
            if (ModelState.IsValid)
            {
                command.Instructor = mapper.Map<Instructor>(viewModel);
                //Only search for a course if the instructor chose to be assigned one
                if(viewModel.CourseId!=null)
                    command.Course = queryDb.Courses.Where(c => c.CourseId == viewModel.CourseId).Single();
                command.Run();
                if (command.Status)
                    return RedirectToAction("Index", "Instructor");
                else
                    return RedirectToAction("Error", "Instructor");
            }
            return View();
        }

        public IActionResult Delete(int id)
        {
            var instructor = queryDb.Instructors.Where(i => i.Id == id).Single();
            var viewModel = mapper.Map<InstructorViewModel>(instructor);
            //Loading related courses into viewmodel
            viewModel.Courses = queryDb.Courses.Where(c => c.InstructorId == id).ToList();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Delete(int id, [FromServices] DeleteInstructor command)
        {
            var instructor = queryDb.InstructorsWithCourses.Where(i => i.Id == id).Single();
            command.Instructor = instructor;
            command.Run();
            if (command.Status)
                    return RedirectToAction("Index", "Instructor");
                else
                    return RedirectToAction("Error", "Instructor");
        }




        //This method fills a Filter[] with data about the courses.
        //Filter[] contains an Assigned Boolean which will tell the view whether or not the course is available for a instructor
        public Filter[] CheckBoxInitialization(InstructorViewModel viewModel)
        {
            int courseCount = viewModel.Courses.Count();//viewModel.Courses.Count();
            Course[] course = new Course[courseCount];
            course = viewModel.Courses.ToArray();
            Filter[] filter = new Filter[courseCount];

            //Has to initialize every filter to avoid Non-instantiated object exception or whatever
            for (int i = 0; i < courseCount; i++)
            {
                filter[i] = new Filter();
            }
            //Now we can fill the Filter array with data which is used by the razor view
            for (int i = 0; i < courseCount; i++)
            {
                filter[i].Id = course[i].CourseId;
                filter[i].Name = course[i].Name;
                filter[i].Selected = false;
                if (course[i].InstructorId != null)
                    filter[i].Assigned = true;
                else
                    filter[i].Assigned = false;

            }
            return filter;
        }

    }
}