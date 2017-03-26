using NUnit.Framework;

namespace CourseProject.Models.Tests.RatingTests
{
    [TestFixture]
    public class Id_Should
    {
        [Test]
        public void HaveGetAndSet()
        {
            var rating = new Rating();

            rating.Id = 10;

            Assert.AreEqual(10, rating.Id);
        }
    }
}
