using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;
using CourseProject.Models;
using CourseProject.Services.Contracts;
using CourseProject.Web.Mapping;
using CourseProject.Web.Controllers;
using CourseProject.Web.Common;
using CourseProject.ViewModels;
using CourseProject.ViewModels.Search;

namespace CourseProject.Web.Tests.Controllers.SearchControllerTests
{
    // TODO: maybe remove caching tests
    [TestFixture]
    public class SearchInitial_Should
    {
        [Test]
        public void HaveChildActionOnlyAttr()
        {
            var method = typeof(SearchController).GetMethod("SearchInitial");
            var hasAttr = method.GetCustomAttributes(typeof(ChildActionOnlyAttribute), false).Any();

            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void CallBooksServiceSearchBooksWithCorrectParams()
        {
            // Arrange
            var mockedBooksService = new Mock<IBooksService>();
            var mockedGenresService = new Mock<IGenresService>();
            var mockedMapper = new Mock<IMapperAdapter>();

            mockedBooksService.Setup(x => x.SearchBooks(null, null, null, 1, Constants.BooksPerPage)).Verifiable();

            var controller = new SearchController(mockedBooksService.Object, mockedGenresService.Object, mockedMapper.Object);

            // Act
            controller.SearchInitial();

            // Assert
            mockedBooksService.Verify(x => x.SearchBooks(null, null, null, 1, Constants.BooksPerPage), Times.Once);
        }
        
        [Test]
        public void CallBooksServiceGetBooksCountWithCorrectParams()
        {
            // Arrange
            var mockedBooksService = new Mock<IBooksService>();
            var mockedGenresService = new Mock<IGenresService>();
            var mockedMapper = new Mock<IMapperAdapter>();

            mockedBooksService.Setup(x => x.GetBooksCount(null, null)).Verifiable();

            var controller = new SearchController(mockedBooksService.Object, mockedGenresService.Object, mockedMapper.Object);

            // Act
            controller.SearchInitial();

            // Assert
            mockedBooksService.Verify(x => x.GetBooksCount(null, null), Times.Once);
        }

        [Test]
        public void CallMapper()
        {
            // Arrange
            var mockedBooksService = new Mock<IBooksService>();
            var mockedGenresService = new Mock<IGenresService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            var books = new List<Book>();
            mockedBooksService.Setup(x => x.SearchBooks(It.IsAny<string>(), It.IsAny<IEnumerable<int>>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(books);
            mockedMapper.Setup(x => x.Map<IEnumerable<BookViewModel>>(It.IsAny<IEnumerable<Book>>())).Verifiable();

            var controller = new SearchController(mockedBooksService.Object, mockedGenresService.Object, mockedMapper.Object);

            // Act
            controller.SearchInitial();

            // Assert
            mockedMapper.Verify(x => x.Map<IEnumerable<BookViewModel>>(books), Times.Once);
        }

        [Test]
        public void ReturnCorrectPartialView()
        {
            // Arrange
            var mockedBooksService = new Mock<IBooksService>();
            var mockedGenresService = new Mock<IGenresService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            var controller = new SearchController(mockedBooksService.Object, mockedGenresService.Object, mockedMapper.Object);

            // Act & Assert
            controller.WithCallTo(c => c.SearchInitial())
                .ShouldRenderPartialView("_ResultsPartial");
        }

        [Test]
        public void ReturnViewModelWithCorrectCount()
        {
            // Arrange
            var count = 956;
            var mockedBooksService = new Mock<IBooksService>();
            var mockedGenresService = new Mock<IGenresService>();
            var mockedMapper = new Mock<IMapperAdapter>();

            mockedBooksService.Setup(x => x.GetBooksCount(It.IsAny<string>(), It.IsAny<IEnumerable<int>>())).Returns(count);

            var controller = new SearchController(mockedBooksService.Object, mockedGenresService.Object, mockedMapper.Object);

            // Act & Assert
            controller.WithCallTo(c => c.SearchInitial())
                .ShouldRenderPartialView("_ResultsPartial")
                .WithModel<SearchResultsViewModel>(x => x.BooksCount == count);
        }

        [Test]
        public void ReturnViewModelWithSubmitModelNotNull()
        {
            // Arrange
            var mockedBooksService = new Mock<IBooksService>();
            var mockedGenresService = new Mock<IGenresService>();
            var mockedMapper = new Mock<IMapperAdapter>();

            var controller = new SearchController(mockedBooksService.Object, mockedGenresService.Object, mockedMapper.Object);

            // Act & Assert
            controller.WithCallTo(c => c.SearchInitial())
                .ShouldRenderPartialView("_ResultsPartial")
                .WithModel<SearchResultsViewModel>(model => 
                {
                    Assert.IsNotNull(model);
                });
        }

        [TestCase(9)]
        [TestCase(16)]
        public void ReturnViewModelWithCorrectSubmitModel(int count)
        {
            // Arrange
            int pages = (int)Math.Ceiling((double)count / Constants.BooksPerPage);
            var mockedBooksService = new Mock<IBooksService>();
            var mockedGenresService = new Mock<IGenresService>();
            var mockedMapper = new Mock<IMapperAdapter>();

            mockedBooksService.Setup(x => x.GetBooksCount(It.IsAny<string>(), It.IsAny<IEnumerable<int>>()))
                .Returns(count);

            var controller = new SearchController(mockedBooksService.Object, mockedGenresService.Object, mockedMapper.Object);

            // Act & Assert
            controller.WithCallTo(c => c.SearchInitial())
                .ShouldRenderPartialView("_ResultsPartial")
                .WithModel<SearchResultsViewModel>(x => x.Pages == pages);
        }

        [Test]
        public void ReturnViewModelWithCorrectBooks()
        {
            // Arrange
            var mockedBooksService = new Mock<IBooksService>();
            var mockedGenresService = new Mock<IGenresService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            var mappedBooks = new List<BookViewModel>();
            mockedMapper.Setup(x => x.Map<IEnumerable<BookViewModel>>(It.IsAny<IEnumerable<Book>>()))
                .Returns(mappedBooks);

            var controller = new SearchController(mockedBooksService.Object, mockedGenresService.Object, mockedMapper.Object);

            // Act & Assert
            controller.WithCallTo(c => c.SearchInitial())
                .ShouldRenderPartialView("_ResultsPartial")
                .WithModel<SearchResultsViewModel>(x => x.Books == mappedBooks);
        }
    }
}
