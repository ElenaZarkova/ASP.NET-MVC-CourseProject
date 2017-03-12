using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using CourseProject.Models;
using CourseProject.Web.Mapping;

namespace CourseProject.Web.Models
{
    public class GenreViewModel : IMapFrom<Genre>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public string NameAndId { get; set; }

        public void CreateMappings(IMapperConfigurationExpression config)
        {
            config.CreateMap<Genre, GenreViewModel>()
                .ForMember(x => x.NameAndId, opt => opt.MapFrom(x => x.Name + " " + x.Id));
        }
    }
}