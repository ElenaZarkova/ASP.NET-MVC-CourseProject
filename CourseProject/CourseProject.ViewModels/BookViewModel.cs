using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CourseProject.ViewModels.Mapping;
using CourseProject.Models;
using System.ComponentModel.DataAnnotations;

namespace CourseProject.ViewModels
{
    public class BookViewModel: IMapFrom<Book>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string CoverFile { get; set; }
        
    }
}