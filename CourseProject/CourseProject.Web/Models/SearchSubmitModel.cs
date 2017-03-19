using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseProject.Web.Models
{
    public class SearchSubmitModel
    {
        public string SearchWord { get; set; }

        public IEnumerable<int> ChosenGenresIds { get; set; }

        public string SortBy { get; set; }
    }
}