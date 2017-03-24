using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CourseProject.Services.Contracts;
using CourseProject.Web.Common;
using CourseProject.Web.Mapping;
using CourseProject.Web.Models;
using CourseProject.Web.Attributes;

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
        
        [ChildActionOnly]
        public PartialViewResult SearchInitial()
        {
            int page = 1;

            return this.ExecuteSearch(new SearchSubmitModel(), page);
        }

        [HttpPost]
        [AjaxOnly]
        public PartialViewResult SearchBooks(SearchSubmitModel submitModel, int? page) //, string genresIds)
        {
            int actualPage = page ?? 1;

            //if(submitModel.ChosenGenresIds == null && string.IsNullOrEmpty(genresIds))
            //{
            //    submitModel.ChosenGenresIds = genresIds.Split(';').Select(int.Parse).ToList();
            //}

            return this.ExecuteSearch(submitModel, actualPage);
        }

        private PartialViewResult ExecuteSearch(SearchSubmitModel submitModel, int page)
        {
            var result = this.booksService.SearchBooks(submitModel.SearchWord, submitModel.ChosenGenresIds, submitModel.SortBy, page, Constants.BooksPerPage);
            var count = this.booksService.GetBooksCount(submitModel.SearchWord, submitModel.ChosenGenresIds);

            var resultViewModel = new SearchResultsViewModel();
            resultViewModel.BooksCount = count;
            resultViewModel.SubmitModel = submitModel;
            resultViewModel.Pages = (int)Math.Ceiling((double)count / Constants.BooksPerPage);
            resultViewModel.Page = page;
            resultViewModel.Books = mapper.Map<IEnumerable<BookViewModel>>(result);

            return this.PartialView("_ResultsPartial", resultViewModel);
        }
    }
}