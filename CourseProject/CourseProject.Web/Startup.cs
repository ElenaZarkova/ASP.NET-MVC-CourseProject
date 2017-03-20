using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using CourseProject.Web.App_Start.NinjectModules;

[assembly: OwinStartupAttribute(typeof(CourseProject.Web.Startup))]
namespace CourseProject.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
