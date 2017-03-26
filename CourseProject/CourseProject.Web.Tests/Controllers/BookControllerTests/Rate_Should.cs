using System.Linq;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;
using CourseProject.Services.Contracts;
using CourseProject.Web.Mapping;
using CourseProject.Web.Controllers;
using CourseProject.Web.Common.Providers.Contracts;

namespace CourseProject.Web.Tests.Controllers.BookControllerTests
{
    [TestFixture]
    public class Rate_Should
    {
        [Test]
        public void HaveAuthorizeAttribute()
        {
            var method = typeof(BookController).GetMethod("Rate");
            var hasAttr = method.GetCustomAttributes(typeof(AuthorizeAttribute), false).Any();

            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void CallUserProviderGetById()
        {
            // Arrange
            var mockedBooksService = new Mock<IBooksService>();
            var mockedRatingsService = new Mock<IRatingsService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            var mockedUserProvider = new Mock<IUserProvider>();
            mockedUserProvider.Setup(x => x.GetUserId()).Verifiable();

            var controller = new BookController(mockedBooksService.Object, mockedRatingsService.Object, mockedMapper.Object, mockedUserProvider.Object);

            // Act
            controller.Rate(5, 5);

            // Assert
            mockedUserProvider.Verify(x => x.GetUserId(), Times.Once);
        }
        
        [Test]
        public void CallRatingsServiceRateBook()
        {
            // Arrange
            var mockedBooksService = new Mock<IBooksService>();
            var mockedRatingsService = new Mock<IRatingsService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            var mockedUserProvider = new Mock<IUserProvider>();
            mockedRatingsService.Setup(x => x.RateBook(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>())).Verifiable();

            var controller = new BookController(mockedBooksService.Object, mockedRatingsService.Object, mockedMapper.Object, mockedUserProvider.Object);

            // Act
            controller.Rate(5, 5);

            // Assert
            mockedRatingsService.Verify(x => x.RateBook(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void CallRatingsServiceRateBookWithCorrectParams()
        {
            // Arrange
            var bookId = 345;
            var userId = "some-str";
            var rate = 3;
            var mockedBooksService = new Mock<IBooksService>();
            var mockedRatingsService = new Mock<IRatingsService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            var mockedUserProvider = new Mock<IUserProvider>();
            mockedUserProvider.Setup(x => x.GetUserId()).Returns(userId);
            mockedRatingsService.Setup(x => x.RateBook(bookId, userId, rate)).Verifiable();

            var controller = new BookController(mockedBooksService.Object, mockedRatingsService.Object, mockedMapper.Object, mockedUserProvider.Object);

            // Act
            controller.Rate(bookId, rate);

            // Assert
            mockedRatingsService.Verify(x => x.RateBook(bookId, userId, rate), Times.Once);
        }

        [Test]
        public void CallBooksServiceGetNewRatingWithCorrectId()
        {
            // Arrange
            int bookId = 7891;
            var mockedBooksService = new Mock<IBooksService>();
            var mockedRatingsService = new Mock<IRatingsService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            var mockedUserProvider = new Mock<IUserProvider>();
            mockedBooksService.Setup(x => x.GetBookRating(It.IsAny<int>())).Verifiable();

            var controller = new BookController(mockedBooksService.Object, mockedRatingsService.Object, mockedMapper.Object, mockedUserProvider.Object);

            // Act
            controller.Rate(bookId, 5);

            // Assert
            mockedBooksService.Verify(x => x.GetBookRating(bookId), Times.Once);
        }

        [Test]
        public void ReturnJsonWithSuccessAndCorrectRating()
        {
            // Arrange
            var rating = 3.784;
            var mockedBooksService = new Mock<IBooksService>();
            var mockedRatingsService = new Mock<IRatingsService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            var mockedUserProvider = new Mock<IUserProvider>();
            mockedBooksService.Setup(x => x.GetBookRating(It.IsAny<int>())).Returns(rating);

            var controller = new BookController(mockedBooksService.Object, mockedRatingsService.Object, mockedMapper.Object, mockedUserProvider.Object);

            // Act & Assert
            controller.WithCallTo(c => c.Rate(4, 5)).ShouldReturnJson(data =>
            {
                // uppercase does not work
                Assert.AreEqual(data.success, true);
                Assert.AreEqual(data.rating, rating);
            });
        }
    }
}
