using AutoMapper;

namespace CourseProject.Web.Mapping
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IMapperConfigurationExpression config);
    }
}