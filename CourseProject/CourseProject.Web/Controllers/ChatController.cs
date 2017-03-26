using System.Web.Mvc;

namespace CourseProject.Web.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        // GET: Chat
        public ActionResult Index()
        {
            return this.View();
        }
    }
}