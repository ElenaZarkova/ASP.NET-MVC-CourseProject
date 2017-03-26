using System.Web;
using CourseProject.Web.Common.Providers.Contracts;

namespace CourseProject.Web.Common.Providers
{
    public class ServerProvider : IServerProvider
    {
        public string MapPath(string relativePath)
        {
            return HttpContext.Current.Server.MapPath(relativePath);
        }
    }
}