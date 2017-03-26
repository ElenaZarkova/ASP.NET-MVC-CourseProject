using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;
using CourseProject.Services.Contracts;
using CourseProject.Web.Mapping;
using CourseProject.Web.Controllers;
using CourseProject.Models;
using CourseProject.ViewModels.Search;

namespace CourseProject.Web.Tests.Controllers.SearchControllerTests
{
    [TestFixture]
    public class Index_Should
    {
        [Test]
        public void CallGenresServiceGetAllGenres()
        {
            // Arrange
            var mockedBooksService = new Mock<IBooksService>();
            var mockedGenresService = new Mock<IGenresService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            mockedGenresService.Setup(x => x.GetAllGenres()).Returns(new List<Genre>());

            var controller = new SearchController(mockedBooksService.Object, mockedGenresService.Object, mockedMapper.Object);

            // Act
            controller.Index();

            // Assert
            mockedGenresService.Verify(x => x.GetAllGenres(), Times.Once);
        }

        [Test]
        public void CallMapper()
        {
            // Arrange
            var mockedBooksService = new Mock<IBooksService>();
            var mockedGenresService = new Mock<IGenresService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            var genres = new List<Genre>();
            mockedGenresService.Setup(x => x.GetAllGenres()).Returns(genres);
            mockedMapper.Setup(x => x.Map<IEnumerable<GenreViewModel>>(genres)).Verifiable();
            var controller = new SearchController(mockedBooksService.Object, mockedGenresService.Object, mockedMapper.Object);

            // Act
            controller.Index();

            // Assert
            mockedMapper.Verify(x => x.Map<IEnumerable<GenreViewModel>>(genres), Times.Once);
        }

        [Test]
        public void ReturnDefaultView()
        {
            // Arrange
            var mockedBooksService = new Mock<IBooksService>();
            var mockedGenresService = new Mock<IGenresService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            var controller = new SearchController(mockedBooksService.Object, mockedGenresService.Object, mockedMapper.Object);

            // Act & Assert
            controller.WithCallTo(c => c.Index()).ShouldRenderDefaultView();
        }

        [Test]
        public void GiveTheViewAModelWithCorrectGenres()
        {
            // Arrange
            var mockedBooksService = new Mock<IBooksService>();
            var mockedGenresService = new Mock<IGenresService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            var genres = new List<Genre>();
            var mappedGenres = new List<GenreViewModel>();
            mockedGenresService.Setup(x => x.GetAllGenres()).Returns(genres);
            mockedMapper.Setup(x => x.Map<IEnumerable<GenreViewModel>>(genres)).Returns(mappedGenres);

            var controller = new SearchController(mockedBooksService.Object, mockedGenresService.Object, mockedMapper.Object);

            // Act & Assert
            controller.WithCallTo(c => c.Index()).ShouldRenderDefaultView()
                .WithModel<SearchViewModel>(m => m.Genres == mappedGenres);
        }
    }
}
