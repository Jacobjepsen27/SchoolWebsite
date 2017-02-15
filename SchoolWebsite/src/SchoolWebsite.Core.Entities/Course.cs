using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolWebsite.Core.Entities
{
    public class Course
    {
        public int CourseId { get; set; }
        public string Name { get; set; }
        public int? InstructorId { get; set; }

        //Navigaiton properties
        public virtual ICollection<CourseStudent> CourseStudents { get; set; } = new HashSet<CourseStudent>();
        public virtual Instructor Instructor { get; set; }

    }
}
