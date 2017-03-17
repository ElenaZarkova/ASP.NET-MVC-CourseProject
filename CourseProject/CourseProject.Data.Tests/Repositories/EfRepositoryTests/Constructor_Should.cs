using System;
using Moq;
using NUnit.Framework;
using CourseProject.Data.Contracts;
using CourseProject.Data.Repositories;
using CourseProject.Data.Tests.Repositories.EfRepositoryTests.Mocks;

namespace CourseProject.Data.Tests.Repositories.EfRepositoryTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenDbContextIsNull()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new EfRepository<MockedModel>(null));
        }

        [Test]
        public void ThrowArgumentNullExceptionWithCorrectMessage_WhenDbContextIsNull()
        {
            // Act & Assert
            Assert.That(() => new EfRepository<MockedModel>(null), 
                Throws.ArgumentNullException.With.Message.Contains("Database context cannot be null."));
        }

        [Test]
        public void NotThrow_WhenDbContextIdNotNull()
        {
            // Arrange
            var mockedContext = new Mock<IBetterReadsDbContext>();

            // Act & Assert
            Assert.DoesNotThrow(() => new EfRepository<MockedModel>(mockedContext.Object));
        }

        [Test]
        public void CallContextSetMethod()
        {
            // Arrange
            var mockedContext = new Mock<IBetterReadsDbContext>();
            mockedContext.Setup(x => x.Set<MockedModel>()).Verifiable();

            // Act
            var repository = new EfRepository<MockedModel>(mockedContext.Object);

            // Assert
            mockedContext.Verify(x => x.Set<MockedModel>(), Times.Once);
        }
    }
}
