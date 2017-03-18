using Ninject.Modules;
using CourseProject.Web.Mapping;

namespace CourseProject.Web.App_Start.NinjectModules
{
    public class AutoMapperModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IMapperAdapter>().To<MapperAdapter>();
        }
    }
}