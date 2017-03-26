using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using CourseProject.Models;

namespace CourseProject.Models.Tests.UserTests
{
    [TestFixture]
    public class UserBooks_Should
    {
        [Test]
        public void BeVirtualProperty()
        {
            var user = new User();
            var isVirtual = user.GetType()
                .GetProperty("UserBooks")
                .GetAccessors()[0].IsVirtual;

            Assert.IsTrue(isVirtual);
        }

        [Test]
        public void HaveGetAndSet()
        {
            var user = new User();
            var userBooks = new List<UserBook>() { new Mock<UserBook>().Object, new Mock<UserBook>().Object };

            user.UserBooks = userBooks;

            CollectionAssert.AreEqual(userBooks, user.UserBooks);
        }
    }
}
