using NUnit.Framework;
using CourseProject.Models;

namespace CourseProject.Models.Tests.GenreTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void InitializeBooks()
        {
            var genre = new Genre();

            Assert.IsNotNull(genre.Books);
        }
    }
}
