using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Moq;
using NUnit.Framework;
using CourseProject.ViewModels.Admin.AddBook;

namespace CourseProject.ViewModels.Tests.Account.AddBookViewModelTests
{
    [TestFixture]
    public class CoverFile_Should
    {
        [Test]
        public void HaveRequiredAttribute()
        {
            var viewModel = new AddBookViewModel();

            var hasAttr = viewModel.GetType()
                .GetProperty("CoverFile")
                .GetCustomAttributes(typeof(RequiredAttribute), false)
                .Any();

            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void HaveDisplayAttribute()
        {
            var viewModel = new AddBookViewModel();

            var hasAttr = viewModel.GetType()
                .GetProperty("CoverFile")
                .GetCustomAttributes(typeof(DisplayAttribute), false)
                .Any();

            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void HaveDisplayAttributeWithCorrectName()
        {
            var viewModel = new AddBookViewModel();

            var attr = viewModel.GetType()
                .GetProperty("CoverFile")
                .GetCustomAttributes(typeof(DisplayAttribute), false)[0]
                as DisplayAttribute;

            Assert.AreEqual("Cover photo", attr.Name);
        }

        [Test]
        public void HaveGetAndSet()
        {
            var viewModel = new AddBookViewModel();
            var file = new Mock<HttpPostedFileBase>();

            viewModel.CoverFile = file.Object;

            Assert.AreEqual(file.Object, viewModel.CoverFile);
        }
    }
}
