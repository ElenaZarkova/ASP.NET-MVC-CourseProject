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
    public class BookWithTitleExists_Should
    {
        [Test]
        public void ReturnTrueWhenBookFound()
        {
            // Arrange
            var title = "Title";
            var mockedData = new Mock<IBetterReadsData>();
            var matchedBook = new Book() { Title = title };
            var books = new List<Book>
            {
                 new Mock<Book>().Object,
                 new Mock<Book>().Object,
                 matchedBook
            }.AsQueryable();

            mockedData.Setup(x => x.Books.All).Returns(books);

            var service = new BooksService(mockedData.Object);

            // Act
            var exists = service.BookWithTitleExists(title);

            // Assert
            Assert.IsTrue(exists);
        }

        [Test]
        public void ReturnFalseWhenBookNotFound()
        {
            // Arrange
            var mockedData = new Mock<IBetterReadsData>();
            var books = new List<Book>
            {
                 new Mock<Book>().Object,
                 new Mock<Book>().Object,
                 new Mock<Book>().Object
            }.AsQueryable();

            mockedData.Setup(x => x.Books.All).Returns(books);

            var service = new BooksService(mockedData.Object);

            // Act
            var exists = service.BookWithTitleExists("Title");

            // Assert
            Assert.IsFalse(exists);
        }
    }
}