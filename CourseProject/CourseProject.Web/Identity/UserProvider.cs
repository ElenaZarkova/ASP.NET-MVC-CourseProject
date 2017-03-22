using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using CourseProject.Web.Identity.Contracts;

namespace CourseProject.Web.Identity
{
    public class UserProvider : IUserProvider
    {
        private readonly HttpContext httpContext;

        public UserProvider(HttpContext httpContext)
        {
            if(httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            this.httpContext = httpContext;
        }

        public string GetUserId()
        {
            var id = this.httpContext.User.Identity.GetUserId();
            return id;
        }

        public string GetUsername()
        {
            var username = this.httpContext.User.Identity.GetUserName();
            return username;
        }
    }
}