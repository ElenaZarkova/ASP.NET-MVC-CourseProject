using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using NUnit.Framework;
using CourseProject.ViewModels.Admin.AddBook;

namespace CourseProject.ViewModels.Tests.Account.AddBookViewModelTests
{
    [TestFixture]
    public class Description_Should
    {
        [Test]
        public void HaveRequiredAttribute()
        {
            var viewModel = new AddBookViewModel();

            var hasAttr = viewModel.GetType()
                .GetProperty("Description")
                .GetCustomAttributes(typeof(RequiredAttribute), false)
                .Any();

            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void HaveMinLengthAttribute()
        {
            var viewModel = new AddBookViewModel();

            var hasAttr = viewModel.GetType()
                .GetProperty("Description")
                .GetCustomAttributes(typeof(MinLengthAttribute), false)
                .Any();

            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void HaveMinLengthAttributeWithCorrectValue()
        {
            var viewModel = new AddBookViewModel();

            var attr = viewModel.GetType()
                .GetProperty("Description")
                .GetCustomAttributes(typeof(MinLengthAttribute), false)[0]
                as MinLengthAttribute;

            Assert.AreEqual(3, attr.Length);
        }

        [Test]
        public void HaveMaxLengthAttribute()
        {
            var viewModel = new AddBookViewModel();

            var hasAttr = viewModel.GetType()
                .GetProperty("Description")
                .GetCustomAttributes(typeof(MaxLengthAttribute), false)
                .Any();

            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void HaveMaxLengthAttributeWithCorrectValue()
        {
            var viewModel = new AddBookViewModel();

            var attr = viewModel.GetType()
                .GetProperty("Description")
                .GetCustomAttributes(typeof(MaxLengthAttribute), false)[0]
                as MaxLengthAttribute;

            Assert.AreEqual(300, attr.Length);
        }

        [Test]
        public void HaveAllowHtmlAttribute()
        {
            var viewModel = new AddBookViewModel();

            var hasAttr = viewModel.GetType()
                .GetProperty("Description")
                .GetCustomAttributes(typeof(AllowHtmlAttribute), false)
                .Any();

            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void HaveGetAndSet()
        {
            var viewModel = new AddBookViewModel();

            viewModel.Description = "The description";

            Assert.AreEqual("The description", viewModel.Description);
        }
    }
}
