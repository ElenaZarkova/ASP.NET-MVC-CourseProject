using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseProject.ViewModels.Mapping;

namespace CourseProject.ViewModels.Admin.AddBook
{
    public class BookModel : IMapFrom<AddBookViewModel>
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }
        
        public DateTime PublishedOn { get; set; }

        public int GenreId { get; set; }
    }
}
