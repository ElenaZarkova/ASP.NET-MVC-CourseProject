﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CourseProject.Models;
using AutoMapper;
using CourseProject.Web.Models;
using CourseProject.Data.Contracts;
using CourseProject.Services.Contracts;
using System.IO;

namespace CourseProject.Web.Controllers
{
    [Authorize(Roles = RoleNames.Admin)]
    public class AddBookController : Controller
    {
        private readonly IBetterReadsData data;
        private readonly IBooksService booksService;

        public AddBookController(IBetterReadsData data, IBooksService booksService)
        {
            this.data = data;
            this.booksService = booksService;
        }

        public ActionResult Index()
        {
            var model = new AddBookViewModel();
            model.Genres = this.GetGenres();
            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(AddBookViewModel bookModel)
        {
            if (!ModelState.IsValid)
            {
                bookModel.Genres = this.GetGenres();
                return View(bookModel);
            }

            if (!this.IsImageFile(bookModel.CoverFile))
            {
                this.ModelState.AddModelError("CoverFile", "Cover photo should be an image file.");
                bookModel.Genres = this.GetGenres();
                return View(bookModel);
            }

            var book = this.GetBook(bookModel);
            this.booksService.AddBook(book);

            return this.Redirect("/home");
        }

        private Book GetBook(AddBookViewModel bookModel)
        {
            var filename = bookModel.CoverFile.FileName;
            var path = this.Server.MapPath($"~/Content/Images/{filename}");
            bookModel.CoverFile.SaveAs(path);

            // TODO: Should it be mapped
            var book = new Book()
            {
                Title = bookModel.Title,
                Author = bookModel.Author,
                Description = bookModel.Description,
                PublishedOn = bookModel.PublishedOn,
                GenreId = bookModel.GenreId,
                CoverFilePath = path
            };

            return book;
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
            var genres = this.data.Genres.All.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList();
            return genres;
        }
    }
}