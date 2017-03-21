using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;
using CourseProject.Services.Contracts;
using CourseProject.Web.Mapping;
using CourseProject.Web.Controllers;
using CourseProject.Models;
using CourseProject.Web.Models;

namespace CourseProject.Web.Tests.Controllers.HomeControllerTests
{
    [TestFixture]
    public class Index_Should
    {
        [Test]
        public void CallBooksServiceHighestRatedBooks()
        {
            // Arrange
            var mockedBooksService = new Mock<IBooksService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            mockedBooksService.Setup(x => x.GetHighestRatedBooks(It.IsAny<int>())).Returns(new List<Book>());

            var controller = new HomeController(mockedBooksService.Object, mockedMapper.Object);

            // Act 
            controller.Index();

            // Assert
            mockedBooksService.Verify(x => x.GetHighestRatedBooks(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void CallBooksServiceHighestRatedBooksWithCorrectCount()
        {
            // Arrange
            var mockedBooksService = new Mock<IBooksService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            mockedBooksService.Setup(x => x.GetHighestRatedBooks(It.IsAny<int>())).Returns(new List<Book>());

            var controller = new HomeController(mockedBooksService.Object, mockedMapper.Object);

            // Act 
            controller.Index();

            // Assert
            mockedBooksService.Verify(x => x.GetHighestRatedBooks(8), Times.Once);
        }
        
        [Test]
        public void CallMapperWithCorrectCollection()
        {
            // Arrange
            var mockedBooksService = new Mock<IBooksService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            var books = new List<Book>();
            mockedBooksService.Setup(x => x.GetHighestRatedBooks(It.IsAny<int>())).Returns(books);
            mockedMapper.Setup(x => x.Map<IEnumerable<BookViewModel>>(It.IsAny<IEnumerable<Book>>())).Verifiable();
            var controller = new HomeController(mockedBooksService.Object, mockedMapper.Object);

            // Act 
            controller.Index();

            // Assert
            mockedMapper.Verify(x => x.Map<IEnumerable<BookViewModel>>(books), Times.Once);
        }

        [Test]
        public void ReturnDefaultView()
        {
            // Arrange
            var mockedBooksService = new Mock<IBooksService>();
            var mockedMapper = new Mock<IMapperAdapter>();

            var controller = new HomeController(mockedBooksService.Object, mockedMapper.Object);

            // Act & Assert
            controller.WithCallTo(c => c.Index()).ShouldRenderDefaultView();
        }

        [Test]
        public void ReturnViewWithCorrectModel()
        {
            // Arrange
            var mockedBooksService = new Mock<IBooksService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            var books = new List<Book>();
            var mappedBooks = new List<BookViewModel>();
            mockedBooksService.Setup(x => x.GetHighestRatedBooks(It.IsAny<int>())).Returns(books);
            mockedMapper.Setup(x => x.Map<IEnumerable<BookViewModel>>(books)).Returns(mappedBooks);
            var controller = new HomeController(mockedBooksService.Object, mockedMapper.Object);

            // Act & Assert
            controller.WithCallTo(c => c.Index()).ShouldRenderDefaultView()
                .WithModel(mappedBooks);

        }
    }
}
