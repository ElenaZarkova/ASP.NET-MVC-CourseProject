using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using CourseProject.Data.Contracts;
using CourseProject.ViewModels;
using CourseProject.Services.Contracts;
using CourseProject.Web.Mapping;
using CourseProject.Web.Common;
using CourseProject.Web.Common.Providers.Contracts;

namespace CourseProject.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBooksService booksService;
        private readonly ICacheProvider cacheProvider;
        private readonly IMapperAdapter mapper;

        public HomeController(IBooksService booksService, ICacheProvider cacheProvider, IMapperAdapter mapper)
        {
            if(booksService == null)
            {
                throw new ArgumentNullException("booksService");
            }

            if (cacheProvider == null)
            {
                throw new ArgumentNullException("cacheProvider");
            }

            if (mapper == null)
            {
                throw new ArgumentNullException("mapper");
            }

            this.booksService = booksService;
            this.cacheProvider = cacheProvider;
            this.mapper = mapper;
        }

        public ActionResult Index()
        {
            var topBooks = (IEnumerable<BookViewModel>)this.cacheProvider.GetValue(Constants.TopBooksCache);

            if(topBooks == null)
            {
                var books = this.booksService.GetHighestRatedBooks(Constants.TopBooksCount).ToList();
                topBooks = this.mapper.Map<IEnumerable<BookViewModel>>(books);

                this.cacheProvider.InsertWithAbsoluteExpiration(Constants.TopBooksCache, topBooks, DateTime.UtcNow.AddMinutes(Constants.TopBooksExpirationInMinutes));
            }

            return View(topBooks);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}