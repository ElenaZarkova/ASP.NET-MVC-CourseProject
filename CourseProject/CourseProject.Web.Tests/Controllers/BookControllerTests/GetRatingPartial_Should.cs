using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using CourseProject.Services.Contracts;
using CourseProject.Web.Mapping;
using CourseProject.Web.Controllers;

namespace CourseProject.Web.Tests.Controllers.BookControllerTests
{
    [TestFixture]
    public class GetRatingPartial_Should
    {
        public void NotThrow_WhenDataIsNotNull()
        {
            // Arrange
            var mockedBooksService = new Mock<IBooksService>();
            var mockedRatingsService = new Mock<IRatingsService>();
            var mockedMapper = new Mock<IMapperAdapter>();

            // Act & Assert
            Assert.DoesNotThrow(() => new BookController(mockedBooksService.Object, mockedRatingsService.Object, mockedMapper.Object));
        }
    }
}
