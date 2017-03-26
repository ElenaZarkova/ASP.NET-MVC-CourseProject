using Moq;
using NUnit.Framework;
using CourseProject.Services.Contracts;
using CourseProject.Web.Common.Providers.Contracts;
using CourseProject.Web.Mapping;
using CourseProject.Web.Areas.Admin.Controllers;

namespace CourseProject.Web.Tests.Areas.Admin.Controllers.AddBookControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullExceptionWithCorrectMessage_WhenBooksServiceIsNull()
        {
            // Arrange
            var mockedGenresService = new Mock<IGenresService>();
            var mockedUserProvider = new Mock<IUserProvider>();
            var mockedServerProvider = new Mock<IServerProvider>();
            var mockedCacheProvider = new Mock<ICacheProvider>();
            var mockedMapper = new Mock<IMapperAdapter>();

            // Act & Assert
            Assert.That(() => new AddBookController(
                null,
                mockedGenresService.Object,
                mockedUserProvider.Object,
                mockedServerProvider.Object,
                mockedCacheProvider.Object,
                mockedMapper.Object),
                Throws.ArgumentNullException.With.Message.Contains("booksService"));
        }

        [Test]
        public void ThrowArgumentNullExceptionWithCorrectMessage_WhenGenresServiceIsNull()
        {
            // Arrange
            var mockedBooksService = new Mock<IBooksService>();
            var mockedUserProvider = new Mock<IUserProvider>();
            var mockedServerProvider = new Mock<IServerProvider>();
            var mockedCacheProvider = new Mock<ICacheProvider>();
            var mockedMapper = new Mock<IMapperAdapter>();

            // Act & Assert
            Assert.That(() => new AddBookController(
                mockedBooksService.Object,
                null,
                mockedUserProvider.Object,
                mockedServerProvider.Object,
                mockedCacheProvider.Object,
                mockedMapper.Object),
                Throws.ArgumentNullException.With.Message.Contains("genresService"));
        }

        [Test]
        public void ThrowArgumentNullExceptionWithCorrectMessage_WhenUserProviderIsNull()
        {
            // Arrange
            var mockedBooksService = new Mock<IBooksService>();
            var mockedGenresService = new Mock<IGenresService>();
            var mockedServerProvider = new Mock<IServerProvider>();
            var mockedCacheProvider = new Mock<ICacheProvider>();
            var mockedMapper = new Mock<IMapperAdapter>();

            // Act & Assert
            Assert.That(() => new AddBookController(
                mockedBooksService.Object,
                mockedGenresService.Object,
                null,
                mockedServerProvider.Object,
                mockedCacheProvider.Object,
                mockedMapper.Object),
                Throws.ArgumentNullException.With.Message.Contains("userProvider"));
        }

        [Test]
        public void ThrowArgumentNullExceptionWithCorrectMessage_WhenServerProviderIsNull()
        {
            // Arrange
            var mockedBooksService = new Mock<IBooksService>();
            var mockedGenresService = new Mock<IGenresService>();
            var mockedUserProvider = new Mock<IUserProvider>();
            var mockedCacheProvider = new Mock<ICacheProvider>();
            var mockedMapper = new Mock<IMapperAdapter>();

            // Act & Assert
            Assert.That(() => new AddBookController(
                mockedBooksService.Object,
                mockedGenresService.Object,
                mockedUserProvider.Object,
                null,
                mockedCacheProvider.Object,
                mockedMapper.Object),
                Throws.ArgumentNullException.With.Message.Contains("serverProvider"));
        }

        [Test]
        public void ThrowArgumentNullExceptionWithCorrectMessage_WhenCacheProviderIsNull()
        {
            // Arrange
            var mockedBooksService = new Mock<IBooksService>();
            var mockedGenresService = new Mock<IGenresService>();
            var mockedUserProvider = new Mock<IUserProvider>();
            var mockedServerProvider = new Mock<IServerProvider>();
            var mockedMapper = new Mock<IMapperAdapter>();

            // Act & Assert
            Assert.That(() => new AddBookController(
                mockedBooksService.Object,
                mockedGenresService.Object,
                mockedUserProvider.Object,
                mockedServerProvider.Object,
                null,
                mockedMapper.Object),
                Throws.ArgumentNullException.With.Message.Contains("cacheProvider"));
        }

        [Test]
        public void ThrowArgumentNullExceptionWithCorrectMessage_WhenMapperIsNull()
        {
            // Arrange
            var mockedBooksService = new Mock<IBooksService>();
            var mockedGenresService = new Mock<IGenresService>();
            var mockedUserProvider = new Mock<IUserProvider>();
            var mockedServerProvider = new Mock<IServerProvider>();
            var mockedCacheProvider = new Mock<ICacheProvider>();

            // Act & Assert
            Assert.That(() => new AddBookController(
                mockedBooksService.Object,
                mockedGenresService.Object,
                mockedUserProvider.Object,
                mockedServerProvider.Object,
                mockedCacheProvider.Object,
                null),
                Throws.ArgumentNullException.With.Message.Contains("mapper"));
        }

        [Test]
        public void NotThrow_WhenAllArgumentsAreNotNull()
        {
            // Arrange
            var mockedBooksService = new Mock<IBooksService>();
            var mockedGenresService = new Mock<IGenresService>();
            var mockedUserProvider = new Mock<IUserProvider>();
            var mockedServerProvider = new Mock<IServerProvider>();
            var mockedCacheProvider = new Mock<ICacheProvider>();
            var mockedMapper = new Mock<IMapperAdapter>();

            // Act & Assert
            Assert.DoesNotThrow(() => new AddBookController(
                mockedBooksService.Object,
                mockedGenresService.Object,
                mockedUserProvider.Object,
                mockedServerProvider.Object,
                mockedCacheProvider.Object,
                mockedMapper.Object));
        }
    }
}
