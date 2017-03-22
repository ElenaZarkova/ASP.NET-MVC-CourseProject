using Moq;
using NUnit.Framework;
using CourseProject.Services.Contracts;
using CourseProject.Web.Mapping;
using CourseProject.Web.Controllers;
using CourseProject.Web.Identity.Contracts;

namespace CourseProject.Web.Tests.Controllers.BookControllerTests
{
    [TestFixture]
    class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullExceptionWithCorrectMessage_WhenBooksServiceIsNull()
        {
            // Arrange
            var mockedRatingsService = new Mock<IRatingsService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            var mockedUserProvider = new Mock<IUserProvider>();

            // Act & Assert
            Assert.That(() => new BookController(null, mockedRatingsService.Object, mockedMapper.Object, mockedUserProvider.Object), 
                Throws.ArgumentNullException.With.Message.Contains("booksService"));
        }

        [Test]
        public void ThrowArgumentNullExceptionWithCorrectMessage_WhenRatingsServiceIsNull()
        {
            // Arrange
            var mockedBooksService = new Mock<IBooksService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            var mockedUserProvider = new Mock<IUserProvider>();

            // Act & Assert
            Assert.That(() => new BookController(mockedBooksService.Object, null, mockedMapper.Object, mockedUserProvider.Object),
                Throws.ArgumentNullException.With.Message.Contains("ratingsService"));
        }

        [Test]
        public void ThrowArgumentNullExceptionWithCorrectMessage_WhenMapperIsNull()
        {
            // Arrange
            var mockedBooksService = new Mock<IBooksService>();
            var mockedRatingsService = new Mock<IRatingsService>();
            var mockedUserProvider = new Mock<IUserProvider>();

            // Act & Assert
            Assert.That(() => new BookController(mockedBooksService.Object, mockedRatingsService.Object, null, mockedUserProvider.Object),
                Throws.ArgumentNullException.With.Message.Contains("mapper"));
        }

        [Test]
        public void ThrowArgumentNullExceptionWithCorrectMessage_WhenUserProviderIsNull()
        {
            // Arrange
            var mockedBooksService = new Mock<IBooksService>();
            var mockedRatingsService = new Mock<IRatingsService>();
            var mockedMapper = new Mock<IMapperAdapter>();

            // Act & Assert
            Assert.That(() => new BookController(mockedBooksService.Object, mockedRatingsService.Object, mockedMapper.Object, null),
                Throws.ArgumentNullException.With.Message.Contains("userProvider"));
        }

        [Test]
        public void NotThrow_WhenArgumentsAreNotNull()
        {
            // Arrange
            var mockedBooksService = new Mock<IBooksService>();
            var mockedRatingsService = new Mock<IRatingsService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            var mockedUserProvider = new Mock<IUserProvider>();

            // Act & Assert
            Assert.DoesNotThrow(() => new BookController(mockedBooksService.Object, mockedRatingsService.Object, mockedMapper.Object, mockedUserProvider.Object));
        }
    }
}
