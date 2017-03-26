using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Ninject;
using CourseProject.Data.Contracts;
using CourseProject.Models;
using CourseProject.Web.App_Start;
using CourseProject.Services.Contracts;

namespace CourseProject.Services.IntegrationTests.GenresServiceIntegrationTests
{
    [TestFixture]
    public class GetAllGenres_Should
    {
        private static IList<Genre> genres = new List<Genre>()
        {
            new Genre() { Id = 1, Name = "Comedy" },
            new Genre() { Id = 2, Name = "History" },
            new Genre() { Id = 3, Name = "Romance" }
        };

        private static IKernel kernel;

        [SetUp]
        public void TestSetUp()
        {
            kernel = NinjectWebCommon.CreateKernel();
            IBetterReadsDbContext dbContext = kernel.Get<IBetterReadsDbContext>();

            foreach (Genre genre in genres)
            {
                dbContext.Genres.Add(genre);
            }

            dbContext.SaveChanges();
        }

        [TearDown]
        public void TestTearDown()
        {
            IBetterReadsDbContext dbContext = kernel.Get<IBetterReadsDbContext>();

            foreach (Genre genre in genres)
            {
                dbContext.Genres.Attach(genre);
                dbContext.Genres.Remove(genre);
            }

            dbContext.SaveChanges();
        }

        [Test]
        public void ReturnAllGenresInDatabase()
        {
            // Arrange
            var service = kernel.Get<IGenresService>();

            // Act
            var result = service.GetAllGenres().ToList();

            // Assert
            for (int i = 0; i < genres.Count; i++)
            {
                Assert.AreEqual(genres[i].Id, result[i].Id);
                Assert.AreEqual(genres[i].Name, result[i].Name);
            }
        }
    }
}
