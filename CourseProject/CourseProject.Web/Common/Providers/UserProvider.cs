using System.Web;
using Microsoft.AspNet.Identity;
using CourseProject.Web.Common.Providers.Contracts;

namespace CourseProject.Web.Common.Providers
{
    public class UserProvider : IUserProvider
    {
        private readonly HttpContextBase httpContext;

        public UserProvider(HttpContextBase httpContext)
        {
            this.httpContext = httpContext;
        }

        public string GetUserId()
        {
            return this.httpContext.User.Identity.GetUserId();
        }

        public string GetUsername()
        {
            return this.httpContext.User.Identity.GetUserName();
        }
    }
}