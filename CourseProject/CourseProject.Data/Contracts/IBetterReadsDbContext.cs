using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using CourseProject.Models;

namespace CourseProject.Data.Contracts
{
    public interface IBetterReadsDbContext
    {
        IDbSet<User> Users { get; set; }
        
        IDbSet<Book> Books { get; set; }

        IDbSet<Genre> Genres { get; set; }

        IDbSet<Rating> Ratings { get; set; }
        
        IDbSet<T> Set<T>() where T : class;

        DbEntityEntry<T> Entry<T>(T entity) where T : class;

        int SaveChanges();
    }
}