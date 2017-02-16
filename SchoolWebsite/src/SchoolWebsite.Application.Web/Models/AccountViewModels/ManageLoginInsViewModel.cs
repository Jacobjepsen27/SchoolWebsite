using SchoolWebsite.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolWebsite.Application.Web.Models.AccountViewModels
{
    public class ManageLoginInsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime? Birth { get; set; }

        public ICollection<Instructor> InstructorsWithLogin { get; set; }
        public ICollection<Instructor> InstructorsNoLogin { get; set; }
    }
}
