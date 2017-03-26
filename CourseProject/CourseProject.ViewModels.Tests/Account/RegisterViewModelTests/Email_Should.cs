using System.ComponentModel.DataAnnotations;
using System.Linq;
using NUnit.Framework;
using CourseProject.ViewModels.Account;

namespace CourseProject.ViewModels.Tests.Account.RegisterViewModelTests
{
    [TestFixture]
    public class Email_Should
    {
        [Test]
        public void HaveRequiredAttribute()
        {
            var viewModel = new RegisterViewModel();

            var hasAttr = viewModel.GetType()
                .GetProperty("Email")
                .GetCustomAttributes(typeof(RequiredAttribute), false)
                .Any();

            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void HaveEmailAddressAttribute()
        {
            var viewModel = new RegisterViewModel();

            var hasAttr = viewModel.GetType()
                .GetProperty("Email")
                .GetCustomAttributes(typeof(EmailAddressAttribute), false)
                .Any();

            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void HaveDisplayAttribute()
        {
            var viewModel = new RegisterViewModel();

            var hasAttr = viewModel.GetType()
                .GetProperty("Email")
                .GetCustomAttributes(typeof(DisplayAttribute), false)
                .Any();

            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void HaveDisplayAttributeWithCorrectName()
        {
            var viewModel = new RegisterViewModel();

            var attr = viewModel.GetType()
                .GetProperty("Email")
                .GetCustomAttributes(typeof(DisplayAttribute), false)[0]
                as DisplayAttribute;

            Assert.AreEqual("Email", attr.Name);
        }

        [Test]
        public void HaveGetAndSet()
        {
            var viewModel = new RegisterViewModel();

            viewModel.Email = "email@email.com";

            Assert.AreEqual("email@email.com", viewModel.Email);
        }
    }
}
