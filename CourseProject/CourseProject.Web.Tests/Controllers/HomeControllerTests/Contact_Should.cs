using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;
using CourseProject.Services.Contracts;
using CourseProject.Web.Mapping;
using CourseProject.Web.Controllers;
using CourseProject.Web.Common.Providers.Contracts;

namespace CourseProject.Web.Tests.Controllers.HomeControllerTests
{
    [TestFixture]
    public class Contact_Should
    {
        [Test]
        public void ReturnDefaultView()
        {
            // Arrange
            var mockedBooksService = new Mock<IBooksService>();
            var mockedCacheProvider = new Mock<ICacheProvider>();
            var mockedMapper = new Mock<IMapperAdapter>();

            var controller =new HomeController(mockedBooksService.Object, mockedCacheProvider.Object, mockedMapper.Object);

            // Act & Assert
            controller.WithCallTo(c => c.Contact()).ShouldRenderDefaultView();
        }
    }
}
