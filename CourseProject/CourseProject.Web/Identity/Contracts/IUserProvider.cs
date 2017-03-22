namespace CourseProject.Web.Identity.Contracts
{
    public interface IUserProvider
    {
        string GetUserId();
        string GetUsername();
    }
}