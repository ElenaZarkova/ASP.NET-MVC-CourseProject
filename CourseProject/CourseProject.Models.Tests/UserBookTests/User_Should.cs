using NUnit.Framework;

namespace CourseProject.Models.Tests.UserBookTests
{
    [TestFixture]
    public class User_Should
    {
        [Test]
        public void BeVirtualProperty()
        {
            var userBook = new UserBook();

            var isVirtual = userBook.GetType()
                .GetProperty("User")
                .GetAccessors()[0].IsVirtual;

            Assert.IsTrue(isVirtual);
        }

        [Test]
        public void HaveGetAndSet()
        {
            var userBook = new UserBook();
            var user = new User();

            userBook.User = user;

            Assert.AreEqual(user, userBook.User);

        }
    }
}
