using Moq;
using NUnit.Framework;
using CourseProject.Services.Contracts;
using CourseProject.Web.Mapping;
using CourseProject.Web.Controllers;
using CourseProject.Web.Common.Providers.Contracts;

namespace CourseProject.Web.Tests.Controllers.HomeControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullExceptionWithCorrectMessage_WhenBooksServiceIsNull()
        {
            // Arrange
            var mockedCacheProvider = new Mock<ICacheProvider>();
            var mockedMapper = new Mock<IMapperAdapter>();

            // Act & Assert
            Assert.That(() => new HomeController(null, mockedCacheProvider.Object, mockedMapper.Object),
                Throws.ArgumentNullException.With.Message.Contains("booksService"));
        }

        [Test]
        public void ThrowArgumentNullExceptionWithCorrectMessage_WhenCacheProviderIsNull()
        {
            // Arrange
            var mockedBooksService = new Mock<IBooksService>();
            var mockedMapper = new Mock<IMapperAdapter>();

            // Act & Assert
            Assert.That(() => new HomeController(mockedBooksService.Object, null, mockedMapper.Object),
                Throws.ArgumentNullException.With.Message.Contains("cacheProvider"));
        }

        [Test]
        public void ThrowArgumentNullExceptionWithCorrectMessage_WhenMapperIsNull()
        {
            // Arrange
            var mockedBooksService = new Mock<IBooksService>();
            var mockedCacheProvider = new Mock<ICacheProvider>();

            // Act & Assert
            Assert.That(() => new HomeController(mockedBooksService.Object, mockedCacheProvider.Object, null),
                Throws.ArgumentNullException.With.Message.Contains("mapper"));
        }

        [Test]
        public void NotThrow_WhenArgumentsAreNotNull()
        {
            // Arrange
            var mockedBooksService = new Mock<IBooksService>();
            var mockedCacheProvider = new Mock<ICacheProvider>();
            var mockedMapper = new Mock<IMapperAdapter>();

            // Act & Assert
            Assert.DoesNotThrow(() => new HomeController(mockedBooksService.Object, mockedCacheProvider.Object, mockedMapper.Object));
        }
    }
}
