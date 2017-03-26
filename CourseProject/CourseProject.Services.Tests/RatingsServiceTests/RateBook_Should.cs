using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using CourseProject.Data.Contracts;
using CourseProject.Models;

namespace CourseProject.Services.Tests.RatingsServiceTests
{
    [TestFixture]
    public class RateBook_Should
    {
        [Test]
        public void ChangeRatingValue_WhenRatingFound()
        {
            // Arrange
            var userId = "user-id-12424";
            var bookId = 24147;
            int rateValue = 5;
            var mockedData = new Mock<IBetterReadsData>();
            var rating1 = new Rating() { UserId = userId, BookId = bookId };
            var rating2 = new Rating() { UserId = userId, BookId = 2345 };
            var ratings = new List<Rating>()
            {
                rating1,
                rating2
            }.AsQueryable();

            mockedData.Setup(x => x.Ratings.All).Returns(ratings);

            var service = new RatingsService(mockedData.Object);

            // Act
            service.RateBook(bookId, userId, rateValue);

            // Assert
            Assert.AreEqual(rateValue, rating1.Value);
        }

        [Test]
        public void CreateNewRating_WhenRatingNotFound()
        {
            // Arrange
            var mockedData = new Mock<IBetterReadsData>();
            var rating1 = new Rating() { UserId = "user-id-1", BookId = 1234 };
            var rating2 = new Rating() { UserId = "user-id-2", BookId = 2345 };
            var ratings = new List<Rating>()
            {
                rating1,
                rating2
            }.AsQueryable();

            mockedData.Setup(x => x.Ratings.All).Returns(ratings);
            mockedData.Setup(x => x.Ratings.Add(It.IsAny<Rating>())).Verifiable();

            var service = new RatingsService(mockedData.Object);

            // Act
            service.RateBook(1, "radom-id", 5);

            // Assert
            mockedData.Verify(x => x.Ratings.Add(It.IsAny<Rating>()), Times.Once);
        }

        [Test]
        public void CreateNewRatingWithCorrectProperties_WhenRatingNotFound()
        {
            // Arrange
            var mockedData = new Mock<IBetterReadsData>();
            var rating1 = new Rating() { UserId = "user-id-1", BookId = 1234 };
            var rating2 = new Rating() { UserId = "user-id-2", BookId = 2345 };
            var ratings = new List<Rating>()
            {
                rating1,
                rating2
            }.AsQueryable();

            Rating addedRating = null;
            mockedData.Setup(x => x.Ratings.All).Returns(ratings);
            mockedData.Setup(x => x.Ratings.Add(It.IsAny<Rating>()))
                .Callback((Rating r) => addedRating = r);

            var service = new RatingsService(mockedData.Object);

            // Act
            service.RateBook(1, "radom-id", 5);

            // Assert
            mockedData.Verify(x => x.Ratings.Add(It.IsAny<Rating>()), Times.Once);
            Assert.AreEqual(1, addedRating.BookId);
            Assert.AreEqual("radom-id", addedRating.UserId);
            Assert.AreEqual(5, addedRating.Value);
        }

        [Test]
        public void CallDataSaveChanges_WhenRatingFound()
        {
            // Arrange
            var userId = "user-id-12424";
            var bookId = 24147;
            var mockedData = new Mock<IBetterReadsData>();

            var rating1 = new Rating() { UserId = userId, BookId = bookId };
            var ratings = new List<Rating>()
            {
                rating1
            }.AsQueryable();

            mockedData.Setup(x => x.Ratings.All).Returns(ratings);
            mockedData.Setup(x => x.SaveChanges()).Verifiable();

            var service = new RatingsService(mockedData.Object);

            // Act
            service.RateBook(bookId, userId, 5);

            // Assert
            mockedData.Verify(x => x.SaveChanges(), Times.Once);
        }

        [Test]
        public void CallDataSaveChanges_WhenRatingNotFound()
        {
            // Arrange
            var mockedData = new Mock<IBetterReadsData>();
            mockedData.Setup(x => x.Ratings.All).Returns(new List<Rating>().AsQueryable());
            mockedData.Setup(x => x.SaveChanges()).Verifiable();

            var service = new RatingsService(mockedData.Object);

            // Act
            service.RateBook(1, "radom-id", 5);

            // Assert
            mockedData.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
