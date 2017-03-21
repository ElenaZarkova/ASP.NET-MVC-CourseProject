using Moq;
using NUnit.Framework;
using CourseProject.Services.Contracts;
using CourseProject.Web.Mapping;
using CourseProject.Web.Controllers;


namespace CourseProject.Web.Tests.Controllers.HomeControllerTests
{
    [TestFixture]
    class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullExceptionWithCorrectMessage_WhenBooksServiceIsNull()
        {
            // Arrange
            var mockedMapper = new Mock<IMapperAdapter>();

            // Act & Assert
            Assert.That(() => new HomeController(null, mockedMapper.Object),
                Throws.ArgumentNullException.With.Message.Contains("booksService"));
        }

        [Test]
        public void ThrowArgumentNullExceptionWithCorrectMessage_WhenMapperIsNull()
        {
            // Arrange
            var mockedBooksService = new Mock<IBooksService>();

            // Act & Assert
            Assert.That(() => new HomeController(mockedBooksService.Object, null),
                Throws.ArgumentNullException.With.Message.Contains("mapper"));
        }

        [Test]
        public void NotThrow_WhenArgumentsAreNotNull()
        {
            // Arrange
            var mockedBooksService = new Mock<IBooksService>();
            var mockedMapper = new Mock<IMapperAdapter>();

            // Act & Assert
            Assert.DoesNotThrow(() => new HomeController(mockedBooksService.Object, mockedMapper.Object));
        }
    }
}
