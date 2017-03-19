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
        private readonly IMapperAdapter mapper;

        public SearchController(IBooksService booksService, IMapperAdapter mapper)
        {
            // TODO: Gaurd

            this.booksService = booksService;
            this.mapper = mapper;
        }

        public ActionResult Index()
        {
            var model = new SearchViewModel();
            model.Books = mapper.Map<IEnumerable<BookViewModel>>(this.booksService.GetHighestRatedBooks(12).ToList());
            return View(model);
        }
    }
}