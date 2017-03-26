using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Ninject.Modules;
using CourseProject.Web.Identity;
using CourseProject.Web.Identity.Contracts;

namespace CourseProject.Web.App_Start.NinjectModules
{
    public class IdentityNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IApplicationSignInManager>().ToMethod(_ => HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>());
            this.Bind<IApplicationUserManager>().ToMethod(_ => HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>());
        }
    }
}