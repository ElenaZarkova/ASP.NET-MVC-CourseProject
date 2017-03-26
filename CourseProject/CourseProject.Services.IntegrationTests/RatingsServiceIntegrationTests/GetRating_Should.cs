using System.Collections.Generic;
using NUnit.Framework;
using Ninject;
using CourseProject.Data.Contracts;
using CourseProject.Models;
using CourseProject.Web.App_Start;
using CourseProject.Services.Contracts;
using System;

namespace CourseProject.Services.IntegrationTests.RatingsServiceIntegrationTests
{
    [TestFixture]
    public class GetRating_Should
    {
        private static User user = new User() { Id = "userid1234", UserName = "pesho" };
        private static Genre genre = new Genre() { Name = "genre" };
        private static Book book = new Book()
        {
            Id = 12,
            Title = "Title",
            Author = "author",
            Description = "desc",
            PublishedOn = new DateTime(2017, 3, 5),
            CoverFile = "pic.jpg",
            Genre = genre
        };
        private static Rating rating = new Rating() { Value = 5, Book = book, User = user };

        private static IKernel kernel;

        [OneTimeSetUp]
        public void TestSetUp()
        {
            kernel = NinjectWebCommon.CreateKernel();
            IBetterReadsDbContext dbContext = kernel.Get<IBetterReadsDbContext>();

            dbContext.Ratings.Add(rating);

            dbContext.SaveChanges();
        }

        [OneTimeTearDown]
        public void TestTearDown()
        {
            IBetterReadsDbContext dbContext = kernel.Get<IBetterReadsDbContext>();

            dbContext.Ratings.Attach(rating);
            dbContext.Ratings.Remove(rating);

            dbContext.Books.Attach(book);
            dbContext.Books.Remove(book);

            dbContext.Users.Attach(user);
            dbContext.Users.Remove(user);
            
            dbContext.Genres.Attach(genre);
            dbContext.Genres.Remove(genre);

            dbContext.SaveChanges();
        }

        [Test]
        public void ReturnValue_WhenRatingExists()
        {
            // Arrange
            var service = kernel.Get<IRatingsService>();

            // Act
            var result = service.GetRating(book.Id, user.Id);

            // Assert
            Assert.AreEqual(rating.Value, result);
        }

        [Test]
        public void ReturnZero_WhenRatingNotFound()
        {
            // Arrange
            var service = kernel.Get<IRatingsService>();

            // Act
            var result = service.GetRating(15, "userid");

            // Assert
            Assert.AreEqual(0, result);
        }
    }
}
