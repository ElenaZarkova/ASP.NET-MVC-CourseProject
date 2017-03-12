using System.Linq;

namespace CourseProject.Data.Repositories
{
    public interface IEfRepository<T>
        where T : class
    {
        IQueryable<T> All { get; }

        T GetById(object id);

        void Add(T entity);

        void Update(T entity);
        
        void Delete(T entity);
    }
}
