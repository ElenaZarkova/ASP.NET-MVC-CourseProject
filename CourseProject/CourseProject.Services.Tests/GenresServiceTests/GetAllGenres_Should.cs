using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using CourseProject.Services;
using CourseProject.Data.Contracts;
using CourseProject.Models;

namespace CourseProject.Services.Tests.GenresServiceTests
{
    [TestFixture]
    public class GetAllGenres_Should
    {
        [Test]
        public void ReturnCorrectListOfGenres()
        {
            // Arrange
            var mockedData = new Mock<IBetterReadsData>();
            var genres = new List<Genre>()
            {
                new Mock<Genre>().Object,
                new Mock<Genre>().Object,
                new Mock<Genre>().Object
            }.AsQueryable();

            mockedData.Setup(x => x.Genres.All).Returns(genres);

            var service = new GenresService(mockedData.Object);

            // Act
            var result = service.GetAllGenres();

            // Assert
            CollectionAssert.AreEqual(genres.ToList(), result);
        }
    }
}
