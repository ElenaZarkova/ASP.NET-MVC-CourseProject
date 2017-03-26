using System.Web;
using NUnit.Framework;
using Moq;
using CourseProject.Web.Common.Providers;

namespace CourseProject.Web.Tests.Common.Providers.ServerProviderTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullExceptionWithCorrectMessage_WhenHttpContextBaseIsNull()
        {
            // Act & Assert
            Assert.That(() => new ServerProvider(null),
                Throws.ArgumentNullException.With.Message.Contains("httpContext"));
        }

        [Test]
        public void NotThrow_WhenArgumentsAreNotNull()
        {
            // Arrange
            var mockedContext = new Mock<HttpContextBase>();

            // Act & Assert
            Assert.DoesNotThrow(() => new ServerProvider(mockedContext.Object));
        }
    }
}
