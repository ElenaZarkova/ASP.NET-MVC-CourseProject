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
    public class GetHighestRatedBooks_Should
    {
        [TestCase(1)]
        [TestCase(2)]
        public void ReturnCorrectBookCount(int count)
        {
            // Arrange
            var mockedData = new Mock<IBetterReadsData>();
            var mockedBook1 = new Mock<Book>();
            var ratingsBook1 = new List<Rating>()
            {
                new Rating() { Value = 1 }
            };
            mockedBook1.Setup(x => x.Ratings).Returns(ratingsBook1);

            var mockedBook2 = new Mock<Book>();
            var ratingsBook2 = new List<Rating>()
            {
                new Rating() { Value = 3 },
                new Rating() { Value = 4 }
            };
            mockedBook2.Setup(x => x.Ratings).Returns(ratingsBook2);

            var booksData = new List<Book>
            {
                 mockedBook1.Object,
                 mockedBook2.Object
            }.AsQueryable();

            mockedData.Setup(x => x.Books.All).Returns(booksData);

            var service = new BooksService(mockedData.Object);

            // Act
            var books = service.GetHighestRatedBooks(count);

            // Assert
            Assert.AreEqual(count, books.Count());
        }
        
        [Test]
        public void ReturnBooksInCorrectOrder()
        {
            // Arrange
            var mockedData = new Mock<IBetterReadsData>();
            var mockedBook1 = new Mock<Book>();
            var ratingsBook1 = new List<Rating>()
            {
                new Rating() { Value = 1 }
            };
            mockedBook1.Setup(x => x.Ratings).Returns(ratingsBook1);

            var mockedBook2 = new Mock<Book>();
            var ratingsBook2 = new List<Rating>()
            {
                new Rating() { Value = 3 },
                new Rating() { Value = 4 }
            };
            mockedBook2.Setup(x => x.Ratings).Returns(ratingsBook2);

            var booksData = new List<Book>
            {
                 mockedBook1.Object,
                 mockedBook2.Object
            }.AsQueryable();

            mockedData.Setup(x => x.Books.All).Returns(booksData);

            var service = new BooksService(mockedData.Object);

            // Act
            var books = service.GetHighestRatedBooks(2).ToList();

            // Assert
            Assert.AreEqual(mockedBook2.Object, books[0]);
            Assert.AreEqual(mockedBook1.Object, books[1]);
        }

        [Test]
        public void ReturnAllBooksWhenCountIsGreaterThenTheirCount()
        {
            // Arrange
            var mockedData = new Mock<IBetterReadsData>();
            var mockedBook1 = new Mock<Book>();
            var ratingsBook1 = new List<Rating>()
            {
                new Rating() { Value = 1 }
            };
            mockedBook1.Setup(x => x.Ratings).Returns(ratingsBook1);

            var mockedBook2 = new Mock<Book>();
            var ratingsBook2 = new List<Rating>()
            {
                new Rating() { Value = 3 },
                new Rating() { Value = 4 }
            };
            mockedBook2.Setup(x => x.Ratings).Returns(ratingsBook2);

            var booksData = new List<Book>
            {
                 mockedBook1.Object,
                 mockedBook2.Object
            }.AsQueryable();

            mockedData.Setup(x => x.Books.All).Returns(booksData);

            var service = new BooksService(mockedData.Object);

            // Act
            var books = service.GetHighestRatedBooks(10).ToList();

            // Assert
            Assert.AreEqual(2, books.Count);
            Assert.AreEqual(mockedBook2.Object, books[0]);
            Assert.AreEqual(mockedBook1.Object, books[1]);
        }
    }
}
