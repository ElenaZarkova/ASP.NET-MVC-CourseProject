using System.Web;
using NUnit.Framework;
using Moq;
using CourseProject.Web.Common.Providers;

namespace CourseProject.Web.Tests.Common.Providers.ServerProviderTests
{
    [TestFixture]
    public class MapPath_Should
    {
        [Test]
        public void CallServerMapPathWithCorrectPath()
        {
            // Arrange
            var mockedContext = new Mock<HttpContextBase>();
            mockedContext.Setup(x => x.Server.MapPath(It.IsAny<string>())).Verifiable();
            var provider = new ServerProvider(mockedContext.Object);

            var path = "~/path/img/jpg";

            // Act
            var result = provider.MapPath(path);

            // Assert
            mockedContext.Verify(x => x.Server.MapPath(path), Times.Once);
        }

        [Test]
        public void ReturnServerMapPathResult()
        {
            // Arrange
            var mockedContext = new Mock<HttpContextBase>();
            var returnedPath = "returned/path";
            mockedContext.Setup(x => x.Server.MapPath(It.IsAny<string>())).Returns(returnedPath);

            var provider = new ServerProvider(mockedContext.Object);

            // Act
            var result = provider.MapPath("path");

            // Assert
            Assert.AreEqual(returnedPath, result);
        }
    }
}
