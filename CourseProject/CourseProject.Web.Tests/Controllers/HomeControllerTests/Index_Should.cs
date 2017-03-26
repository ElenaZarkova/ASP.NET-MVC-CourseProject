using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;
using CourseProject.Models;
using CourseProject.Services.Contracts;
using CourseProject.Web.Mapping;
using CourseProject.Web.Controllers;
using CourseProject.Web.Common;
using CourseProject.Web.Common.Providers.Contracts;
using CourseProject.ViewModels;

namespace CourseProject.Web.Tests.Controllers.HomeControllerTests
{
    [TestFixture]
    public class Index_Should
    {
        [Test]
        public void CallCacheGetValueWithCorrectKey()
        {
            // Arrange
            var mockedBooksService = new Mock<IBooksService>();
            var mockedCacheProvider = new Mock<ICacheProvider>();
            var mockedMapper = new Mock<IMapperAdapter>();
            mockedCacheProvider.Setup(x => x.GetValue(It.IsAny<string>())).Verifiable();

            var controller = new HomeController(mockedBooksService.Object, mockedCacheProvider.Object, mockedMapper.Object);

            // Act 
            controller.Index();

            // Assert
            mockedCacheProvider.Verify(x => x.GetValue(Constants.TopBooksCache), Times.Once);
        }

        [Test]
        public void NotCallBooksService_WhenCacheIsNotNull()
        {
            // Arrange
            var mockedBooksService = new Mock<IBooksService>();
            var mockedCacheProvider = new Mock<ICacheProvider>();
            var mockedMapper = new Mock<IMapperAdapter>();
            mockedBooksService.Setup(x => x.GetHighestRatedBooks(It.IsAny<int>())).Verifiable();
            mockedCacheProvider.Setup(x => x.GetValue(It.IsAny<string>())).Returns(new List<BookViewModel>());

            var controller = new HomeController(mockedBooksService.Object, mockedCacheProvider.Object, mockedMapper.Object);

            // Act 
            controller.Index();

            // Assert
            mockedBooksService.Verify(x => x.GetHighestRatedBooks(It.IsAny<int>()), Times.Never);
        }

