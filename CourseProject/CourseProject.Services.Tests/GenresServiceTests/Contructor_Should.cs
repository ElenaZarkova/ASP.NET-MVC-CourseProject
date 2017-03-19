using System;
using Moq;
using NUnit.Framework;
using CourseProject.Services;
using CourseProject.Data.Contracts;

namespace CourseProject.Services.Tests.GenresServiceTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenDataIsNull()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new GenresService(null));
        }

        [Test]
        public void ThrowArgumentNullExceptionWithCorrectMessage_WhenDataIsNull()
        {
            // Act & Assert
            Assert.That(() => new GenresService(null), 
                Throws.ArgumentNullException.With.Message.Contains("data"));
        }

        [Test]
        public void NotThrow_WhenDataIsNotNull()
        {
            // Arrange
            var mockedData = new Mock<IBetterReadsData>();

            // Act & Assert
            Assert.DoesNotThrow(() => new GenresService(mockedData.Object));
        }
    }
}
