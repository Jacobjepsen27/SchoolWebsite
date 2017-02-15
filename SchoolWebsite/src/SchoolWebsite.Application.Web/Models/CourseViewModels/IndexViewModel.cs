using SchoolWebsite.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolWebsite.Application.Web.Models.CourseViewModels
{
    public class IndexViewModel
    {
        public List<Course> Courses { get; set; }
    }
}
