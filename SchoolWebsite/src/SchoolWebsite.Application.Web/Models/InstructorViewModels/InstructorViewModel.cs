using SchoolWebsite.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolWebsite.Application.Web.Models.InstructorViewModels
{
    public class InstructorViewModel
    {
        [Display(Name = "Instructor Id")]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime? Birth { get; set; }

        public Filter[] Filters { get; set; }

        public int? CourseId { get; set; }

        public IList<Instructor> Instructors { get; set; }

        public IList<Course> Courses { get; set; }
    }
}
