using NUnit.Framework;

namespace CourseProject.Models.Tests.BookTests
{
    [TestFixture]
    public class GenreId_Should
    {
        [Test]
        public void HaveGetAndSet()
        {
            var book = new Book();

            book.GenreId = 10;

            Assert.AreEqual(10, book.GenreId);
        }
    }
}