using System;
using CourseProject.Models;
using CourseProject.ViewModels.Mapping;

namespace CourseProject.ViewModels.BookDetails
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
    }
}