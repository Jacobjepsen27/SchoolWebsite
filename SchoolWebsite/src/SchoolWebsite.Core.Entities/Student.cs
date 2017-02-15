using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolWebsite.Core.Entities
{
    public class Student : Person
    {
        public override int Id { get; set; }
        public override string Name { get; set; }
        public override string Email { get; set; }
        public override DateTime? Birth { get; set; }

        //Navigation properties
        public virtual ICollection<CourseStudent> CourseStudents { get; set; } = new HashSet<CourseStudent>();
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
