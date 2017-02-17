using SchoolWebsite.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolWebsite.Application.Web.Models.StudentViewModels
{
    public class IndexViewModel
    {
        
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public DateTime? Birth { get; set; }

        public IList<Student> Students { get; set; }
    }
}
