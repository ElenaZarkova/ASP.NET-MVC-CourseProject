using System;
using System.Web;
using CourseProject.Web.Common.Providers.Contracts;

namespace CourseProject.Web.Common.Providers
{
    public class ServerProvider : IServerProvider
    {
        private readonly HttpContextBase httpContext;

        public ServerProvider(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            this.httpContext = httpContext;
        }

        public string MapPath(string relativePath)
        {
            return this.httpContext.Server.MapPath(relativePath);
        }
    }
}