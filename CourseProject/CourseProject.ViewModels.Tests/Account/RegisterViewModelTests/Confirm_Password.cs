using System.ComponentModel.DataAnnotations;
using System.Linq;
using NUnit.Framework;
using CourseProject.ViewModels.Account;

namespace CourseProject.ViewModels.Tests.Account.RegisterViewModelTests
{
    [TestFixture]
    public class ConfirmPassword_Should
    {
        [Test]
        public void HaveDataTypeAttribute()
        {
            var viewModel = new RegisterViewModel();

            var hasAttr = viewModel.GetType()
                .GetProperty("ConfirmPassword")
                .GetCustomAttributes(typeof(DataTypeAttribute), false)
                .Any();

            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void HaveDataTypeAttributeWithCorrectType()
        {
            var viewModel = new RegisterViewModel();

            var attr = viewModel.GetType()
                .GetProperty("ConfirmPassword")
                .GetCustomAttributes(typeof(DataTypeAttribute), false)[0]
                as DataTypeAttribute;

            Assert.AreEqual(DataType.Password, attr.DataType);
        }

        [Test]
        public void HaveDisplayAttribute()
        {
            var viewModel = new RegisterViewModel();

            var hasAttr = viewModel.GetType()
                .GetProperty("ConfirmPassword")
                .GetCustomAttributes(typeof(DisplayAttribute), false)
                .Any();

            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void HaveDisplayAttributeWithCorrectName()
        {
            var viewModel = new RegisterViewModel();

            var attr = viewModel.GetType()
                .GetProperty("ConfirmPassword")
                .GetCustomAttributes(typeof(DisplayAttribute), false)[0]
                as DisplayAttribute;

            Assert.AreEqual("Confirm password", attr.Name);
        }

        [Test]
        public void HaveCompareAttribute()
        {
            var viewModel = new RegisterViewModel();

            var hasAttr = viewModel.GetType()
                .GetProperty("ConfirmPassword")
                .GetCustomAttributes(typeof(CompareAttribute), false)
                .Any();

            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void HaveCompareAttributeWithCorrectProperty()
        {
            var viewModel = new RegisterViewModel();

            var attr = viewModel.GetType()
                .GetProperty("ConfirmPassword")
                .GetCustomAttributes(typeof(CompareAttribute), false)[0]
                as CompareAttribute;

            Assert.AreEqual("Password", attr.OtherProperty);
        }

        [Test]
        public void HaveCompareAttributeWithCorrectErrorMessage()
        {
            var viewModel = new RegisterViewModel();

            var attr = viewModel.GetType()
                .GetProperty("ConfirmPassword")
                .GetCustomAttributes(typeof(CompareAttribute), false)[0]
                as CompareAttribute;

            Assert.AreEqual("The password and confirmation password do not match.", attr.ErrorMessage);
        }

        [Test]
        public void HaveGetAndSet()
        {
            var viewModel = new RegisterViewModel();

            viewModel.ConfirmPassword = "123456";

            Assert.AreEqual("123456", viewModel.ConfirmPassword);
        }
    }
}
