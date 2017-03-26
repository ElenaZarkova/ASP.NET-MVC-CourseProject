using NUnit.Framework;

namespace CourseProject.Models.Tests.RatingTests
{
    [TestFixture]
    public class Book_Should
    {
        [Test]
        public void BeVirtualProperty()
        {
            var rating = new Rating();

            var isVirtual = rating.GetType()
                .GetProperty("Book")
                .GetAccessors()[0].IsVirtual;

            Assert.IsTrue(isVirtual);
        }

        [Test]
        public void HaveGetAndSet()
        {
            var rating = new Rating();
            var book = new Book();

            rating.Book = book;

            Assert.AreEqual(book, rating.Book);
        }
    }
}

