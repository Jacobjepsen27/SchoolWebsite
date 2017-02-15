using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolWebsite.Application.Web.Models.InstructorViewModels
{
    public class Filter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Selected { get; set; }
        public bool Assigned { get; set; }

    }
}
