using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolWebsite.Core.Entities
{
    public class CourseStudent
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }

        //Navgiation Properties
        public virtual Student Student { get; set; }
        public virtual Course Course { get; set; }
    }
}
