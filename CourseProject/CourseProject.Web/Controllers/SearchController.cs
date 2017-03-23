using CourseProject.Services.Contracts;
using CourseProject.Web.Common;
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
            if (booksService == null)
            {
                throw new ArgumentNullException("booksService");
            }

            if (genresService == null)
            {
                throw new ArgumentNullException("genresService");
            }

            if (mapper == null)
            {
                throw new ArgumentNullException("mapper");
            }

            this.booksService = booksService;
            this.genresService = genresService;
            this.mapper = mapper;
        }

        public ActionResult Index()
        {
            var model = new SearchViewModel();
            var genres = this.genresService.GetAllGenres();
            model.Genres = this.mapper.Map<IEnumerable<GenreViewModel>>(genres);

            return View(model);
        }
        
        public PartialViewResult SearchBooks(SearchSubmitModel submitModel, int? page)
        {
            int actualPage = page ?? 1;

            var result = this.booksService.SearchBooks(submitModel.SearchWord, submitModel.ChosenGenresIds, submitModel.SortBy, actualPage, Constants.BooksPerPage);
            var count = this.booksService.GetBooksCount(submitModel.SearchWord, submitModel.ChosenGenresIds);

            var resultViewModel = new SearchResultsViewModel();
            resultViewModel.BooksCount = count;
            resultViewModel.SubmitModel = submitModel;
            resultViewModel.Pages = (int)Math.Ceiling((double)count / Constants.BooksPerPage);

            resultViewModel.Books = mapper.Map<IEnumerable<BookViewModel>>(result);

            return this.PartialView("_ResultsPartial", resultViewModel);
        }
    }
}