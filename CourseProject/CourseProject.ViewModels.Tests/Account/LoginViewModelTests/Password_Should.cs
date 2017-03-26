using System.ComponentModel.DataAnnotations;
using System.Linq;
using NUnit.Framework;
using CourseProject.ViewModels.Account;

namespace CourseProject.ViewModels.Tests.Account.LoginViewModelTests
{
    [TestFixture]
    public class Password_Should
    {
        [Test]
        public void HaveRequiredAttribute()
        {
            var viewModel = new LoginViewModel();

            var hasAttr = viewModel.GetType()
                .GetProperty("Password")
                .GetCustomAttributes(typeof(RequiredAttribute), false)
                .Any();

            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void HaveDataTyprAttribute()
        {
            var viewModel = new LoginViewModel();

            var hasAttr = viewModel.GetType()
                .GetProperty("Password")
                .GetCustomAttributes(typeof(DataTypeAttribute), false)
                .Any();

            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void HaveDataTyprAttributeWithCorrectType()
        {
            var viewModel = new LoginViewModel();

            var attr = viewModel.GetType()
                .GetProperty("Password")
                .GetCustomAttributes(typeof(DataTypeAttribute), false)[0]
                as DataTypeAttribute;

            Assert.AreEqual(DataType.Password, attr.DataType);
        }

        [Test]
        public void HaveDisplayAttribute()
        {
            var viewModel = new LoginViewModel();

            var hasAttr = viewModel.GetType()
                .GetProperty("Password")
                .GetCustomAttributes(typeof(DisplayAttribute), false)
                .Any();

            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void HaveDisplayAttributeWithCorrectName()
        {
            var viewModel = new LoginViewModel();

            var attr = viewModel.GetType()
                .GetProperty("Password")
                .GetCustomAttributes(typeof(DisplayAttribute), false)[0]
                as DisplayAttribute;

            Assert.AreEqual("Password", attr.Name);
        }

        [Test]
        public void HaveGetAndSet()
        {
            var viewModel = new LoginViewModel();

            viewModel.Password = "123456";

            Assert.AreEqual("123456", viewModel.Password);
        }
    }
}
