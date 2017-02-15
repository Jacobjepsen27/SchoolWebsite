using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolWebsite.Core.Entities
{
    public abstract class Person
    {
        public abstract int Id { get; set; }
        public abstract string Name { get; set; }
        public abstract string Email { get; set; }
        public abstract DateTime? Birth { get; set; }
    }
}
