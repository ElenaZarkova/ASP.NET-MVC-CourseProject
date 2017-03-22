using Moq;
using NUnit.Framework;
using CourseProject.Services.Contracts;
using CourseProject.Web.Mapping;
using CourseProject.Web.Controllers;

namespace CourseProject.Web.Tests.Controllers.SearchControllerTests
{
    [TestFixture]
    class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullExceptionWithCorrectMessage_WhenBooksServiceIsNull()
        {
            // Arrange
            var mockedGenresService = new Mock<IGenresService>();
            var mockedMapper = new Mock<IMapperAdapter>();

            // Act & Assert
            Assert.That(() => new SearchController(null, mockedGenresService.Object, mockedMapper.Object),
                Throws.ArgumentNullException.With.Message.Contains("booksService"));
        }

        [Test]
        public void ThrowArgumentNullExceptionWithCorrectMessage_WhenGenresServiceIsNull()
        {
            // Arrange
            var mockedBooksService = new Mock<IBooksService>();
            var mockedMapper = new Mock<IMapperAdapter>();

            // Act & Assert
            Assert.That(() => new SearchController(mockedBooksService.Object, null, mockedMapper.Object),
                Throws.ArgumentNullException.With.Message.Contains("genresService"));
        }

        [Test]
        public void ThrowArgumentNullExceptionWithCorrectMessage_WhenMapperIsNull()
        {
            // Arrange
            var mockedBooksService = new Mock<IBooksService>();
            var mockedGenresService = new Mock<IGenresService>();

            // Act & Assert
            Assert.That(() => new SearchController(mockedBooksService.Object, mockedGenresService.Object, null),
                Throws.ArgumentNullException.With.Message.Contains("mapper"));
        }

        [Test]
        public void NotThrow_WhenArgumentsAreNotNull()
        {
            // Arrange
            var mockedBooksService = new Mock<IBooksService>();
            var mockedGenresService = new Mock<IGenresService>();
            var mockedMapper = new Mock<IMapperAdapter>();

            // Act & Assert
            Assert.DoesNotThrow(() => new SearchController(mockedBooksService.Object, mockedGenresService.Object, mockedMapper.Object));
        }
    }
}

