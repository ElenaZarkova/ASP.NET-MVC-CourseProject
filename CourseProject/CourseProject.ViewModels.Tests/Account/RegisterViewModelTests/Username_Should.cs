using System.ComponentModel.DataAnnotations;
using System.Linq;
using NUnit.Framework;
using CourseProject.ViewModels.Account;

namespace CourseProject.ViewModels.Tests.Account.RegisterViewModelTests
{
    [TestFixture]
    public class Username_Should
    {
        [Test]
        public void HaveRequiredAttribute()
        {
            var viewModel = new RegisterViewModel();

            var hasAttr = viewModel.GetType()
                .GetProperty("Username")
                .GetCustomAttributes(typeof(RequiredAttribute), false)
                .Any();

            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void HaveMinLengthAttribute()
        {
            var viewModel = new RegisterViewModel();

            var hasAttr = viewModel.GetType()
                .GetProperty("Username")
                .GetCustomAttributes(typeof(MinLengthAttribute), false)
                .Any();

            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void HaveMinLengthAttributeWithCorrectValue()
        {
            var viewModel = new RegisterViewModel();

            var attr = viewModel.GetType()
                .GetProperty("Username")
                .GetCustomAttributes(typeof(MinLengthAttribute), false)[0]
                as MinLengthAttribute;

            Assert.AreEqual(3, attr.Length);
        }

        [Test]
        public void HaveMaxLengthAttribute()
        {
            var viewModel = new RegisterViewModel();

            var hasAttr = viewModel.GetType()
                .GetProperty("Username")
                .GetCustomAttributes(typeof(MaxLengthAttribute), false)
                .Any();

            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void HaveMaxLengthAttributeWithCorrectValue()
        {
            var viewModel = new RegisterViewModel();

            var attr = viewModel.GetType()
                .GetProperty("Username")
                .GetCustomAttributes(typeof(MaxLengthAttribute), false)[0]
                as MaxLengthAttribute;

            Assert.AreEqual(20, attr.Length);
        }

        [Test]
        public void HaveDisplayAttribute()
        {
            var viewModel = new RegisterViewModel();

            var hasAttr = viewModel.GetType()
                .GetProperty("Username")
                .GetCustomAttributes(typeof(DisplayAttribute), false)
                .Any();

            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void HaveDisplayAttributeWithCorrectName()
        {
            var viewModel = new RegisterViewModel();

            var attr = viewModel.GetType()
                .GetProperty("Username")
                .GetCustomAttributes(typeof(DisplayAttribute), false)[0]
                as DisplayAttribute;

            Assert.AreEqual("Username", attr.Name);
        }

        [Test]
        public void HaveGetAndSet()
        {
            var viewModel = new RegisterViewModel();

            viewModel.Username = "Theusername";

            Assert.AreEqual("Theusername", viewModel.Username);
        }
    }
}
