using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Ninject.Modules;
using Ninject.Web.Common;
using CourseProject.Web.Identity.Contracts;
using CourseProject.Web.Identity;

namespace CourseProject.Web.App_Start.NinjectModules
{
    public class IdentityNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IApplicationSignInManager>().ToMethod(_ => HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>());
            this.Bind<IApplicationUserManager>().ToMethod(_ => HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>());
            this.Bind<IUserProvider>().To<UserProvider>().InRequestScope();
        }
    }
}