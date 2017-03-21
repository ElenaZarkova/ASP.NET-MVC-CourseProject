using Moq;
using NUnit.Framework;
using CourseProject.Services.Contracts;
using CourseProject.Web.Mapping;
using CourseProject.Web.Controllers;

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

            // Act & Assert
            Assert.That(() => new BookController(null, mockedRatingsService.Object, mockedMapper.Object), 
                Throws.ArgumentNullException.With.Message.Contains("booksService"));
        }

        [Test]
        public void ThrowArgumentNullExceptionWithCorrectMessage_WhenRatingsServiceIsNull()
        {
            // Arrange
            var mockedBooksService = new Mock<IBooksService>();
            var mockedMapper = new Mock<IMapperAdapter>();

            // Act & Assert
            Assert.That(() => new BookController(mockedBooksService.Object, null, mockedMapper.Object),
                Throws.ArgumentNullException.With.Message.Contains("ratingsService"));
        }

        [Test]
        public void ThrowArgumentNullExceptionWithCorrectMessage_WhenMapperIsNull()
        {
            // Arrange
            var mockedBooksService = new Mock<IBooksService>();
            var mockedRatingsService = new Mock<IRatingsService>();

            // Act & Assert
            Assert.That(() => new BookController(mockedBooksService.Object, mockedRatingsService.Object, null),
                Throws.ArgumentNullException.With.Message.Contains("mapper"));
        }

        [Test]
        public void NotThrow_WhenDataIsNotNull()
        {
            // Arrange
            var mockedBooksService = new Mock<IBooksService>();
            var mockedRatingsService = new Mock<IRatingsService>();
            var mockedMapper = new Mock<IMapperAdapter>();

            // Act & Assert
            Assert.DoesNotThrow(() => new BookController(mockedBooksService.Object, mockedRatingsService.Object, mockedMapper.Object));
        }
    }
}
