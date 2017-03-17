using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseProject.Web.Controllers
{
    public class BookController : Controller
    {
        public ActionResult Index()
        {
            return this.View();
        }
    }
}