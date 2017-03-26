using NUnit.Framework;
using CourseProject.Models;

namespace CourseProject.Models.Tests.GenreTests
{
    [TestFixture]
    public class Id_Should
    {
        [Test]
        public void HaveGetAndSet()
        {
            var genre = new Genre();

            genre.Id = 12314;

            Assert.AreEqual(12314, genre.Id);
        }
    }
}
