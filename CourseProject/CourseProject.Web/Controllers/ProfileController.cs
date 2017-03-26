using System.Web.Mvc;

namespace CourseProject.Web.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index()
        {
            return this.View();
        }
    }
}