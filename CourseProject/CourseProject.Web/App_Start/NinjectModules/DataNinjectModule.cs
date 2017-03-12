using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Modules;
using Ninject.Web.Common;
using CourseProject.Data;
using CourseProject.Data.Contracts;
using CourseProject.Data.Repositories;

namespace CourseProject.Web.App_Start.NinjectModules
{
    public class DataNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IBetterReadsDbContext>().To<BetterReadsDbContext>().InRequestScope();
            this.Bind(typeof(IEfRepository<>)).To(typeof(EfRepository<>)).InRequestScope();
            this.Bind<IBetterReadsData>().To<BetterReadsData>().InRequestScope();
        }
    }
}