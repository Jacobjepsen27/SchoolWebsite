using SchoolWebsite.Application.Web.Models.CourseViewModels;
using SchoolWebsite.Application.Web.Models.InstructorViewModels;
using SchoolWebsite.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolWebsite.Application.Web.Services
{
    public class AutoMapperConfig : AutoMapper.Profile
    {
        protected override void Configure()
        {
            //CreateMap<RegisterViewModel, ApplicationUser>().ReverseMap();
            CreateMap<EditCourseViewModel, Course>().ReverseMap();
            CreateMap<InstructorViewModel, Instructor>().ReverseMap();
        }
    }
}