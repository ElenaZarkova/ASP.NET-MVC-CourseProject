using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using NUnit.Framework;
using CourseProject.Models;

namespace CourseProject.Models.Tests.UserBookTests
{
    [TestFixture]
    public class BookId_Should
    {
        [Test]
        public void HaveKeyAttribute()
        {
            var userBook = new UserBook();

            var hasAttr = userBook.GetType()
                .GetProperty("BookId")
                .GetCustomAttributes(typeof(KeyAttribute), false)
                .Any();

            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void HaveColumnAttribute()
        {
            var userBook = new UserBook();

            var hasAttr = userBook.GetType()
                .GetProperty("BookId")
                .GetCustomAttributes(typeof(ColumnAttribute), false)
                .Any();

            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void HaveColumnAttributeWithOrderOne()
        {
            var userBook = new UserBook();

            var attr = userBook.GetType()
                .GetProperty("BookId")
                .GetCustomAttributes(typeof(ColumnAttribute), false)[0]
                as ColumnAttribute;

            Assert.AreEqual(1, attr.Order);
        }

        [Test]
        public void HaveGetAndSet()
        {
            var userBook = new UserBook();

            userBook.BookId = 5;

            Assert.AreEqual(5, userBook.BookId);
        }
    }
}
