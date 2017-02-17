using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolWebsite.Application.Web.ConfigurationObjects
{
    public class RoleOptions
    {
        public RoleOptions()
        {
            Admin = "default";
            Instructor = "default";
            Student = "default";
        }

        public string Admin { get; set; }
        public string Instructor { get; set; }
        public string Student { get; set; }
    }
}
