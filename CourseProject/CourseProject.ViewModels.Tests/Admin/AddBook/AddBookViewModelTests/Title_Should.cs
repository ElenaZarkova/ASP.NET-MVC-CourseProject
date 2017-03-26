using System.ComponentModel.DataAnnotations;
using System.Linq;
using NUnit.Framework;
using CourseProject.ViewModels.Admin.AddBook;
using System.Web.Mvc;

namespace CourseProject.ViewModels.Tests.Account.AddBookViewModelTests
{
    [TestFixture]
    public class Title_Should
    {
        [Test]
        public void HaveRequiredAttribute()
        {
            var viewModel = new AddBookViewModel();

            var hasAttr = viewModel.GetType()
                .GetProperty("Title")
                .GetCustomAttributes(typeof(RequiredAttribute), false)
                .Any();

            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void HaveMinLengthAttribute()
        {
            var viewModel = new AddBookViewModel();

            var hasAttr = viewModel.GetType()
                .GetProperty("Title")
                .GetCustomAttributes(typeof(MinLengthAttribute), false)
                .Any();

            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void HaveMinLengthAttributeWithCorrectValue()
        {
            var viewModel = new AddBookViewModel();

            var attr = viewModel.GetType()
                .GetProperty("Title")
                .GetCustomAttributes(typeof(MinLengthAttribute), false)[0]
                as MinLengthAttribute;

            Assert.AreEqual(1, attr.Length);
        }

        [Test]
        public void HaveMaxLengthAttribute()
        {
            var viewModel = new AddBookViewModel();

            var hasAttr = viewModel.GetType()
                .GetProperty("Title")
                .GetCustomAttributes(typeof(MaxLengthAttribute), false)
                .Any();

            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void HaveMaxLengthAttributeWithCorrectValue()
        {
            var viewModel = new AddBookViewModel();

            var attr = viewModel.GetType()
                .GetProperty("Title")
                .GetCustomAttributes(typeof(MaxLengthAttribute), false)[0]
                as MaxLengthAttribute;

            Assert.AreEqual(50, attr.Length);
        }

        [Test]
        public void HaveAllowHtmlAttribute()
        {
            var viewModel = new AddBookViewModel();

            var hasAttr = viewModel.GetType()
                .GetProperty("Title")
                .GetCustomAttributes(typeof(AllowHtmlAttribute), false)
                .Any();

            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void HaveGetAndSet()
        {
            var viewModel = new AddBookViewModel();

            viewModel.Title = "The title";

            Assert.AreEqual("The title", viewModel.Title);
        }
    }
}
