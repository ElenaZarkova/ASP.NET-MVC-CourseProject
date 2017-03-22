using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using CourseProject.Models;
using CourseProject.Services.Contracts;
using CourseProject.Web.Mapping;
using CourseProject.Web.Controllers;
using TestStack.FluentMVCTesting;
using CourseProject.Web.Models;
using CourseProject.Web.Identity.Contracts;

namespace CourseProject.Web.Tests.Controllers.BookControllerTests
{
    [TestFixture]
    public class Index_Should
    {
        [Test]
        public void CallBooksServiceGetById()
        {
            // Arrange
            var mockedBooksService = new Mock<IBooksService>();
            var mockedRatingsService = new Mock<IRatingsService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            var mockedUserProvider = new Mock<IUserProvider>();
            mockedBooksService.Setup(x => x.GetById(It.IsAny<int>())).Verifiable();

            var controller = new BookController(mockedBooksService.Object, mockedRatingsService.Object, mockedMapper.Object, mockedUserProvider.Object);

            // Act
            controller.Index(5);

            // Assert
            mockedBooksService.Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
        }

        [TestCase(3)]
        [TestCase(49287)]
        public void CallBooksServiceGetByIdWithCorrectId(int id)
        {
            // Arrange
            var mockedBooksService = new Mock<IBooksService>();
            var mockedRatingsService = new Mock<IRatingsService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            var mockedUserProvider = new Mock<IUserProvider>();
            mockedBooksService.Setup(x => x.GetById(It.IsAny<int>())).Verifiable();

            var controller = new BookController(mockedBooksService.Object, mockedRatingsService.Object, mockedMapper.Object, mockedUserProvider.Object);

            // Act
            controller.Index(id);

            // Assert
            mockedBooksService.Verify(x => x.GetById(id), Times.Once);
        }

        [Test]
        public void ReturnErrorView_WhenBookNotFound()
        {
            // Arrange
            var mockedBooksService = new Mock<IBooksService>();
            var mockedRatingsService = new Mock<IRatingsService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            var mockedUserProvider = new Mock<IUserProvider>();
            mockedBooksService.Setup(x => x.GetById(It.IsAny<int>())).Returns((Book)null);

            var controller = new BookController(mockedBooksService.Object, mockedRatingsService.Object, mockedMapper.Object, mockedUserProvider.Object);

            // Act & Assert
            controller.WithCallTo(c => c.Index(5)).ShouldRenderView("Error");
        }

        [Test]
        public void MapBookToViewModel()
        {
            // Arrange
            var mockedBooksService = new Mock<IBooksService>();
            var mockedRatingsService = new Mock<IRatingsService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            var mockedUserProvider = new Mock<IUserProvider>();
            var mockedBook = new Mock<Book>();
            mockedBooksService.Setup(x => x.GetById(It.IsAny<int>())).Returns(mockedBook.Object);
            mockedMapper.Setup(x => x.Map<BookDetailsViewModel>(It.IsAny<Book>())).Verifiable();

            var controller = new BookController(mockedBooksService.Object, mockedRatingsService.Object, mockedMapper.Object, mockedUserProvider.Object);

            // Act
            controller.Index(5);

            // Assert
            mockedMapper.Verify(x => x.Map<BookDetailsViewModel>(mockedBook.Object), Times.Once);
        }

        [Test]
        public void ReturnCorrectView_WhenBookIsNotNull()
        {
            // Arrange
            var mockedBooksService = new Mock<IBooksService>();
            var mockedRatingsService = new Mock<IRatingsService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            var mockedUserProvider = new Mock<IUserProvider>();
            mockedBooksService.Setup(x => x.GetById(It.IsAny<int>())).Returns(new Mock<Book>().Object);

            var controller = new BookController(mockedBooksService.Object, mockedRatingsService.Object, mockedMapper.Object, mockedUserProvider.Object);

            // Act & Assert
            controller.WithCallTo(c => c.Index(5)).ShouldRenderDefaultView();
        }

        [Test]
        public void ReturnViewWithCorrectModel()
        {
            // Arrange
            var mockedBooksService = new Mock<IBooksService>();
            var mockedRatingsService = new Mock<IRatingsService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            var mockedUserProvider = new Mock<IUserProvider>();
            var mockedBook = new Mock<Book>();
            var mockedBookDetailsViewModel = new Mock<BookDetailsViewModel>();
            mockedBooksService.Setup(x => x.GetById(It.IsAny<int>())).Returns(mockedBook.Object);
            mockedMapper.Setup(x => x.Map<BookDetailsViewModel>(It.IsAny<Book>())).Returns(mockedBookDetailsViewModel.Object);

            var controller = new BookController(mockedBooksService.Object, mockedRatingsService.Object, mockedMapper.Object, mockedUserProvider.Object);

            // Act & Assert
            controller.WithCallTo(c => c.Index(7)).ShouldRenderDefaultView()
                .WithModel(mockedBookDetailsViewModel.Object);
        }
    }
}
