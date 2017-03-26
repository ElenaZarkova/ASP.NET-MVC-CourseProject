using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using NUnit.Framework;
using CourseProject.Models;

namespace CourseProject.Models.Tests.UserBookTests
{
    [TestFixture]
    public class UserId_Should
    {
        [Test]
        public void HaveKeyAttribute()
        {
            var userBook = new UserBook();

            var hasAttr = userBook.GetType()
                .GetProperty("UserId")
                .GetCustomAttributes(typeof(KeyAttribute), false)
                .Any();

            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void HaveColumnAttribute()
        {
            var userBook = new UserBook();

            var hasAttr = userBook.GetType()
                .GetProperty("UserId")
                .GetCustomAttributes(typeof(ColumnAttribute), false)
                .Any();

            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void HaveColumnAttributeWithOrderZero()
        {
            var userBook = new UserBook();

            var attr = userBook.GetType()
                .GetProperty("UserId")
                .GetCustomAttributes(typeof(ColumnAttribute), false)[0]
                as ColumnAttribute;

            Assert.AreEqual(0, attr.Order);
        }

        [Test]
        public void HaveGetAndSet()
        {
            var userBook = new UserBook();

            userBook.UserId = "userid123";

            Assert.AreEqual("userid123", userBook.UserId);
        }
    }
}