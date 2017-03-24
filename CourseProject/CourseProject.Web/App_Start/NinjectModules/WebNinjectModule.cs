using System.Web;
using Ninject.Modules;
using CourseProject.Web.Hubs;

namespace CourseProject.Web.App_Start.NinjectModules
{
    public class WebNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<HttpServerUtility>().ToSelf();
            this.Bind<ChatHub>().ToSelf();
        }
    }
}