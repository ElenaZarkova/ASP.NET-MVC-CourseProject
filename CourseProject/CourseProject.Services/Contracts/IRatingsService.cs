namespace CourseProject.Services.Contracts
{
    public interface IRatingsService
    {
        void RateBook(int bookId, string userId, int rate);
    }
}