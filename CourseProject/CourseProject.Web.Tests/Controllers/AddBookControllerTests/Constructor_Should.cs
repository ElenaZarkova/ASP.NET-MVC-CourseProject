using Moq;
using NUnit.Framework;
using CourseProject.Web.Controllers;
using CourseProject.Services.Contracts;
using CourseProject.Data.Contracts;
using CourseProject.Web.Areas.Admin.Controllers;

namespace CourseProject.Web.Tests.Controllers.AddBookControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        //[Test]
        //public void ThrowArgumentNullExceptionWithCorrectMessage_WhenBooksServiceIsNull()
        //{
        //    // Arrange
        //    var mockedGenresService = new Mock<IGenresService>();

        //    // Act & Assert
        //    Assert.That(() => new AddBookController(null, mockedGenresService.Object), Throws.ArgumentNullException.With.Message.Contains("booksService"));
        //}

        //[Test]
        //public void ThrowArgumentNullExceptionWithCorrectMessage_WhenGenresServiceIsNull()
        //{
        //    // Arrange
        //    var mockedBooksService = new Mock<IBooksService>();

        //    // Act & Assert
        //    Assert.That(() => new AddBookController(mockedBooksService.Object, null), Throws.ArgumentNullException.With.Message.Contains("genresService"));
        //}

        //[Test]
        //public void NotThrow_WhenAllParametersAreNotNull()
        //{
        //    // Arrange
        //    var mockedBooksService = new Mock<IBooksService>();
        //    var mockedGenresService = new Mock<IGenresService>();

        //    // Act & Assert
        //    Assert.DoesNotThrow(() => new AddBookController(mockedBooksService.Object, mockedGenresService.Object));
        //}
    }
}
