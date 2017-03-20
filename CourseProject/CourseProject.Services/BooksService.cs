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
            if (data == null)
            {
                throw new ArgumentNullException("data");
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
                .OrderByDescending(x => x.Ratings.Sum(y => y.Value) / (double)x.Ratings.Count)
                .Take(count)
                .ToList();

            return books;
        }

        public Book GetById(int id)
        {
            var book = this.data.Books.All
                .Where(x => x.Id == id)
                .Include(x => x.Genre)
                .FirstOrDefault();

            return book;
        }

        public double GetBookRating(int id)
        {
            // TODO: should it be in one query ??
            var book = this.data.Books.All
                .Where(x => x.Id == id)
                .Include(x=>x.Ratings)
                .FirstOrDefault();

            if(book != null)
            {
                return book.RatingCalculated;
            }
            else
            {
                return 0;
            }
        }

        public IEnumerable<Book> SearchBooks(string searchWord, string orderProperty, IEnumerable<int> genreIds, int page = 1, int booksPerPage = 9)
        {
            var skip = (page - 1) * booksPerPage;

            var books = this.data.Books.All;

            if(searchWord != null)
            {
                books = books.Where(x => x.Title.Contains(searchWord) || x.Author.Contains(searchWord));
            }

            if(genreIds != null && genreIds.Any())
            {
                books = books.Where(x => genreIds.Contains(x.GenreId));
            }

            orderProperty = orderProperty == null ? string.Empty : orderProperty.ToLower();
            switch (orderProperty)
            {
                case "author": books = books.OrderBy(x => x.Author); break; 
                case "year": books = books.OrderByDescending(x => x.PublishedOn.Year); break; 
                default: books = books.OrderBy(x => x.Title); break;
            }

            var resultBooks = books
                .Skip(skip)
                .Take(booksPerPage)
                .ToList();

            return resultBooks;
        }
    }
}
