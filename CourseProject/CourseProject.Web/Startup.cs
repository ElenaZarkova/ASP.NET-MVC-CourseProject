using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CourseProject.Web.Startup))]
namespace CourseProject.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
