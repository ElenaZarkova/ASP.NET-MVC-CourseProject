using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CourseProject.Models;
using CourseProject.Web.Mapping;

namespace CourseProject.Web.Models
{
    public class RatingViewModel : IMapFrom<Book>
    {
        public int Id { get; set; }

        public double RatingCalculated { get; set; }

        public int UserRating { get; set; }
    }
}