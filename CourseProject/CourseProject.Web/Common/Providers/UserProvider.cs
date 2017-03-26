using System.Web;
using Microsoft.AspNet.Identity;
using CourseProject.Web.Common.Providers.Contracts;

namespace CourseProject.Web.Common.Providers
{
    public class UserProvider : IUserProvider
    {
        public string GetUserId()
        {
            return HttpContext.Current.User.Identity.GetUserId();
        }

        public string GetUsername()
        {
            return HttpContext.Current.User.Identity.GetUserName();
        }
    }
}