using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using CourseProject.Data.Contracts;
using CourseProject.Models;

namespace CourseProject.Services.Tests.BooksServiceTests
{
    [TestFixture]
    public class GetBookById_Should
    {
        [TestCase(2)]
        [TestCase(78)]
        public void ReturnCorrectBook(int id)
        {
            // Arrange
            var mockedData = new Mock<IBetterReadsData>();
            var expectedBook = new Book();
            expectedBook.Id = id;
            var books = new List<Book>
            {
                 new Mock<Book>().Object,
                 new Mock<Book>().Object,
                 expectedBook,
                 new Mock<Book>().Object,
                 new Mock<Book>().Object,
            }.AsQueryable();

            mockedData.Setup(x => x.Books.All).Returns(books);

            var service = new BooksService(mockedData.Object);

            // Act
            var book = service.GetById(id);

            // Assert
            Assert.AreEqual(expectedBook, book);
        }

        [TestCase(-2)]
        [TestCase(78)]
        public void ReturnNullWhenBookNotFound(int id)
        {
            // Arrange
            var mockedData = new Mock<IBetterReadsData>();
            var books = new List<Book>
            {
                 new Mock<Book>().Object,
                 new Mock<Book>().Object,
                 new Mock<Book>().Object,
                 new Mock<Book>().Object
            }.AsQueryable();

            mockedData.Setup(x => x.Books.All).Returns(books);

            var service = new BooksService(mockedData.Object);

            // Act
            var book = service.GetById(id);

            // Assert
            Assert.IsNull(book);
        }
    }
}
