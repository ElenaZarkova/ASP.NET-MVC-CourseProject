using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CourseProject.Services.Contracts;
using CourseProject.Web.Mapping;
using CourseProject.Web.Models;

namespace CourseProject.Web.Controllers
{
    public class BookController : Controller
    {
        private readonly IBooksService booksService;
        private readonly IMapperAdapter mapper;

        public BookController(IBooksService booksService, IMapperAdapter mapper)
        {
            // TODO: Gaurd

            this.booksService = booksService;
            this.mapper = mapper;
        }

        public ActionResult Index(int id)
        {
            var book = this.booksService.GetById(id);

            if(book == null)
            {
                return this.View("Error");
            }

            var bookViewModel = this.mapper.Map<BookDetailsViewModel>(book);
            return this.View(bookViewModel);
        }
    }
}