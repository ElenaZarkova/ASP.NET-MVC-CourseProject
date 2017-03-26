using System.ComponentModel.DataAnnotations;
using NUnit.Framework;

namespace CourseProject.Models.Tests.BookTests
{
    [TestFixture]
    public class Author_Should
    {
        [Test]
        public void HaveRequiredAttribute()
        {
            var book = new Book();

            var attrs = book.GetType()
                .GetProperty("Author")
                .GetCustomAttributes(typeof(RequiredAttribute), false);

            Assert.AreEqual(1, attrs.Length);
        }

        [Test]
        public void HaveMinLengthAttribute()
        {
            var book = new Book();

            var attrs = book.GetType()
                .GetProperty("Author")
                .GetCustomAttributes(typeof(MinLengthAttribute), false);

            Assert.AreEqual(1, attrs.Length);
        }

        [Test]
        public void HaveMinLengthAttributeWithCorrectValue()
        {
            var book = new Book();

            var attr = book.GetType()
                .GetProperty("Author")
                .GetCustomAttributes(typeof(MinLengthAttribute), false)[0]
                as MinLengthAttribute;

            Assert.AreEqual(3, attr.Length);
        }

        [Test]
        public void HaveMaxLengthAttribute()
        {
            var book = new Book();

            var attrs = book.GetType()
                .GetProperty("Author")
                .GetCustomAttributes(typeof(MaxLengthAttribute), false);

            Assert.AreEqual(1, attrs.Length);
        }

        [Test]
        public void HaveMaxLengthAttributeWithCorrectValue()
        {
            var book = new Book();

            var attr = book.GetType()
                .GetProperty("Author")
                .GetCustomAttributes(typeof(MaxLengthAttribute), false)[0]
                as MaxLengthAttribute;

            Assert.AreEqual(80, attr.Length);
        }

        [Test]
        public void HaveGetAndSet()
        {
            var book = new Book();

            book.Author = "The author";

            Assert.AreEqual("The author", book.Author);
        }
    }
}