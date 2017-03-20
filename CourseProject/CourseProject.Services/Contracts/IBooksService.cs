using System.Collections.Generic;
using CourseProject.Models;

namespace CourseProject.Services.Contracts
{
    public interface IBooksService
    {
        void AddBook(Book book);

        IEnumerable<Book> GetHighestRatedBooks(int count);

        Book GetById(int id);

        double GetBookRating(int id);

        IEnumerable<Book> SearchBooks(string searchWord, IEnumerable<int> genreIds, string orderProperty, int page = 1, int numberOfPages = 9);

        int GetBooksCount(string searchWord, IEnumerable<int> genreIds);
    }
}