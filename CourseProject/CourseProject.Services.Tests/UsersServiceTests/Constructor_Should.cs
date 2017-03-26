using System;
using Moq;
using NUnit.Framework;
using CourseProject.Services;
using CourseProject.Data.Contracts;

namespace CourseProject.Services.Tests.UsersServiceTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenDataIsNull()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new UsersService(null));
        }

        [Test]
        public void ThrowArgumentNullExceptionWithCorrectMessage_WhenDataIsNull()
        {
            // Act & Assert
            Assert.That(() => new UsersService(null), Throws.ArgumentNullException.With.Message.Contains("data"));
        }

        [Test]
        public void NotThrow_WhenDataIsNotNull()
        {
            // Arrange
            var mockedData = new Mock<IBetterReadsData>();

            // Act & Assert
            Assert.DoesNotThrow(() => new UsersService(mockedData.Object));
        }
    }
}
