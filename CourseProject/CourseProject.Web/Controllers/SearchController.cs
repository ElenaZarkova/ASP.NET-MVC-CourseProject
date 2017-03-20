using CourseProject.Services.Contracts;
using CourseProject.Web.Mapping;
using CourseProject.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseProject.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly IBooksService booksService;
        private readonly IGenresService genresService;
        private readonly IMapperAdapter mapper;

        public SearchController(IBooksService booksService, IGenresService genresService, IMapperAdapter mapper)
        {
            // TODO: Gaurd

            this.booksService = booksService;
            this.genresService = genresService;
            this.mapper = mapper;
        }

        public ActionResult Index()
        {
            var model = new SearchViewModel();
            //model.Books = mapper.Map<IEnumerable<BookViewModel>>(this.booksService.GetHighestRatedBooks(9).ToList());
            model.Genres = this.mapper.Map<IEnumerable<GenreViewModel>>(this.genresService.GetAllGenres());

            return View(model);
        }
        
        public PartialViewResult SearchBooks(SearchSubmitModel submitModel, int? page)
        {
            //bool modelIsNull = submitModel.SearchWord == null && submitModel.ChosenGenresIds == null && submitModel.SortBy == null;
            //if(modelIsNull)
            //{
            //    var books = mapper.Map<IEnumerable<BookViewModel>>(this.booksService.GetHighestRatedBooks(9).ToList());
            //    return this.PartialView("_ResultsPartial", books);
            //}
            int actualPage = page ?? 1;
            int booksPerPage = 3;
            var result = this.booksService.SearchBooks(submitModel.SearchWord, submitModel.ChosenGenresIds, submitModel.SortBy, actualPage, booksPerPage);
            var count = this.booksService.GetBooksCount(submitModel.SearchWord, submitModel.ChosenGenresIds);

            var resultViewModel = new SearchResultsViewModel();
            resultViewModel.BooksCount = count;
            resultViewModel.SubmitModel = submitModel;
            resultViewModel.Pages = (int)Math.Ceiling((double)count / booksPerPage);

            resultViewModel.Books = mapper.Map<IEnumerable<BookViewModel>>(result);

            return this.PartialView("_ResultsPartial", resultViewModel);
        }
    }
}