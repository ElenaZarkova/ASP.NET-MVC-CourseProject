using NUnit.Framework;

namespace CourseProject.Models.Tests.RatingTests
{
    [TestFixture]
    public class User_Should
    {
        [Test]
        public void BeVirtualProperty()
        {
            var rating = new Rating();

            var isVirtual = rating.GetType()
                .GetProperty("User")
                .GetAccessors()[0].IsVirtual;

            Assert.IsTrue(isVirtual);
        }

        [Test]
        public void HaveGetAndSet()
        {
            var rating = new Rating();
            var user = new User();

            rating.User = user;

            Assert.AreEqual(user, rating.User);
        }
    }
}
