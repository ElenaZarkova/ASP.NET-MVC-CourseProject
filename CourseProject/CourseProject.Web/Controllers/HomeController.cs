using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using CourseProject.Data.Contracts;
using CourseProject.Web.Models;
using CourseProject.Models;

namespace CourseProject.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBetterReadsData data;

        public HomeController(IBetterReadsData data)
        {
            this.data = data;
        }

        public ActionResult Index()
        {
            //Genre genre = null;
            //var genreviewModel = (GenreViewModel)AutoMapper.Mapper.Map(genre, typeof(Genre), typeof(GenreViewModel));
            // var genreviewModel = AutoMapper.Mapper.Map<GenreViewModel>(genre);
            var genreviewModel = this.data.Genres.All.ProjectTo<GenreViewModel>().FirstOrDefault();
            this.ViewBag.Name = genreviewModel.Name;
            this.ViewBag.NameAndId = genreviewModel.NameAndId;
            return View();
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