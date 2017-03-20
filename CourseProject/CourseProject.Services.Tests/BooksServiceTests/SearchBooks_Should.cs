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
    public class SearchBooks_Should
    {
        [TestCase("1")]
        [TestCase("aaa")]
        public void FilterBySearchWordRegardingTitleAndAuthor(string searchWord)
        {
            // Arrange
            var mockedData = new Mock<IBetterReadsData>();
            var booksData = new List<Book>
            {
                new Book() { Title = "some-digits-1", Author = ""},
                new Book() { Title = "", Author = "aaaabbbbbb"},
                new Book() { Title = "random", Author = "strings"},
            }.AsQueryable();

            mockedData.Setup(x => x.Books.All).Returns(booksData);

            var service = new BooksService(mockedData.Object);

            // Act
            var books = service.SearchBooks(searchWord, null, null, 1, 10);

            // Assert
            var expected = booksData.Where(x => x.Title.Contains(searchWord) || x.Author.Contains(searchWord)).ToList();
            CollectionAssert.AreEquivalent(expected, books);
        }

        [TestCase(1)]
        [TestCase(2, 4)]
        public void FilterByGenres(params int[] genreIds)
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
            var books = service.SearchBooks(null, genreIds, null, 1, 10);

            // Assert
            var expected = booksData.Where(x => genreIds.Contains(x.GenreId)).ToList();
            CollectionAssert.AreEquivalent(expected, books);
        }

        [Test]
        public void FilterBySearchWordAndGenres()
        {
            // Arrange
            var searchWord = "abc";
            var genreIds = new int[] { 1, 3 };
            var mockedData = new Mock<IBetterReadsData>();

            // only the first two answer the conditions
            var booksData = new List<Book>
            {
                new Book() {Title="", Author="abc-blabal", GenreId = 1 },
                new Book() {Title="abc", Author="", GenreId = 3 },
                new Book() {Title="", Author="", GenreId = 3 },
                new Book() {Title="abc", Author="", GenreId = 4 }
            }.AsQueryable();

            mockedData.Setup(x => x.Books.All).Returns(booksData);

            var service = new BooksService(mockedData.Object);

            // Act
            var books = service.SearchBooks(searchWord, genreIds, null, 1, 10);

            // Assert
            var expected = booksData
                .Where(x => x.Title.Contains(searchWord) || x.Author.Contains(searchWord))
                .Where(x => genreIds.Contains(x.GenreId)).ToList();

            CollectionAssert.AreEquivalent(expected, books);
        }

        [Test]
        public void ReturnWholeCollection_WhenFiltersAreNull()
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
            var books = service.SearchBooks(null, null, null, 1, 10);

            // Assert
            CollectionAssert.AreEquivalent(booksData.ToList(), books);
        }
        
        [Test]
        public void ReturnCorrectlyOrderedCollection()
        {
            // Arrange
            var mockedData = new Mock<IBetterReadsData>();
            var booksData = new List<Book>
            {
                new Book() { Author = "zzz" },
                new Book() { Author = "vvv" },
                new Book() { Author = "eee" },
                new Book() { Author = "aaa" }
            }.AsQueryable();

            mockedData.Setup(x => x.Books.All).Returns(booksData);

            var service = new BooksService(mockedData.Object);

            // Act
            var books = service.SearchBooks(null, null, "author", 1, 10);

            // Assert
            var expected = booksData.OrderBy(x => x.Author).ToList();
            CollectionAssert.AreEqual(expected, books);
        }

        [Test]
        public void OrderBooksInDescendingWhenTheValueIsYear()
        {
            // Arrange
            var mockedData = new Mock<IBetterReadsData>();
            var booksData = new List<Book>
            {
                new Book() { PublishedOn = new DateTime(2010,5,7) },
                new Book() { PublishedOn = new DateTime(2013,5,7) },
                new Book() { PublishedOn = new DateTime(2018,5,7) },
                new Book() { PublishedOn = new DateTime(2011,5,7) }
            }.AsQueryable();

            mockedData.Setup(x => x.Books.All).Returns(booksData);

            var service = new BooksService(mockedData.Object);

            // Act
            var books = service.SearchBooks(null, null, "year", 1, 10);

            // Assert
            var expected = booksData.OrderByDescending(x => x.PublishedOn.Year).ToList();
            CollectionAssert.AreEqual(expected, books);
        }

        [Test]
        public void OrderByTitle_WhenArgumentIsIncorrect()
        {
            // Arrange
            var mockedData = new Mock<IBetterReadsData>();
            var booksData = new List<Book>
            {
                new Book() { Title = "zzz" },
                new Book() { Title = "vvv" },
                new Book() { Title = "eee" },
                new Book() { Title = "aaa" }
            }.AsQueryable();

            mockedData.Setup(x => x.Books.All).Returns(booksData);

            var service = new BooksService(mockedData.Object);

            // Act
            var books = service.SearchBooks(null, null, "non-existing prop", 1, 10);

            // Assert
            var expected = booksData.OrderBy(x => x.Title).ToList();
            CollectionAssert.AreEqual(expected, books);
        }

        [Test]
        public void OrderAndFilterCorrectly()
        {
            // Arrange
            var searchWord = "z";
            var genreIds = new int[] { 2, 3 };
            var mockedData = new Mock<IBetterReadsData>();

            // only the first 2 answer the conditions
            var booksData = new List<Book>
            {
                new Book() { Title = "zzz", GenreId = 2, Author = "" },
                new Book() { Title = "zaa", GenreId = 3, Author = "" },
                new Book() { Title = "eee", GenreId = 1, Author = "" },
                new Book() { Title = "zaa", GenreId = 1, Author = "" }
            }.AsQueryable();

            mockedData.Setup(x => x.Books.All).Returns(booksData);

            var service = new BooksService(mockedData.Object);

            // Act
            var books = service.SearchBooks("z", genreIds, "title", 1, 10);

            // Assert
            var expected = booksData
                .Where(x => x.Title.Contains(searchWord) || x.Author.Contains(searchWord))
                .Where(x => genreIds.Contains(x.GenreId))
                .OrderBy(x=>x.Title)
                .ToList();
            CollectionAssert.AreEqual(expected, books);
        }
        
        [Test]
        public void SkipCorrectNumberOfElements()
        {
            int page = 2;
            int perPage = 3;
            var mockedData = new Mock<IBetterReadsData>();
            var mockedLastButOneBook = new Mock<Book>();
            var mockedLastBook = new Mock<Book>();
            var booksData = new List<Book>
            {
                new Mock<Book>().Object,
                new Mock<Book>().Object,
                new Mock<Book>().Object,
                new Mock<Book>().Object,
                new Mock<Book>().Object
            }.AsQueryable();

            mockedData.Setup(x => x.Books.All).Returns(booksData);

            var service = new BooksService(mockedData.Object);

            // Act
            var books = service.SearchBooks(null, null, null, page, perPage);

            // Assert
            var expected = booksData.OrderBy(x => x.Title).Skip(3).Take(perPage);
            CollectionAssert.AreEqual(expected, books);
        }
        
        [Test]
        public void TakeCorrectNumberOfElements()
        {
            int page = 3;
            int perPage = 2;
            var mockedData = new Mock<IBetterReadsData>();
            var mockedLastButOneBook = new Mock<Book>();
            var mockedLastBook = new Mock<Book>();
            var booksData = new List<Book>
            {
                new Mock<Book>().Object,
                new Mock<Book>().Object,
                new Mock<Book>().Object,
                new Mock<Book>().Object,
                new Mock<Book>().Object,
                new Mock<Book>().Object,
                new Mock<Book>().Object,
                new Mock<Book>().Object
            }.AsQueryable();

            mockedData.Setup(x => x.Books.All).Returns(booksData);

            var service = new BooksService(mockedData.Object);

            // Act
            var books = service.SearchBooks(null, null, null, page, perPage);

            // Assert
            var expected = booksData.OrderBy(x => x.Title).Skip(4).Take(perPage);
            CollectionAssert.AreEqual(expected, books);
        }
    }
}
