using System;
using System.Linq;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;
using CourseProject.Web.Controllers;
using CourseProject.Web.Identity.Contracts;

namespace CourseProject.Web.Tests.Controllers.AccountControllerTests
{
    [TestFixture]
    public class Register_Should
    {
        [Test]
        public void HaveAllowAnonymousAttribute()
        {
            var method = typeof(AccountController).GetMethod("Register", new Type[0]);
            var hasAttr = method.GetCustomAttributes(typeof(AllowAnonymousAttribute), false).Any();

            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void ReturnDefaultView()
        {
            // Arrange
            var mockedUserManager = new Mock<IApplicationUserManager>();
            var mockedSignInManager = new Mock<IApplicationSignInManager>();
            var controller = new AccountController(mockedUserManager.Object, mockedSignInManager.Object);

            // Act & Assert
            controller.WithCallTo(c => c.Register())
                .ShouldRenderDefaultView();
        }
    }
}