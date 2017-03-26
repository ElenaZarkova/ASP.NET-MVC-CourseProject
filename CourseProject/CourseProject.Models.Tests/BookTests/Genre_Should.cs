using NUnit.Framework;

namespace CourseProject.Models.Tests.bookTests
{
    [TestFixture]
    public class Genre_Should
    {
        [Test]
        public void BeVirtualProperty()
        {
            var book = new Book();

            var isVirtual = book.GetType()
                .GetProperty("Genre")
                .GetAccessors()[0].IsVirtual;

            Assert.IsTrue(isVirtual);
        }

        [Test]
        public void HaveGetAndSet()
        {
            var book = new Book();
            var genre = new Genre();

            book.Genre = genre;

            Assert.AreEqual(genre, book.Genre);
        }
    }
}