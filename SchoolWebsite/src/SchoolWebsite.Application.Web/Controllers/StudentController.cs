using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using SchoolWebsite.Core.Query;
using Microsoft.Extensions.Logging;
using AutoMapper;
using SchoolWebsite.Application.Web.Models.StudentViewModels;
using SchoolWebsite.Core.Entities;
using SchoolWebsite.Core.Command.StudentCommands;

namespace SchoolWebsite.Application.Web.Controllers
{
    public class StudentController : Controller
    {
        private readonly QueryDb queryDb;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public StudentController(QueryDb queryDb, ILoggerFactory loggerFactory, IMapper mapper)
        {
            this.queryDb = queryDb;
            this.logger = loggerFactory.CreateLogger<StudentController>();
            this.mapper = mapper;
        }

        // GET: Student
        public ActionResult Index()
        {
            var viewModel = new IndexViewModel();
            viewModel.Students = queryDb.Students.ToList();
            return View(viewModel);
        }

        public IActionResult Create()
        {
            var viewModel = new IndexViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IndexViewModel viewModel, [FromServices] AddStudent command)
        {
            var student = mapper.Map<Student>(viewModel);
            command.Student = student;
            command.Run();
            return RedirectToAction("Index");
        }

        // GET: Student/Delete/5
        public ActionResult Delete(int id)
        {
            var student = queryDb.Students.Where(s => s.Id == id).Single();
            var viewModel = mapper.Map<IndexViewModel>(student);
            return View(viewModel);
        }

        // POST: Student/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, [FromServices] DeleteStudent command)
        {
            var student = queryDb.Students.Where(s => s.Id == id).Single();
            command.Student = student;
            command.Run();
            return RedirectToAction("Index");
        }


        // GET: Student/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Student/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        
    }
}