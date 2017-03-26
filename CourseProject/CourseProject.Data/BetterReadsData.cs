using System;
using CourseProject.Data.Contracts;
using CourseProject.Data.Repositories;
using CourseProject.Models;

namespace CourseProject.Data
{
    public class BetterReadsData : IBetterReadsData
    {
        private readonly IBetterReadsDbContext dbContext;

        public BetterReadsData(
            IBetterReadsDbContext dbContext,
            IEfRepository<User> usersRepo,
            IEfRepository<Book> booksRepo,
            IEfRepository<Genre> genresRepo,
            IEfRepository<Rating> ratingsRepo)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("Database context cannot be null.");
            }

            if (usersRepo == null)
            {
                throw new ArgumentNullException("Users entity framework repository cannot be null.");
            }

            if (booksRepo == null)
            {
                throw new ArgumentNullException("Books entity framework repository cannot be null.");
            }

            if (genresRepo == null)
            {
                throw new ArgumentNullException("Genres entity framework repository cannot be null.");
            }

            if (ratingsRepo == null)
            {
                throw new ArgumentNullException("Ratings entity framework repository cannot be null.");
            }

            this.dbContext = dbContext;
            this.Users = usersRepo;
            this.Books = booksRepo;
            this.Genres = genresRepo;
            this.Ratings = ratingsRepo;
        }

        public IEfRepository<User> Users { get; private set; }

        public IEfRepository<Book> Books { get; private set; }

        public IEfRepository<Genre> Genres { get; private set; }

        public IEfRepository<Rating> Ratings { get; private set; }

        public void SaveChanges()
        {
            this.dbContext.SaveChanges();
        }
    }
}
