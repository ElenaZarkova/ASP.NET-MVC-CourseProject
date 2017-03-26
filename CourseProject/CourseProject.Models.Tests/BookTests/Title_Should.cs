using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NUnit.Framework;

namespace CourseProject.Models.Tests.BookTests
{
    [TestFixture]
    public class Title_Should
    {
        [Test]
        public void HaveRequiredAttribute()
        {
            var book = new Book();

            var attrs = book.GetType()
                .GetProperty("Title")
                .GetCustomAttributes(typeof(RequiredAttribute), false);

            Assert.AreEqual(1, attrs.Length);
        }

        [Test]
        public void HaveMinLengthAttribute()
        {
            var book = new Book();

            var attrs = book.GetType()
                .GetProperty("Title")
                .GetCustomAttributes(typeof(MinLengthAttribute), false);

            Assert.AreEqual(1, attrs.Length);
        }

        [Test]
        public void HaveMinLengthAttributeWithCorrectValue()
        {
            var book = new Book();

            var attr = book.GetType()
                .GetProperty("Title")
                .GetCustomAttributes(typeof(MinLengthAttribute), false)[0]
                as MinLengthAttribute;

            Assert.AreEqual(1, attr.Length);
        }

        [Test]
        public void HaveMaxLengthAttribute()
        {
            var book = new Book();

            var attrs = book.GetType()
                .GetProperty("Title")
                .GetCustomAttributes(typeof(MaxLengthAttribute), false);

            Assert.AreEqual(1, attrs.Length);
        }

        [Test]
        public void HaveMaxLengthAttributeWithCorrectValue()
        {
            var book = new Book();

            var attr = book.GetType()
                .GetProperty("Title")
                .GetCustomAttributes(typeof(MaxLengthAttribute), false)[0]
                as MaxLengthAttribute;

            Assert.AreEqual(50, attr.Length);
        }

        [Test]
        public void HaveIndexAttribute()
        {
            var book = new Book();

            var attrs = book.GetType()
                .GetProperty("Title")
                .GetCustomAttributes(typeof(IndexAttribute), false);

            Assert.AreEqual(1, attrs.Length);
        }

        [Test]
        public void HaveIndexWithUniqueTrue()
        {
            var book = new Book();

            var attr = book.GetType()
                .GetProperty("Title")
                .GetCustomAttributes(typeof(IndexAttribute), false)[0]
                as IndexAttribute;

            Assert.AreEqual(true, attr.IsUnique);
        }

        [Test]
        public void HaveGetAndSet()
        {
            var book = new Book();

            book.Title = "The title";

            Assert.AreEqual("The title", book.Title);
        }
    }
}
