using System.Collections.Generic;

namespace CourseProject.ViewModels.Search
{
    public class SearchSubmitModel
    {
        public string SearchWord { get; set; }

        public IEnumerable<int> ChosenGenresIds { get; set; }

        public string SortBy { get; set; }
    }
}