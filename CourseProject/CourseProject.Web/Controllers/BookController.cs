using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CourseProject.Services.Contracts;
using CourseProject.Web.Mapping;
using CourseProject.Web.Models;
using Microsoft.AspNet.Identity;

namespace CourseProject.Web.Controllers
{
    public class BookController : Controller
    {
        private readonly IBooksService booksService;
        private readonly IRatingsService ratingService;
        private readonly IMapperAdapter mapper;

        public BookController(IBooksService booksService,IRatingsService ratingService, IMapperAdapter mapper)
        {
            // TODO: Gaurd

            this.booksService = booksService;
            this.ratingService = ratingService;
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

            // property would not map automatically
            bookViewModel.RatingCalculated = book.RatingCalculated;
            int userRating;
            var rating = book.Ratings.FirstOrDefault(x => x.UserId == this.User.Identity.GetUserId());
            if (rating != null)
            {
                userRating = rating.Value;
            }
            else
            {
                userRating = 0;
            }

            bookViewModel.UserRating = userRating;        
            return this.View(bookViewModel);
        }

        public JsonResult Rate(int id, int rate)
        {
            // TODO: error handling
            this.ratingService.RateBook(id, this.User.Identity.GetUserId(), rate);
            var rating = this.booksService.GetBookRating(id);
            return Json(new { success = true, rating = rating }, JsonRequestBehavior.AllowGet);
        }
    }
}