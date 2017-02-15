using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolWebsite.Core.Entities
{
    public class Instructor : Person
    {
        public override int Id { get; set; }
        public override string Name { get; set; }
        public override string Email { get; set; }
        public override DateTime? Birth { get; set; }

        //Navgiation properties
        public virtual ICollection<Course> Courses { get; set; } = new HashSet<Course>();
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
