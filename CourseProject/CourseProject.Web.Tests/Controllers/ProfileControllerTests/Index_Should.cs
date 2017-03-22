using NUnit.Framework;
using CourseProject.Web.Controllers;
using TestStack.FluentMVCTesting;

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
