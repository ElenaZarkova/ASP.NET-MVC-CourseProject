using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using CourseProject.Data.Contracts;
using CourseProject.Web.Models;
using CourseProject.Models;
using CourseProject.Services.Contracts;
using CourseProject.Web.Mapping;

namespace CourseProject.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBooksService booksService;
        private readonly IMapperAdapter mapper;

        public HomeController(IBooksService booksService, IMapperAdapter mapper)
        {
            // TODO: Guard

            this.booksService = booksService;
            this.mapper = mapper;
        }

        public ActionResult Index()
        {
            // TODO: fix mapping
            var books = this.booksService.GetHighestRatedBooks(8).ToList();
            var mappedBooks = this.mapper.Map<IEnumerable<BookViewModel>>(books);
            return View(mappedBooks);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}