using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using CourseProject.Services.Contracts;
using CourseProject.Web.Mapping;
using CourseProject.Web.Controllers;
using CourseProject.Web.Identity.Contracts;
using TestStack.FluentMVCTesting;
using CourseProject.Web.Models;
using CourseProject.Web.Common.Providers.Contracts;

namespace CourseProject.Web.Tests.Controllers.BookControllerTests
{
    [TestFixture]
    public class GetRatingPartial_Should
    {
        [Test]
        public void HaveChildActionOnlyAttribute()
        {
            var method = typeof(BookController).GetMethod("GetRatingPartial");
            var hasAttr = method.GetCustomAttributes(typeof(ChildActionOnlyAttribute), false).Any();

            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void CallBooksServiceGetBookRatingWithCorrectId()
        {
            // Arrange
            var bookId = 89492;
            var mockedBooksService = new Mock<IBooksService>();
            var mockedRatingsService = new Mock<IRatingsService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            var mockedUserProvider = new Mock<IUserProvider>();
            mockedBooksService.Setup(x => x.GetBookRating(It.IsAny<int>())).Verifiable();

            var controller = new BookController(mockedBooksService.Object, mockedRatingsService.Object, mockedMapper.Object, mockedUserProvider.Object);

            // Act
            controller.GetRatingPartial(bookId);

            // Assert
            mockedBooksService.Verify(x => x.GetBookRating(bookId), Times.Once);
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
            controller.GetRatingPartial(5);

            // Assert
            mockedUserProvider.Verify(x => x.GetUserId(), Times.Once);
        }

        [Test]
        public void CallRatingsGetRating()
        {
            // Arrange
            var mockedBooksService = new Mock<IBooksService>();
            var mockedRatingsService = new Mock<IRatingsService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            var mockedUserProvider = new Mock<IUserProvider>();
            mockedRatingsService.Setup(x => x.GetRating(It.IsAny<int>(), It.IsAny<string>())).Verifiable();

            var controller = new BookController(mockedBooksService.Object, mockedRatingsService.Object, mockedMapper.Object, mockedUserProvider.Object);

            // Act
            controller.GetRatingPartial(5);

            // Assert
            mockedRatingsService.Verify(x => x.GetRating(It.IsAny<int>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void CallRatingsGetRatingWithCorrectBookIdAndUserId()
        {
            // Arrange
            var bookId = 3989;
            var userId = "pesho-pesho";
            var mockedBooksService = new Mock<IBooksService>();
            var mockedRatingsService = new Mock<IRatingsService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            var mockedUserProvider = new Mock<IUserProvider>();
            mockedUserProvider.Setup(x => x.GetUserId()).Returns(userId);
            mockedRatingsService.Setup(x => x.GetRating(bookId, userId)).Verifiable();

            var controller = new BookController(mockedBooksService.Object, mockedRatingsService.Object, mockedMapper.Object, mockedUserProvider.Object);

            // Act
            controller.GetRatingPartial(bookId);

            // Assert
            mockedRatingsService.Verify(x => x.GetRating(bookId, userId), Times.Once);
        }
        
        [Test]
        public void ReturnCorrectPartialView()
        {
            // Arrange
            var mockedBooksService = new Mock<IBooksService>();
            var mockedRatingsService = new Mock<IRatingsService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            var mockedUserProvider = new Mock<IUserProvider>();

            var controller = new BookController(mockedBooksService.Object, mockedRatingsService.Object, mockedMapper.Object, mockedUserProvider.Object);

            // Act & Assert
            controller.WithCallTo(c => c.GetRatingPartial(5))
                .ShouldRenderPartialView("_RatingPartial");
        }

        [Test]
        public void ReturnCorrectPartialViewWithCorrectModel()
        {
            // Arrange
            var bookId = 3989;
            var rating = 4.567;
            var userRating = 5;
            var mockedBooksService = new Mock<IBooksService>();
            var mockedRatingsService = new Mock<IRatingsService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            var mockedUserProvider = new Mock<IUserProvider>();
            mockedBooksService.Setup(x => x.GetBookRating(It.IsAny<int>())).Returns(rating);
            mockedRatingsService.Setup(x => x.GetRating(It.IsAny<int>(), It.IsAny<string>())).Returns(userRating);

            var controller = new BookController(mockedBooksService.Object, mockedRatingsService.Object, mockedMapper.Object, mockedUserProvider.Object);

            // Act & Assert
            controller.WithCallTo(c => c.GetRatingPartial(bookId))
                .ShouldRenderPartialView("_RatingPartial")
                .WithModel<RatingViewModel>(x =>
                {
                    Assert.AreEqual(x.Id, bookId);
                    Assert.AreEqual(x.RatingCalculated, rating);
                    Assert.AreEqual(x.UserRating, userRating);
                });
        }

    }
}