        [Test]
        public void CallBooksServiceHighestRatedBooks_WhenCacheIsNull()
        {
            // Arrange
            var mockedBooksService = new Mock<IBooksService>();
            var mockedCacheProvider = new Mock<ICacheProvider>();
            var mockedMapper = new Mock<IMapperAdapter>();
            mockedCacheProvider.Setup(x => x.GetValue(It.IsAny<string>())).Returns(null);
            mockedBooksService.Setup(x => x.GetHighestRatedBooks(It.IsAny<int>())).Returns(new List<Book>());

            var controller = new HomeController(mockedBooksService.Object, mockedCacheProvider.Object, mockedMapper.Object);

            // Act 
            controller.Index();

            // Assert
            mockedBooksService.Verify(x => x.GetHighestRatedBooks(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void CallBooksServiceHighestRatedBooksWithCorrectCount_WhenCacheIsNull()
        {
            // Arrange
            var mockedBooksService = new Mock<IBooksService>();
            var mockedCacheProvider = new Mock<ICacheProvider>();
            var mockedMapper = new Mock<IMapperAdapter>();
            mockedCacheProvider.Setup(x => x.GetValue(It.IsAny<string>())).Returns(null);
            mockedBooksService.Setup(x => x.GetHighestRatedBooks(It.IsAny<int>())).Returns(new List<Book>());

            var controller = new HomeController(mockedBooksService.Object, mockedCacheProvider.Object, mockedMapper.Object);

            // Act 
            controller.Index();

            // Assert
            mockedBooksService.Verify(x => x.GetHighestRatedBooks(Constants.TopBooksCount), Times.Once);
        }

        [Test]
        public void CallMapperWithCorrectCollection_WhenCacheIsNull()
        {
            // Arrange
            var mockedBooksService = new Mock<IBooksService>();
            var mockedCacheProvider = new Mock<ICacheProvider>();
            var mockedMapper = new Mock<IMapperAdapter>();

            mockedCacheProvider.Setup(x => x.GetValue(It.IsAny<string>())).Returns(null);

            var books = new List<Book>();
            mockedBooksService.Setup(x => x.GetHighestRatedBooks(It.IsAny<int>())).Returns(books);
            mockedMapper.Setup(x => x.Map<IEnumerable<BookViewModel>>(It.IsAny<IEnumerable<Book>>())).Verifiable();
            var controller = new HomeController(mockedBooksService.Object, mockedCacheProvider.Object, mockedMapper.Object);

            // Act 
            controller.Index();

            // Assert
            mockedMapper.Verify(x => x.Map<IEnumerable<BookViewModel>>(books), Times.Once);
        }

        [Test]
        public void CallCacheInsertWithCorrectBooks_WhenCacheIsNull()
        {
            // Arrange
            var mockedBooksService = new Mock<IBooksService>();
            var mockedCacheProvider = new Mock<ICacheProvider>();
            var mockedMapper = new Mock<IMapperAdapter>();

            var mappedBooks = new List<BookViewModel>();
            mockedMapper.Setup(x => x.Map<IEnumerable<BookViewModel>>(It.IsAny<IEnumerable<Book>>())).Returns(mappedBooks);

            IEnumerable<BookViewModel> cachedBooks = null;
            mockedCacheProvider.Setup(x => x.GetValue(It.IsAny<string>())).Returns(null);
            mockedCacheProvider.Setup(x => x.InsertWithAbsoluteExpiration(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<DateTime>()))
                .Callback((string key, object value, DateTime expiration) => cachedBooks = (IEnumerable<BookViewModel>)value);

            var controller = new HomeController(mockedBooksService.Object, mockedCacheProvider.Object, mockedMapper.Object);

            // Act 
            controller.Index();

            // Assert
            CollectionAssert.AreEqual(mappedBooks, cachedBooks);
        }

        [Test]
        public void CallCacheInsertWithCorrectKey_WhenCacheIsNull()
        {
            // Arrange
            var mockedBooksService = new Mock<IBooksService>();
            var mockedCacheProvider = new Mock<ICacheProvider>();
            var mockedMapper = new Mock<IMapperAdapter>();

            mockedCacheProvider.Setup(x => x.GetValue(It.IsAny<string>())).Returns(null);
            mockedCacheProvider.Setup(x => x.InsertWithAbsoluteExpiration(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<DateTime>())).Verifiable();
            var controller = new HomeController(mockedBooksService.Object, mockedCacheProvider.Object, mockedMapper.Object);

            // Act 
            controller.Index();

            // Assert
            mockedCacheProvider.Verify(x => x.InsertWithAbsoluteExpiration(Constants.TopBooksCache, It.IsAny<object>(), It.IsAny<DateTime>()), Times.Once);
        }

        [Test]
        public void ReturnDefaultView()
        {
            // Arrange
            var mockedBooksService = new Mock<IBooksService>();
            var mockedCacheProvider = new Mock<ICacheProvider>();
            var mockedMapper = new Mock<IMapperAdapter>();

            var controller = new HomeController(mockedBooksService.Object, mockedCacheProvider.Object, mockedMapper.Object);

            // Act & Assert
            controller.WithCallTo(c => c.Index()).ShouldRenderDefaultView();
        }

        [Test]
        public void ReturnViewWithCorrectModel_WhenCacheIsNull()
        {
            // Arrange
            var mockedBooksService = new Mock<IBooksService>();
            var mockedCacheProvider = new Mock<ICacheProvider>();
            var mockedMapper = new Mock<IMapperAdapter>();

            mockedCacheProvider.Setup(x => x.GetValue(It.IsAny<string>())).Returns(null);

            var mappedBooks = new List<BookViewModel>();
            mockedMapper.Setup(x => x.Map<IEnumerable<BookViewModel>>(It.IsAny<IEnumerable<Book>>())).Returns(mappedBooks);
            var controller = new HomeController(mockedBooksService.Object, mockedCacheProvider.Object, mockedMapper.Object);

            // Act & Assert
            controller.WithCallTo(c => c.Index()).ShouldRenderDefaultView()
                .WithModel(mappedBooks);
        }

        [Test]
        public void ReturnViewWithCorrectModel_WhenCacheIsNotNull()
        {
            // Arrange
            var mockedBooksService = new Mock<IBooksService>();
            var mockedCacheProvider = new Mock<ICacheProvider>();
            var mockedMapper = new Mock<IMapperAdapter>();

            var mappedBooks = new List<BookViewModel>();
            mockedCacheProvider.Setup(x => x.GetValue(It.IsAny<string>())).Returns(mappedBooks);

            var controller = new HomeController(mockedBooksService.Object, mockedCacheProvider.Object, mockedMapper.Object);

            // Act & Assert
            controller.WithCallTo(c => c.Index()).ShouldRenderDefaultView()
                .WithModel(mappedBooks);
        }
    }
}
