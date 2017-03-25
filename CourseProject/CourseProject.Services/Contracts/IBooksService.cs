using System.Collections.Generic;
using CourseProject.Models;
using CourseProject.ViewModels.Admin.AddBook;

namespace CourseProject.Services.Contracts
{
    public interface IBooksService
    {
        bool BookWithTitleExists(string title);

        int AddBook(BookModel bookModel, string fileName);

        IEnumerable<Book> GetHighestRatedBooks(int count);

        Book GetById(int id);

        double GetBookRating(int id);

        IEnumerable<Book> SearchBooks(string searchWord, IEnumerable<int> genreIds, string orderProperty, int page = 1, int numberOfPages = 9);

        int GetBooksCount(string searchWord, IEnumerable<int> genreIds);
    }
}