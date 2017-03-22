using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CourseProject.Services.Contracts;
using CourseProject.Web.Mapping;
using CourseProject.Web.Models;
using Microsoft.AspNet.Identity;
using CourseProject.Web.Identity.Contracts;

namespace CourseProject.Web.Controllers
{
    public class BookController : Controller
    {
        private readonly IBooksService booksService;
        private readonly IRatingsService ratingsService;
        private readonly IMapperAdapter mapper;
        private readonly IUserProvider userProvider;

        public BookController(IBooksService booksService, IRatingsService ratingsService, IMapperAdapter mapper, IUserProvider userProvider)
        {
            if (booksService == null)
            {
                throw new ArgumentNullException("booksService");
            }

            if (ratingsService == null)
            {
                throw new ArgumentNullException("ratingsService");
            }

            if (mapper == null)
            {
                throw new ArgumentNullException("mapper");
            }

            if (userProvider == null)
            {
                throw new ArgumentNullException("userProvider");
            }

            this.booksService = booksService;
            this.ratingsService = ratingsService;
            this.mapper = mapper;
            this.userProvider = userProvider;
        }

        public ActionResult Index(int id)
        {
            var book = this.booksService.GetById(id);

            if (book == null)
            {
                return this.View("Error");
            }

            var bookViewModel = this.mapper.Map<BookDetailsViewModel>(book);

            return this.View(bookViewModel);
        }

        [ChildActionOnly]
        public PartialViewResult GetRatingPartial(int id)
        {
            // TODO: should not include genre
            var ratingModel = new RatingViewModel();
            ratingModel.Id = id;

            var rating = this.booksService.GetBookRating(id);
            ratingModel.RatingCalculated = rating;

            string userId = this.userProvider.GetUserId();
            var userRating = this.ratingsService.GetRating(id, userId);

            ratingModel.UserRating = userRating;

            return PartialView("_RatingPartial", ratingModel);
        }

        [Authorize]
        public JsonResult Rate(int id, int rate)
        {
            // TODO: error handling
            var userId = this.userProvider.GetUserId();
            this.ratingsService.RateBook(id, userId, rate);
            var rating = this.booksService.GetBookRating(id);
            return Json(new { success = true, rating = rating }, JsonRequestBehavior.AllowGet);
        }
    }
}