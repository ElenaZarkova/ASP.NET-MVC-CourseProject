using System.Collections.Generic;
using NUnit.Framework;
using Ninject;
using CourseProject.Data.Contracts;
using CourseProject.Models;
using CourseProject.Web.App_Start;
using CourseProject.Services.Contracts;

namespace CourseProject.Services.IntegrationTests.UsersSerivceIntegrationTests
{
    [TestFixture]
    public class CheckIfUserExists_Should
    {
        private static IList<User> users = new List<User>()
        {
            new User() { UserName = "pesho1" },
            new User() { UserName = "pesho2" },
            new User() { UserName = "peshoto" }
        };

        private static IKernel kernel;

        [SetUp]
        public void TestSetUp()
        {
            kernel = NinjectWebCommon.CreateKernel();
            IBetterReadsDbContext dbContext = kernel.Get<IBetterReadsDbContext>();

            foreach (User user in users)
            {
                dbContext.Users.Add(user);
            }

            dbContext.SaveChanges();
        }

        [TearDown]
        public void TestTearDown()
        {
            IBetterReadsDbContext dbContext = kernel.Get<IBetterReadsDbContext>();

            foreach (User user in users)
            {
                dbContext.Users.Attach(user);
                dbContext.Users.Remove(user);
            }

            dbContext.SaveChanges();
        }

        [Test]
        public void ReturnTrue_WhenUserExists()
        {
            // Arrange
            var service = kernel.Get<IUsersService>();

            // Act
            var result = service.CheckIfUserExists("peshoto");

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void ReturnFalse_WhenUserNotFound()
        {
            // Arrange
            var service = kernel.Get<IUsersService>();

            // Act
            var result = service.CheckIfUserExists("pesho4");

            // Assert
            Assert.IsFalse(result);
        }
    }
}
