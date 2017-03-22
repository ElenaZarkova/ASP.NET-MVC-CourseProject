using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CourseProject.Web.Identity.Contracts;
using Ninject.Modules;
using CourseProject.Web.Identity;
using Ninject.Web.Common;

namespace CourseProject.Web.App_Start.NinjectModules
{
    public class IdentityNinjectModule : NinjectModule
    {
        public override void Load()
        {
            // maybe
            // this.Bind<HttpContext>().ToMethod(ninjectContext => HttpContext.Current).InRequestScope();
            this.Bind<IUserProvider>().To<UserProvider>().InRequestScope();
        }
    }
}