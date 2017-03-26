using NUnit.Framework;

namespace CourseProject.Models.Tests.BookTests
{
    [TestFixture]
    public class Id_Should
    {
        [Test]
        public void HaveGetAndSet()
        {
            var book = new Book();

            book.Id = 10;

            Assert.AreEqual(10, book.Id);
        }
    }
}

