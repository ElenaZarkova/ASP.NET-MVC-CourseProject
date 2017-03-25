using AutoMapper;

namespace CourseProject.ViewModels.Mapping
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IMapperConfigurationExpression config);
    }
}