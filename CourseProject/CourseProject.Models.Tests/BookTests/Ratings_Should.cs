using System.Collections.Generic;
using NUnit.Framework;
using Moq;

namespace CourseProject.Models.Tests.BookTests
{
    [TestFixture]
    public class Ratings_Should
    {
        [Test]
        public void BeVirtualProperty()
        {
            var book = new Book();
            var isVirtual = book.GetType()
                .GetProperty("Ratings")
                .GetAccessors()[0].IsVirtual;

            Assert.IsTrue(isVirtual);
        }

        [Test]
        public void HaveGetAndSet()
        {
            var book = new Book();
            var ratings = new List<Rating>() { new Mock<Rating>().Object, new Mock<Rating>().Object };

            book.Ratings = ratings;

            CollectionAssert.AreEqual(ratings, book.Ratings);
        }
    }
}
