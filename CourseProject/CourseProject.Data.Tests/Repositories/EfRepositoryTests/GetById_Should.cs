using System.Data.Entity;
using NUnit.Framework;
using Moq;
using CourseProject.Data.Contracts;
using CourseProject.Data.Repositories;
using CourseProject.Data.Tests.Repositories.EfRepositoryTests.Mocks;

namespace CourseProject.Data.Tests.Repositories.EfRepositoryTests
{
    [TestFixture]
    public class GetById_Should
    {
        [TestCase(7)]
        [TestCase("the-id-as-string")]
        public void CallDbSetFindMethod(object id)
        {
            // Arrange
            var mockedContext = new Mock<IBetterReadsDbContext>();
            var mockedSet = new Mock<IDbSet<MockedModel>>();
            mockedContext.Setup(x => x.Set<MockedModel>()).Returns(mockedSet.Object);
            mockedSet.Setup(x => x.Find(It.IsAny<object>())).Verifiable();

            var repository = new EfRepository<MockedModel>(mockedContext.Object);

            // Act
            repository.GetById(id);

            // Assert
            mockedSet.Verify(x => x.Find(It.IsAny<object>()), Times.Once);
        }

        [TestCase(7)]
        [TestCase("the-id-of-the-object-9273872")]
        public void CallDbSetFindMethodWithCorrectParameter(object id)
        {
            // Arrange
            var mockedContext = new Mock<IBetterReadsDbContext>();
            var mockedSet = new Mock<IDbSet<MockedModel>>();
            mockedContext.Setup(x => x.Set<MockedModel>()).Returns(mockedSet.Object);
            mockedSet.Setup(x => x.Find(It.IsAny<object>())).Verifiable();

            var repository = new EfRepository<MockedModel>(mockedContext.Object);

            // Act
            repository.GetById(id);

            // Assert
            mockedSet.Verify(x => x.Find(id), Times.Once);
        }

        [Test]
        public void ReturnTheCorrectResult()
        {
            // Arrange
            var mockedContext = new Mock<IBetterReadsDbContext>();
            var mockedSet = new Mock<IDbSet<MockedModel>>();
            var mockedModel = new MockedModel();
            mockedContext.Setup(x => x.Set<MockedModel>()).Returns(mockedSet.Object);
            mockedSet.Setup(x => x.Find(It.IsAny<object>())).Returns(mockedModel);

            var repository = new EfRepository<MockedModel>(mockedContext.Object);

            // Act
            var result = repository.GetById("super random string 49023748");

            // Assert
            Assert.AreEqual(mockedModel, result);
        }
    }
}