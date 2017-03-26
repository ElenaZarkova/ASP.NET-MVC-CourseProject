using NUnit.Framework;
using CourseProject.Models;

namespace CourseProject.Models.Tests.UserTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void InitializeRatings()
        {
            var user = new User();

            Assert.IsNotNull(user.Ratings);
        }

        [Test]
        public void InitializeUserBooks()
        {
            var user = new User();

            Assert.IsNotNull(user.UserBooks);
        }
    }
}
