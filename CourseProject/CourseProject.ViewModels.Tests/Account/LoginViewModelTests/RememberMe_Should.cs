using System.ComponentModel.DataAnnotations;
using System.Linq;
using NUnit.Framework;
using CourseProject.ViewModels.Account;

namespace CourseProject.ViewModels.Tests.Account.LoginViewModelTests
{
    [TestFixture]
    public class RememberMe_Should
    {
        [Test]
        public void HaveDisplayAttribute()
        {
            var viewModel = new LoginViewModel();

            var hasAttr = viewModel.GetType()
                .GetProperty("RememberMe")
                .GetCustomAttributes(typeof(DisplayAttribute), false)
                .Any();

            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void HaveDisplayAttributeWithCorrectName()
        {
            var viewModel = new LoginViewModel();

            var attr = viewModel.GetType()
                .GetProperty("RememberMe")
                .GetCustomAttributes(typeof(DisplayAttribute), false)[0]
                as DisplayAttribute;

            Assert.AreEqual("Remember me?", attr.Name);
        }

        [Test]
        public void HaveGetAndSet()
        {
            var viewModel = new LoginViewModel();

            viewModel.RememberMe = true;

            Assert.AreEqual(true, viewModel.RememberMe);
        }
    }
}
