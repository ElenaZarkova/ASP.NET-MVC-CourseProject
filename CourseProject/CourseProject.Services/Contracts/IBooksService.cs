using System.Collections.Generic;
using CourseProject.Models;

namespace CourseProject.Services.Contracts
{
    public interface IBooksService
    {
        void AddBook(Book book);

        IEnumerable<Book> GetHighestRatedBooks(int count);
    }
}