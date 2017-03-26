using NUnit.Framework;

namespace CourseProject.Models.Tests.BookTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void InitializeRatings()
        {
            var book = new Book();

            Assert.IsNotNull(book.Ratings);
        }

        [Test]
        public void InitializeUserBooks()
        {
            var book = new Book();

            Assert.IsNotNull(book.UserBooks);
        }
    }
}
