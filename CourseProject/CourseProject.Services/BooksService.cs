using System;
using System.Collections.Generic;
using System.Linq;
using CourseProject.Data.Contracts;
using CourseProject.Models;
using CourseProject.Services.Contracts;
using System.Data.Entity;

namespace CourseProject.Services
{
    public class BooksService : IBooksService
    {
        private readonly IBetterReadsData data;

        public BooksService(IBetterReadsData data)
        {
            if(data == null)
            {
                throw new ArgumentNullException("Better reads data cannot be null.");
            }

            this.data = data;
        }
        
        public void AddBook(Book book)
        {
            this.data.Books.Add(book);
            this.data.SaveChanges();
        }

        public IEnumerable<Book> GetHighestRatedBooks(int count)
        {
            var books = this.data.Books.All
                .OrderByDescending(x => (double)x.Ratings.Count / x.Ratings.Sum(y => y.Value))
                .Take(count)
                .ToList();

            return books;
        }

        public Book GetById(int id)
        {
            var book = this.data.Books.All
                .Where(x => x.Id == id)
                .Include(x => x.Genre)
                .Include(x => x.Ratings)
                .FirstOrDefault();

            return book;
        }
    }
}
