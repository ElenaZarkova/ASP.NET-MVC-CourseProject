using NUnit.Framework;

namespace CourseProject.Models.Tests.RatingTests
{
    [TestFixture]
    public class UserId_Should
    {
        [Test]
        public void HaveGetAndSet()
        {
            var rating = new Rating();

            rating.UserId = "peshoto";

            Assert.AreEqual("peshoto", rating.UserId);
        }
    }
}