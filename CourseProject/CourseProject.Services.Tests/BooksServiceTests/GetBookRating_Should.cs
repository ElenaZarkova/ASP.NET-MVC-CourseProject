using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using CourseProject.Data.Contracts;
using CourseProject.Models;
using CourseProject.Services.Tests.BooksServiceTests.Mocks;

namespace CourseProject.Services.Tests.BooksServiceTests
{
    [TestFixture]
    public class GetBookRating_Should
    {
        [Test]
        public void ReturnRatingOfTheCorrectBook()
        {
            // Arrange
            int id = 5;
            int expectedRating = 10;
            var mockedData = new Mock<IBetterReadsData>();
            var mockedBook1 = new BookMockedRating();
            mockedBook1.Id = 3;
            
            var mockedBook2 = new BookMockedRating();
            mockedBook2.Id = id;
            mockedBook2.SetRating(expectedRating);

            var booksData = new List<Book>
            {
                 mockedBook1,
                 mockedBook2 
            }.AsQueryable();

            mockedData.Setup(x => x.Books.All).Returns(booksData);

            var service = new BooksService(mockedData.Object);

            // Act
            var rating = service.GetBookRating(id);

            // Assert
            Assert.AreEqual(expectedRating, rating);
        }

        [Test]
        public void ReturnZeroWhenNoBookIsFound()
        {
            var mockedData = new Mock<IBetterReadsData>();

            mockedData.Setup(x => x.Books.All).Returns(new List<Book>().AsQueryable());

            var service = new BooksService(mockedData.Object);

            // Act
            var rating = service.GetBookRating(345);

            // Assert
            Assert.AreEqual(0, rating);
        }
    }
}
