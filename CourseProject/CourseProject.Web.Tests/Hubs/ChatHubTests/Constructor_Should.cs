using Moq;
using NUnit.Framework;
using CourseProject.Web.Hubs;
using CourseProject.Services.Contracts;

namespace CourseProject.Web.Tests.Hubs.ChatHubTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullExceptionWithCorrectMessage_WhenUsersServiceIsNull()
        {
            // Act & Assert
            Assert.That(() => new ChatHub(null),
                Throws.ArgumentNullException.With.Message.Contains("usersService"));
        }

        [Test]
        public void NotThrow_WhenArgumentsAreNotNull()
        {
            // Arrange
            var mockedService = new Mock<IUsersService>();

            // Act & Assert
            Assert.DoesNotThrow(() => new ChatHub(mockedService.Object));
        }
    }
}
