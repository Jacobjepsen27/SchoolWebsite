using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using SchoolWebsite.Core.Entities;
using SchoolWebsite.Core.Query;
using SchoolWebsite.Application.Web.Models.CourseViewModels;
using Microsoft.AspNetCore.Authorization;
using SchoolWebsite.Core.Command.CourseCommands;
using AutoMapper;
using System;

namespace SchoolWebsite.Application.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CourseController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly QueryDb queryDb;
        private readonly ILogger _logger;
        private readonly IMapper mapper;

        public CourseController(UserManager<ApplicationUser> userManager, ILoggerFactory loggerFactory,
            QueryDb queryDb, IMapper mapper)
        {
            this.userManager = userManager;
            this.queryDb = queryDb;
            _logger = loggerFactory.CreateLogger<CourseController>();
            this.mapper = mapper;
        }

        
        public IActionResult Index()
        {
            var viewModel = new IndexViewModel();
            viewModel.Courses = queryDb.CoursesWithInstructors.ToList();
            return View(viewModel);
        }

        public IActionResult Create()
        {
            var viewModel = new CreateCourseViewModel();
            viewModel.Instructors = queryDb.Instructors.ToList();
            //ViewBag.InstructorList = new SelectList(queryDb.Instructors.ToList(), "Id","Name");//,"Id","Instructor Name");
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(CreateCourseViewModel viewModel, [FromServices] AddCourse command)
        {
            if (ModelState.IsValid)
            {
                //if course already exist return error
                try
                {
                    queryDb.Courses.Where(c => c.CourseId == viewModel.CourseId).Single();
                    ModelState.AddModelError(String.Empty, "A course with that Id already exists");
                    return RedirectToAction("Index","Course");
                }
                catch (Exception)
                {
                    command.CourseId = viewModel.CourseId;
                    command.Name = viewModel.Name;
                    command.InstructorId = viewModel.InstructorId;
                    command.Run();
                    if (command.status)
                        return RedirectToAction("Index", "Course");
                    else
                        return RedirectToAction("Error", "Course");
                }
            }
            //Something bad happened if we got to here
            return View(viewModel);
        }

        public IActionResult Delete(int id)
        {
            var course = queryDb.CoursesWithInstructors.Where(Id => Id.CourseId == id).Single();
            var viewModel = new DeleteCourseViewModel();
            viewModel.CourseId = course.CourseId;
            viewModel.Name = course.Name;
            if(course.Instructor!=null)
                viewModel.InstructorName = course.Instructor.Name;
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Delete(int id, [FromServices] DeleteCourse command)
        {
            if (ModelState.IsValid)
            {
                var course = queryDb.CoursesWithInstructors.Where(c => c.CourseId == id).Single();
                command.Course = course;
                command.Run();
                if (command.Status)
                    return RedirectToAction("Index", "Course");
                else
                    return RedirectToAction("Error", "Course");
            }
            return View();
           
        }

        public IActionResult Edit(int id)
        {
            var course = queryDb.CoursesWithInstructors.Where(c => c.CourseId == id).Single();
            var viewModel = mapper.Map<EditCourseViewModel>(course);
            viewModel.Instructors = queryDb.Instructors.ToList();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(int id, EditCourseViewModel viewModel, [FromServices] EditCourse command)
        {
            //Orignal course
            var course = queryDb.CoursesWithInstructors.Where(c => c.CourseId == id).Single();
            //Maps changes from viewModel to course
            course = mapper.Map<Course>(viewModel);
            course.CourseId = id;
            //Inserts course to command
            command.Course = course;
            //Execute command
            command.Run();
            if (command.Status)
                return RedirectToAction("Index", "Course");
            else
                return RedirectToAction("Error", "Course");
        }
    }
}