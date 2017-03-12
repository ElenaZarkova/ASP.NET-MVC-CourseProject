using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CourseProject.Data.Contracts;

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
            var name = this.data.Genres.All.First().Name;
            this.ViewBag.Name = name;
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