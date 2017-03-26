using System;
using System.Web.Mvc;
using CourseProject.Services.Contracts;
using CourseProject.ViewModels.BookDetails;
using CourseProject.Web.Mapping;
using CourseProject.Web.Common.Providers.Contracts;
using CourseProject.Web.Common;
using CourseProject.Web.Attributes;

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
                return this.View("BookError");
            }

            var bookViewModel = this.mapper.Map<BookDetailsViewModel>(book);

            return this.View(bookViewModel);
        }

        [ChildActionOnly]
        public PartialViewResult GetRatingPartial(int id)
        {
            var rating = this.booksService.GetBookRating(id);

            string userId = this.userProvider.GetUserId();
            var userRating = this.ratingsService.GetRating(id, userId);

            var ratingModel = new RatingViewModel()
            {
                Id = id,
                RatingCalculated = rating,
                UserRating = userRating
            };

            return this.PartialView("_RatingPartial", ratingModel);
        }

        [Authorize]
        [AjaxOnly]
        public JsonResult Rate(int id, int rate)
        {
            if(rate < Constants.MinRating || Constants.MaxRating < rate)
            {
                return this.Json(new { error = true, message = Constants.RatingErrorMessage }, JsonRequestBehavior.AllowGet);
            }
            var userId = this.userProvider.GetUserId();
            this.ratingsService.RateBook(id, userId, rate);
            var rating = this.booksService.GetBookRating(id);
            
            return this.Json(new { success = true, rating = rating }, JsonRequestBehavior.AllowGet);
        }
    }
}