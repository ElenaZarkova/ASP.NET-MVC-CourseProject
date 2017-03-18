namespace CourseProject.Web.Mapping
{
    public interface IMapperAdapter
    {
        TDestination Map<TDestination>(object source);
    }
}
