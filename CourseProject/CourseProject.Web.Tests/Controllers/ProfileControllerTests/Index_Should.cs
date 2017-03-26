using NUnit.Framework;
using TestStack.FluentMVCTesting;
using CourseProject.Web.Controllers;

namespace CourseProject.Web.Tests.Controllers.ProfileControllerTests
{
    [TestFixture]
    public class Index_Should
    {
        [Test]
        public void ReturnDefaultView()
        {
            // Arrange
            var controller = new ProfileController();

            // Act & Assert
            controller.WithCallTo(c => c.Index()).ShouldRenderDefaultView();
        }
    }
}
