using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CourseProject.Models;
using CourseProject.Web.Mapping;

namespace CourseProject.Web.Models
{
    public class BookDetailsViewModel : IMapFrom<Book>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }

        public DateTime PublishedOn { get; set; }

        public string CoverFile { get; set; }
        
        public string GenreName { get; set; }

        public ICollection<Rating> Ratings { get; set; }

        public double RatingCalculated { get; }
    }
}