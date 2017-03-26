using NUnit.Framework;

namespace CourseProject.Models.Tests.RatingTests
{
    [TestFixture]
    public class BookId_Should
    {
        [Test]
        public void HaveGetAndSet()
        {
            var rating = new Rating();

            rating.BookId = 10;

            Assert.AreEqual(10, rating.BookId);
        }
    }
}

