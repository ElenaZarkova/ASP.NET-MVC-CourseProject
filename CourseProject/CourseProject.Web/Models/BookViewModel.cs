using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CourseProject.Web.Mapping;
using CourseProject.Models;
using System.ComponentModel.DataAnnotations;

namespace CourseProject.Web.Models
{
    public class BookViewModel: IMapFrom<Book>
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public string CoverFile { get; set; }
        
    }
}