﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CourseProject.Services.Contracts;
using CourseProject.ViewModels.Admin.AddBook;
using CourseProject.Web.Common;
using CourseProject.Web.Common.Providers.Contracts;
using CourseProject.Web.Mapping;

namespace CourseProject.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = Constants.AdminRole)]
    public class AddBookController : Controller
    {
        private readonly IBooksService booksService;
        private readonly IGenresService genresService;
        private readonly IUserProvider userProvider;
        private readonly IServerProvider serverProvider;
        private readonly ICacheProvider cacheProvider;
        private readonly IMapperAdapter mapper;

        public AddBookController(
            IBooksService booksService, 
            IGenresService genresService, 
            IUserProvider userProvider,
            IServerProvider serverProvider,
            ICacheProvider cacheProvider,
            IMapperAdapter mapper)
        {
            if (booksService == null)
            {
                throw new ArgumentNullException("booksService");
            }

            if (genresService == null)
            {
                throw new ArgumentNullException("genresService");
            }

            if (userProvider == null)
            {
                throw new ArgumentNullException("userProvider");
            }

            if (serverProvider == null)
            {
                throw new ArgumentNullException("serverProvider");
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
            this.genresService = genresService;
            this.userProvider = userProvider;
            this.serverProvider = serverProvider;
            this.cacheProvider = cacheProvider;
            this.mapper = mapper;
        }

        public ActionResult Index()
        {
            var model = new AddBookViewModel();
            model.Genres = this.GetGenres();
            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Index([Bind(Exclude = "Genres")]AddBookViewModel bookSubmitModel)
        {
            if (!ModelState.IsValid)
            {
                bookSubmitModel.Genres = this.GetGenres();
                return this.View(bookSubmitModel);
            }

            if (!this.IsImageFile(bookSubmitModel.CoverFile))
            {
                this.ModelState.AddModelError("CoverFile", Constants.CoverFileErrorMessage);
                bookSubmitModel.Genres = this.GetGenres();
                return this.View(bookSubmitModel);
            }

            if (this.booksService.BookWithTitleExists(bookSubmitModel.Title))
            {
                this.ModelState.AddModelError("Title", Constants.TitleExistsErrorMessage);
                bookSubmitModel.Genres = this.GetGenres();
                return this.View(bookSubmitModel);
            }

            var filename = bookSubmitModel.CoverFile.FileName;
            var path = this.serverProvider.MapPath(Constants.ImagesRelativePath + filename);
            bookSubmitModel.CoverFile.SaveAs(path);

            var bookModel = this.mapper.Map<BookModel>(bookSubmitModel);
            var bookId = this.booksService.AddBook(bookModel, filename);

            this.TempData.Add(Constants.AddBookSuccessKey, Constants.AddBookSuccessMessage);
            return this.Redirect($"/book/{bookId}");
        }

        private bool IsImageFile(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return false;
            }

            var contentType = file.ContentType.ToLower();
            var result = contentType == "image/jpg" 
                || contentType == "image/jpeg" 
                || contentType == "image/png";

            return result;
        }

        private IEnumerable<SelectListItem> GetGenres()
        {
            IEnumerable<SelectListItem> genres;
            var cachedGenres = this.cacheProvider.GetValue(Constants.GenresCache);
            if (cachedGenres != null)
            {
                genres = (IEnumerable<SelectListItem>)cachedGenres;
            }
            else
            {
                genres = this.genresService.GetAllGenres().Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList();
                
                // maxvalue because api
                this.cacheProvider.InsertWithSlidingExpiration(Constants.GenresCache, genres, Constants.GenresExpirationInMinutes);
            }

            return genres;
        }
    }
}