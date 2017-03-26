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
    public class Login_Should
    {
        [Test]
        public void HaveAllowAnonymousAttribute()
        {
            var method = typeof(AccountController).GetMethod("Login", new Type[] { typeof(string) });
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
            controller.WithCallTo(c => c.Login(""))
                .ShouldRenderDefaultView();
        }

        [Test]
        public void SetCorrectUrlToViewBag()
        {
            // Arrange
            var mockedUserManager = new Mock<IApplicationUserManager>();
            var mockedSignInManager = new Mock<IApplicationSignInManager>();
            var controller = new AccountController(mockedUserManager.Object, mockedSignInManager.Object);

            string url = "home";

            // Act
            controller.Login(url);

            // Assert
            controller.ViewBag.ReturnUrl = url;
        }
    }
}

