using CourseProject.Data.Repositories;
using CourseProject.Models;

namespace CourseProject.Data.Contracts
{
    public interface IBetterReadsData
    {
        IEfRepository<Book> Books { get; }

        IEfRepository<Genre> Genres { get; }

        IEfRepository<Rating> Ratings { get; }

        IEfRepository<User> Users { get; }

        void SaveChanges();
    }
}