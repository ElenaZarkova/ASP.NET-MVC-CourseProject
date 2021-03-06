﻿using CourseProject.Models;
using CourseProject.ViewModels.Mapping;

namespace CourseProject.ViewModels.Search
{
    public class GenreViewModel : IMapFrom<Genre>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}