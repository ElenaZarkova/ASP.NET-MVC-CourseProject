using System.ComponentModel.DataAnnotations;
using System.Linq;
using NUnit.Framework;
using CourseProject.ViewModels.Account;

namespace CourseProject.ViewModels.Tests.Account.RegisterViewModelTests
{
    [TestFixture]
    public class Password_Should
    {
        [Test]
        public void HaveRequiredAttribute()
        {
            var viewModel = new RegisterViewModel();

            var hasAttr = viewModel.GetType()
                .GetProperty("Password")
                .GetCustomAttributes(typeof(RequiredAttribute), false)
                .Any();

            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void HaveStringLengthAttribute()
        {
            var viewModel = new RegisterViewModel();

            var hasAttr = viewModel.GetType()
                .GetProperty("Password")
                .GetCustomAttributes(typeof(StringLengthAttribute), false)
                .Any();

            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void HaveStringLengthAttributeWithCorrectLength()
        {
            var viewModel = new RegisterViewModel();

            var attr = viewModel.GetType()
                .GetProperty("Password")
                .GetCustomAttributes(typeof(StringLengthAttribute), false)[0]
                as StringLengthAttribute;

            Assert.AreEqual(6, attr.MinimumLength);
            Assert.AreEqual(100, attr.MaximumLength);
        }

        [Test]
        public void HaveStringLengthAttributeWithCorrectErrorMessage()
        {
            var viewModel = new RegisterViewModel();

            var attr = viewModel.GetType()
                .GetProperty("Password")
                .GetCustomAttributes(typeof(StringLengthAttribute), false)[0]
                as StringLengthAttribute;

            Assert.AreEqual("The {0} must be at least {2} characters long.", attr.ErrorMessage);
        }

        [Test]
        public void HaveDataTypeAttribute()
        {
            var viewModel = new RegisterViewModel();

            var hasAttr = viewModel.GetType()
                .GetProperty("Password")
                .GetCustomAttributes(typeof(DataTypeAttribute), false)
                .Any();

            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void HaveDataTypeAttributeWithCorrectType()
        {
            var viewModel = new RegisterViewModel();

            var attr = viewModel.GetType()
                .GetProperty("Password")
                .GetCustomAttributes(typeof(DataTypeAttribute), false)[0]
                as DataTypeAttribute;

            Assert.AreEqual(DataType.Password, attr.DataType);
        }

        [Test]
        public void HaveDisplayAttribute()
        {
            var viewModel = new RegisterViewModel();

            var hasAttr = viewModel.GetType()
                .GetProperty("Password")
                .GetCustomAttributes(typeof(DisplayAttribute), false)
                .Any();

            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void HaveDisplayAttributeWithCorrectName()
        {
            var viewModel = new RegisterViewModel();

            var attr = viewModel.GetType()
                .GetProperty("Password")
                .GetCustomAttributes(typeof(DisplayAttribute), false)[0]
                as DisplayAttribute;

            Assert.AreEqual("Password", attr.Name);
        }

        [Test]
        public void HaveGetAndSet()
        {
            var viewModel = new RegisterViewModel();

            viewModel.Password = "123456";

            Assert.AreEqual("123456", viewModel.Password);
        }
    }
}
