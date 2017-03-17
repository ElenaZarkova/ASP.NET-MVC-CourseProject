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

namespace CourseProject.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBooksService booksService;

        public HomeController(IBooksService booksService)
        {
            // TODO: Guard

            this.booksService = booksService;
        }

        public ActionResult Index()
        {
            // TODO: fix mapping
            var books = this.booksService.GetHighestRatedBooks(8).ToList();
            var mappedBooks = AutoMapper.Mapper.Map<IEnumerable<BookViewModel>>(books);
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