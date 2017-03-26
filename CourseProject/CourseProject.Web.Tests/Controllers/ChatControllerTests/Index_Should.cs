using NUnit.Framework;
using TestStack.FluentMVCTesting;
using CourseProject.Web.Controllers;

namespace CourseProject.Web.Tests.Controllers.ChatControllerTests
{
    [TestFixture]
    public class Index_Should
    {
        [Test]
        public void ReturnDefaultView()
        {
            // Arrange
            var controller = new ChatController();

            // Act & Assert
            controller.WithCallTo(c => c.Index()).ShouldRenderDefaultView();
        }
    }
}
