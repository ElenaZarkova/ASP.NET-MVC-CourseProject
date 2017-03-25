using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseProject.ViewModels.Search
{
    public class SearchViewModel
    {
        public IEnumerable<GenreViewModel> Genres { get; set; }
    }
}