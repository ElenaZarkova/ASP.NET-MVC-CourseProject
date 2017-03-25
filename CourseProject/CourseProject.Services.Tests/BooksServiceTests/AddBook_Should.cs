using System;
using Moq;
using NUnit.Framework;
using CourseProject.Services;
using CourseProject.Data.Contracts;
using CourseProject.Models;

namespace CourseProject.Services.Tests.BooksServiceTests
{
    [TestFixture]
    public class AddBook_Should
    {
        //[Test]
        //public void CallDataBooksAddMethod()
        //{
        //    // Arrange
        //    var mockedBook = new Mock<Book>();
        //    var mockedData = new Mock<IBetterReadsData>();
        //    mockedData.Setup(x => x.Books.Add(It.IsAny<Book>())).Verifiable();

        //    var service = new BooksService(mockedData.Object);

        //    // Act
        //    service.AddBook(mockedBook.Object);

        //    // Assert
        //    mockedData.Verify(x => x.Books.Add(It.IsAny<Book>()), Times.Once);
        //}

        //[Test]
        //public void CallDataBooksAddMethodWithCorrectBook()
        //{
        //    // Arrange
        //    var mockedBook = new Mock<Book>();
        //    var mockedData = new Mock<IBetterReadsData>();
        //    mockedData.Setup(x => x.Books.Add(mockedBook.Object)).Verifiable();

        //    var service = new BooksService(mockedData.Object);

        //    // Act
        //    service.AddBook(mockedBook.Object);

        //    // Assert
        //    mockedData.Verify(x => x.Books.Add(mockedBook.Object), Times.Once);
        //}

        //[Test]
        //public void CallDataSaveChanges()
        //{
        //    // Arrange
        //    var mockedBook = new Mock<Book>();
        //    var mockedData = new Mock<IBetterReadsData>();
        //    mockedData.Setup(x => x.Books.Add(It.IsAny<Book>())).Verifiable();
        //    mockedData.Setup(x => x.SaveChanges()).Verifiable();
        //    var service = new BooksService(mockedData.Object);

        //    // Act
        //    service.AddBook(mockedBook.Object);

        //    // Assert
        //    mockedData.Verify(x => x.SaveChanges(), Times.Once);
        //}
    }
}
