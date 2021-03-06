﻿using SchoolWebsite.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolWebsite.Application.Web.Models.CourseViewModels
{
    public class CreateCourseViewModel
    {
        [Display(Name = "Course Id")]
        public int CourseId { get; set; }
        [Display(Name = "Course Name")]
        public string Name { get; set; }
        [Display(Name = "Instructor")]
        public int? InstructorId { get; set; }

        public IList<Instructor> Instructors { get; set; }
    }
}
