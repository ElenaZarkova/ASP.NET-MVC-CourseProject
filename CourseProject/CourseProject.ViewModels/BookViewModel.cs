using CourseProject.Models;
using CourseProject.ViewModels.Mapping;

namespace CourseProject.ViewModels
{
    public class BookViewModel : IMapFrom<Book>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string CoverFile { get; set; }   
    }
}