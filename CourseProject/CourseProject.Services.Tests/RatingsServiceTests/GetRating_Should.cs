using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using CourseProject.Services;
using CourseProject.Data.Contracts;
using CourseProject.Models;

namespace CourseProject.Services.Tests.RatingsServiceTests
{
    [TestFixture]
    public class GetRating_Should
    {
        [TestCase(3, "str", 11)]
        [TestCase(15, "user", 2)]
        public void ReturnCorrectRating_WhenFound(int bookId, string userId, int expectedValue)
        {
            // Arrange
            var mockedData = new Mock<IBetterReadsData>();

            var rating1Match = new Rating() { BookId = 3, UserId = "str", Value = 11 };
            var rating2Match = new Rating() { BookId = 15, UserId = "user", Value = 2 };
            var ratingNotMatch = new Rating() { BookId = 1, UserId = "str" };
            var ratings = new List<Rating>()
            {
                rating1Match,
                rating2Match,
                ratingNotMatch
            }.AsQueryable();

            mockedData.Setup(x => x.Ratings.All).Returns(ratings);
            mockedData.Setup(x => x.SaveChanges()).Verifiable();

            var service = new RatingsService(mockedData.Object);

            // Act
            var rating = service.GetRating(bookId, userId);

            // Assert
            Assert.AreEqual(expectedValue, rating);
        }

        [Test]
        public void ReturnZero_WhenRatingNotFound()
        {
            // Arrange
            var mockedData = new Mock<IBetterReadsData>();

            var ratings = new List<Rating>()
            {
                new Mock<Rating>().Object,
                new Mock<Rating>().Object,
                new Mock<Rating>().Object
            }.AsQueryable();

            mockedData.Setup(x => x.Ratings.All).Returns(ratings);

            var service = new RatingsService(mockedData.Object);

            // Act
            var rating = service.GetRating(1, "str");

            // Assert
            Assert.AreEqual(0, rating);
        }
    }
}
