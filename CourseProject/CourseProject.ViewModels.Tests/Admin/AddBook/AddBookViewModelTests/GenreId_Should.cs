using System.ComponentModel.DataAnnotations;
using System.Linq;
using NUnit.Framework;
using CourseProject.ViewModels.Admin.AddBook;

namespace CourseProject.ViewModels.Tests.Account.AddBookViewModelTests
{
    [TestFixture]
    public class GenreId_Should
    {
        [Test]
        public void HaveRequiredAttribute()
        {
            var viewModel = new AddBookViewModel();

            var hasAttr = viewModel.GetType()
                .GetProperty("GenreId")
                .GetCustomAttributes(typeof(RequiredAttribute), false)
                .Any();

            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void HaveDisplayAttribute()
        {
            var viewModel = new AddBookViewModel();

            var hasAttr = viewModel.GetType()
                .GetProperty("GenreId")
                .GetCustomAttributes(typeof(DisplayAttribute), false)
                .Any();

            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void HaveDisplayAttributeWithCorrectName()
        {
            var viewModel = new AddBookViewModel();

            var attr = viewModel.GetType()
                .GetProperty("GenreId")
                .GetCustomAttributes(typeof(DisplayAttribute), false)[0]
                as DisplayAttribute;

            Assert.AreEqual("Genre", attr.Name);
        }

        [Test]
        public void HaveGetAndSet()
        {
            var viewModel = new AddBookViewModel();

            viewModel.GenreId = 10;

            Assert.AreEqual(10, viewModel.GenreId);
        }
    }
}
