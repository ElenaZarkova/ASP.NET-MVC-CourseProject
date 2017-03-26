using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;
using CourseProject.Models;
using CourseProject.Services.Contracts;
using CourseProject.Web.Common.Providers.Contracts;
using CourseProject.Web.Areas.Admin.Controllers;
using CourseProject.Web.Mapping;
using CourseProject.Web.Common;
using CourseProject.ViewModels.Admin.AddBook;

namespace CourseProject.Web.Tests.Areas.Admin.Controllers.AddBookControllerTests
{
    [TestFixture]
    public class Index_Should
    {
        // TODO: maybe decide on tests for caching
        private Mock<IBooksService> mockedBooksService;
        private Mock<IGenresService> mockedGenresService;
        private Mock<IUserProvider> mockedUserProvider;
        private Mock<IServerProvider> mockedServerProvider;
        private Mock<ICacheProvider> mockedCacheProvider;
        private Mock<IMapperAdapter> mockedMapper;
        private AddBookController controller;

        [SetUp]
        public void TestSetup()
        {
            this.mockedBooksService = new Mock<IBooksService>();
            this.mockedGenresService = new Mock<IGenresService>();
            this.mockedUserProvider = new Mock<IUserProvider>();
            this.mockedServerProvider = new Mock<IServerProvider>();
            this.mockedCacheProvider = new Mock<ICacheProvider>();
            this.mockedMapper = new Mock<IMapperAdapter>();

            this.controller = new AddBookController(
                this.mockedBooksService.Object,
                this.mockedGenresService.Object,
                this.mockedUserProvider.Object,
                this.mockedServerProvider.Object,
                this.mockedCacheProvider.Object,
                this.mockedMapper.Object);
        }

        [Test]
        public void ReturnDefaultView()
        {
            // Act & Assert
            this.controller.WithCallTo(c => c.Index())
                .ShouldRenderDefaultView();
        }

        [Test]
        public void ReturnDefaultViewWithCorrectModel()
        {
            // Act & Assert
            this.controller.WithCallTo(c => c.Index())
                .ShouldRenderDefaultView()
                .WithModel<AddBookViewModel>();
        }

        [Test]
        public void CallCacheForGenres()
        {
            // Arrange
            this.mockedCacheProvider.Setup(x => x.GetValue(It.IsAny<string>())).Verifiable();

            // Act
            this.controller.Index();

            // Assert
            this.mockedCacheProvider.Verify(x => x.GetValue(Constants.GenresCache), Times.Once);
        }

        [Test]
        public void ReturnCachedGenres_WhenCacheIsNotEmpty()
        {
            // Arrange
            var genres = new List<SelectListItem>();
            this.mockedCacheProvider.Setup(x => x.GetValue(Constants.GenresCache)).Returns(genres);

            // Act & Assert
            this.controller.WithCallTo(x => x.Index())
                .ShouldRenderDefaultView()
                .WithModel<AddBookViewModel>(m => m.Genres == genres);
        }

        [Test]
        public void CallGenresServiceGetGenres_WhenCacheIsEmpty()
        {
            // Arrange
            this.mockedGenresService.Setup(x => x.GetAllGenres()).Returns(new List<Genre>());
            this.mockedCacheProvider.Setup(x => x.GetValue(Constants.GenresCache)).Returns(null);

            // Act
            this.controller.Index();

            // Assert
            this.mockedGenresService.Verify(x => x.GetAllGenres(), Times.Once);
        }

        [Test]
        public void ReturnGenresFromGenresService_WhenCacheIsEmpty()
        {
            // Arrange
            var genres = new List<Genre>()
            {
                new Genre() { Id = 1, Name = "GenreName1" },
                new Genre() { Id = 2, Name = "GenreName2" }
            };
            this.mockedGenresService.Setup(x => x.GetAllGenres()).Returns(genres);

            this.mockedCacheProvider.Setup(x => x.GetValue(Constants.GenresCache)).Returns(null);

            // Act
            var result = this.controller.Index() as ViewResult;
            var model = result.Model as AddBookViewModel;

            // Assert
            var modelGenres = model.Genres.ToList();
            for (int i = 0; i < genres.Count; i++)
            {
                Assert.AreEqual(genres[i].Id.ToString(), modelGenres[i].Value);
                Assert.AreEqual(genres[i].Name, modelGenres[i].Text);
            }
        }

        [Test]
        public void SaveGenresToCache_WhenCacheIsEmpty()
        {
            // Arrange
            var genres = new List<Genre>()
            {
                new Genre() { Id = 1, Name = "GenreName1" },
                new Genre() { Id = 2, Name = "GenreName2" }
            };
            this.mockedGenresService.Setup(x => x.GetAllGenres()).Returns(genres);

            List<SelectListItem> cacheValue = null;
            this.mockedCacheProvider.Setup(x => x.GetValue(Constants.GenresCache)).Returns(null);
            this.mockedCacheProvider.Setup(x => x.InsertWithSlidingExpiration(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<int>()))
                .Callback((string key, object value, int minutes) =>
                {
                    cacheValue = (List<SelectListItem>)value;
                });

            // Act
            var result = this.controller.Index();

            // Assert
            for (int i = 0; i < genres.Count; i++)
            {
                Assert.AreEqual(genres[i].Id.ToString(), cacheValue[i].Value);
                Assert.AreEqual(genres[i].Name, cacheValue[i].Text);
            }
        }

        [Test]
        public void SaveGenresToCacheWithCorrectParameters_WhenCacheIsEmpty()
        {
            // Arrange
            this.mockedCacheProvider.Setup(x => x.GetValue(Constants.GenresCache)).Returns(null);
            this.mockedCacheProvider.Setup(x => x.InsertWithSlidingExpiration(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<int>()))
                .Verifiable();

            // Act
            var result = this.controller.Index();

            // Assert
            this.mockedCacheProvider.Verify(x => x.InsertWithSlidingExpiration(Constants.GenresCache, It.IsAny<object>(), Constants.GenresExpirationInMinutes), Times.Once);
        }
    }
}
