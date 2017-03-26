using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using CourseProject.Models;

namespace CourseProject.Models.Tests.BookTests
{
    [TestFixture]
    public class UserBooks_Should
    {
        [Test]
        public void BeVirtualProperty()
        {
            var book = new Book();
            var isVirtual = book.GetType()
                .GetProperty("UserBooks")
                .GetAccessors()[0].IsVirtual;

            Assert.IsTrue(isVirtual);
        }

        [Test]
        public void HaveGetAndSet()
        {
            var book = new Book();
            var userBooks = new List<UserBook>() { new Mock<UserBook>().Object, new Mock<UserBook>().Object };

            book.UserBooks = userBooks;

            CollectionAssert.AreEqual(userBooks, book.UserBooks);
        }
    }
}