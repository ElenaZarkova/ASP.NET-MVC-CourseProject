using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using CourseProject.Data.Contracts;
using CourseProject.Models;

namespace CourseProject.Services.Tests.BooksServiceTests
{
    [TestFixture]
    public class GetBooksCount_Should
    {
        [TestCase(1, "1")]
        [TestCase(2, "a")]
        public void ReturnCorrectcount_WhenFilterBySearchWord(int expectedCount, string searchWord)
        {
            // Arrange
            var mockedData = new Mock<IBetterReadsData>();
            var booksData = new List<Book>
            {
                new Book() { Title = "some-digits-1", Author = "" },
                new Book() { Title = "", Author = "aaaabbbbbb" },
                new Book() { Title = "random", Author = "strings" },
            }.AsQueryable();

            mockedData.Setup(x => x.Books.All).Returns(booksData);

            var service = new BooksService(mockedData.Object);

            // Act
            var count = service.GetBooksCount(searchWord, null);

            // Assert
            Assert.AreEqual(expectedCount, count);
        }

        [TestCase(1, 1)]
        [TestCase(2, 2, 4)]
        public void ReturnCorrectCount_WhenFilterByGenres(int expectedCount, params int[] genreIds)
        {
            // Arrange
            var mockedData = new Mock<IBetterReadsData>();
            var booksData = new List<Book>
            {
                new Book() { GenreId = 1 },
                new Book() { GenreId = 2 },
                new Book() { GenreId = 4 },
                new Book() { GenreId = 5 }
            }.AsQueryable();

            mockedData.Setup(x => x.Books.All).Returns(booksData);

            var service = new BooksService(mockedData.Object);

            // Act
            var count = service.GetBooksCount(null, genreIds);

            // Assert
            Assert.AreEqual(expectedCount, count);
        }

        [Test]
        public void ReturnCorrectCount_WhenFilterBySearchWordAndGenres()
        {
            // Arrange
            var searchWord = "abc";
            var genreIds = new int[] { 1, 3 };
            var mockedData = new Mock<IBetterReadsData>();

            // only the first two answer the conditions
            var booksData = new List<Book>
            {
                new Book() { Title = "", Author = "abc-blabal", GenreId = 1 },
                new Book() { Title = "abc", Author = "", GenreId = 3 },
                new Book() { Title = "", Author = "", GenreId = 3 },
                new Book() { Title = "abc", Author = "", GenreId = 4 }
            }.AsQueryable();

            mockedData.Setup(x => x.Books.All).Returns(booksData);

            var service = new BooksService(mockedData.Object);

            // Act
            var count = service.GetBooksCount(searchWord, genreIds);

            // Assert
            Assert.AreEqual(2, count);
        }

        [Test]
        public void ReturnCountOfAllElements_WhenFiltersAreNull()
        {
            // Arrange
            var mockedData = new Mock<IBetterReadsData>();

            var booksData = new List<Book>
            {
                new Mock<Book>().Object,
                new Mock<Book>().Object,
                new Mock<Book>().Object,
                new Mock<Book>().Object,
            }.AsQueryable();

            mockedData.Setup(x => x.Books.All).Returns(booksData);

            var service = new BooksService(mockedData.Object);

            // Act
            var count = service.GetBooksCount(null, null);

            // Assert
            Assert.AreEqual(booksData.Count(), count);
        }
    }
}
