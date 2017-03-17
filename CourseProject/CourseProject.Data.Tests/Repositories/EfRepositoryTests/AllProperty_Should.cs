using NUnit.Framework;
using Moq;
using CourseProject.Data.Contracts;
using CourseProject.Data.Repositories;
using CourseProject.Data.Tests.Repositories.EfRepositoryTests.Mocks;
using System.Data.Entity;

namespace CourseProject.Data.Tests.Repositories.EfRepositoryTests
{
    [TestFixture]
    public class AllProperty_Should
    {
        [Test]
        public void ReturnDbSet()
        {
            // Arrange
            var mockedContext = new Mock<IBetterReadsDbContext>();
            var mockedSet = new Mock<IDbSet<MockedModel>>();
            mockedContext.Setup(x => x.Set<MockedModel>()).Returns(mockedSet.Object);

            var repository = new EfRepository<MockedModel>(mockedContext.Object);

            // Act
            var result = repository.All;

            // Assert
            Assert.AreEqual(mockedSet.Object, result);
        }
    }
}
