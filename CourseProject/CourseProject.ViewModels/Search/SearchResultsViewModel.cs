using System.Collections.Generic;

namespace CourseProject.ViewModels.Search
{
    public class SearchResultsViewModel
    {
        public IEnumerable<BookViewModel> Books { get; set; }

        public int BooksCount { get; set; }

        public int Pages { get; set; }

        public int Page { get; set; }

        public SearchSubmitModel SubmitModel { get; set; }
    }
}