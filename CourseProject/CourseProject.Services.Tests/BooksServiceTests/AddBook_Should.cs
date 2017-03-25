using System;
using Moq;
using NUnit.Framework;
using CourseProject.Services;
using CourseProject.Data.Contracts;
using CourseProject.Models;
using CourseProject.ViewModels.Admin.AddBook;

namespace CourseProject.Services.Tests.BooksServiceTests
{
    [TestFixture]
    public class AddBook_Should
    {
        [Test]
        public void CallDataBooksAddMethod()
        {
            // Arrange
            var bookModel = new BookModel();
            var mockedData = new Mock<IBetterReadsData>();
            mockedData.Setup(x => x.Books.Add(It.IsAny<Book>())).Verifiable();

            var service = new BooksService(mockedData.Object);

            // Act
            service.AddBook(bookModel, "filename");

            // Assert
            mockedData.Verify(x => x.Books.Add(It.IsAny<Book>()), Times.Once);
        }

        [Test]
        public void CallDataBooksAddMethodWithBookWithCorrectProperties()
        {
            // Arrange
            var bookModel = new BookModel()
            {
                Title = "Title",
                Author = "TheAuthor",
                Description = "Some description",
                GenreId = 5,
                PublishedOn = new DateTime(2014, 3, 5)
            };
            var filename = "pic.jpg";
            var mockedData = new Mock<IBetterReadsData>();
            Book addedBook = null;
            mockedData.Setup(x => x.Books.Add(It.IsAny<Book>()))
                .Callback((Book book) => addedBook = book);

            var service = new BooksService(mockedData.Object);

            // Act
            service.AddBook(bookModel, filename);

            // Assert
            Assert.AreEqual(bookModel.Title, addedBook.Title);
            Assert.AreEqual(bookModel.Author, addedBook.Author);
            Assert.AreEqual(bookModel.Description, addedBook.Description);
            Assert.AreEqual(bookModel.GenreId, addedBook.GenreId);
            Assert.AreEqual(bookModel.PublishedOn, addedBook.PublishedOn);
            Assert.AreEqual(filename, addedBook.CoverFile);
        }

        [Test]
        public void CallDataSaveChanges()
        {
            // Arrange
            var mockedData = new Mock<IBetterReadsData>();
            mockedData.Setup(x => x.Books.Add(It.IsAny<Book>())).Verifiable();
            mockedData.Setup(x => x.SaveChanges()).Verifiable();
            var service = new BooksService(mockedData.Object);

            // Act
            service.AddBook(new BookModel(), "filename");

            // Assert
            mockedData.Verify(x => x.SaveChanges(), Times.Once);
        }

        //[Test]
        //public void ReturnCorrectBookId()
        //{
        //    // Arrange
        //    var bookModel = new BookModel();
        //    var mockedData = new Mock<IBetterReadsData>();
        //    var service = new BooksService(mockedData.Object);

        //    // Act
        //    service.AddBook(new BookModel(), "filename");

        //    // Assert
        //    mockedData.Verify(x => x.SaveChanges(), Times.Once);
        //}
    }
}
