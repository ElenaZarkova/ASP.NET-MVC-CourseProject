using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseProject.Web.Models
{
    public class SearchViewModel
    {
        public IEnumerable<SelectListItem> Genres { get; set; }

        public SearchSubmitModel submitModel { get; set; }

        public IEnumerable<BookViewModel> Books { get; set; }
    }
}