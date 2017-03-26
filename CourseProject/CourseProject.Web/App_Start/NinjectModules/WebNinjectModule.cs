using System.Web;
using Ninject.Modules;
using Ninject.Web.Common;
using CourseProject.Web.Hubs;
using CourseProject.Web.Common.Providers;
using CourseProject.Web.Common.Providers.Contracts;

namespace CourseProject.Web.App_Start.NinjectModules
{
    public class WebNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<HttpServerUtility>().ToSelf();
            this.Bind<ChatHub>().ToSelf();
            this.Bind<IUserProvider>().To<UserProvider>().InRequestScope();
            this.Bind<ICacheProvider>().To<CacheProvider>().InRequestScope();
            this.Bind<IServerProvider>().To<ServerProvider>().InRequestScope();
        }
    }
}