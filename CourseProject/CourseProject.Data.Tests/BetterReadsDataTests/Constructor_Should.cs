using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using CourseProject.Data.Contracts;
using CourseProject.Data.Repositories;
using CourseProject.Data.Tests.Repositories.EfRepositoryTests.Mocks;
using CourseProject.Models;

namespace CourseProject.Data.Tests.BetterReadsDataTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullExceptionWithCorrectMessage_WhenDbContextIsNull()
        {
            // Arrange
            var mockedUsers = new Mock<IEfRepository<User>>();
            var mockedBooks = new Mock<IEfRepository<Book>>();
            var mockedGenres = new Mock<IEfRepository<Genre>>();
            var mockedRatings = new Mock<IEfRepository<Rating>>();

            // Act & Assert
            Assert.That(() => new BetterReadsData(
                null,
                mockedUsers.Object,
                mockedBooks.Object,
                mockedGenres.Object,
                mockedRatings.Object), 
                Throws.ArgumentNullException.With.Message.Contains("Database context cannot be null."));
        }

        [Test]
        public void NotThrow_WhenAllArgumetsAreNotNull()
        {
            // Arrange
            var mockedContext = new Mock<IBetterReadsDbContext>();
            var mockedUsers = new Mock<IEfRepository<User>>();
            var mockedBooks = new Mock<IEfRepository<Book>>();
            var mockedGenres = new Mock<IEfRepository<Genre>>();
            var mockedRatings = new Mock<IEfRepository<Rating>>();

            // Act & Assert
            Assert.DoesNotThrow(() => new BetterReadsData(
                mockedContext.Object,
                mockedUsers.Object,
                mockedBooks.Object,
                mockedGenres.Object,
                mockedRatings.Object));
        }

        public void Copy()
        {
            var mockedContext = new Mock<IBetterReadsDbContext>();
            var mockedUsers = new Mock<IEfRepository<User>>();
            var mockedBooks = new Mock<IEfRepository<Book>>();
            var mockedGenres = new Mock<IEfRepository<Genre>>();
            var mockedRatings = new Mock<IEfRepository<Rating>>();
        }
    }
}