using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using CourseProject.Web.Controllers;
using CourseProject.Web.Identity.Contracts;

namespace CourseProject.Web.Tests.Controllers.AccountControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullExceptionWithCorrectMessage_WhenUserManagerIsNull()
        {
            // Arrange
            var mockedSignInManager = new Mock<IApplicationSignInManager>();

            // Act & Assert
            Assert.That(() => new AccountController(null, mockedSignInManager.Object),
                Throws.ArgumentNullException.With.Message.Contains("userManager"));
        }

        [Test]
        public void ThrowArgumentNullExceptionWithCorrectMessage_WhenSignInManagerIsNull()
        {
            // Arrange
            var mockedUserManager = new Mock<IApplicationUserManager>();

            // Act & Assert
            Assert.That(() => new AccountController(mockedUserManager.Object, null),
                Throws.ArgumentNullException.With.Message.Contains("signInManager"));
        }

        [Test]
        public void NotThrow_WhenArgumentsAreNotNull()
        {
            // Arrange
            var mockedUserManager = new Mock<IApplicationUserManager>();
            var mockedSignInManager = new Mock<IApplicationSignInManager>();

            // Act & Assert
            Assert.DoesNotThrow(() => new AccountController(mockedUserManager.Object, mockedSignInManager.Object));
        }
    }
}
