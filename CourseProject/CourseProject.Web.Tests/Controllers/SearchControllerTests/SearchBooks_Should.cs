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
using CourseProject.Web.Attributes;
using CourseProject.ViewModels;
using CourseProject.ViewModels.Search;

namespace CourseProject.Web.Tests.Controllers.SearchControllerTests
{
    [TestFixture]
    public class SearchBooks_Should
    {
        [Test]
        public void HaveAjaxOnlyAttr()
        {
            var method = typeof(SearchController).GetMethod("SearchBooks");
            var hasAttr = method.GetCustomAttributes(typeof(AjaxOnlyAttribute), false).Any();

            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void HaveHttpPostAttr()
        {
            var method = typeof(SearchController).GetMethod("SearchBooks");
            var hasAttr = method.GetCustomAttributes(typeof(HttpPostAttribute), false).Any();

            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void CallBooksServiceSearchBooksWithCorrectParams()
        {
            // Arrange
            var searchWord = "abcv";
            var sortBy = "property";
            var chosenGenresIds = new List<int> { 1, 7 };
            var page = 15;

            var mockedBooksService = new Mock<IBooksService>();
            var mockedGenresService = new Mock<IGenresService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            var submitModel = new SearchSubmitModel()
            {
                SearchWord = searchWord,
                SortBy = sortBy,
                ChosenGenresIds = chosenGenresIds
            };

            mockedBooksService.Setup(x => x.SearchBooks(searchWord, chosenGenresIds, sortBy, page, Constants.BooksPerPage)).Verifiable();

            var controller = new SearchController(mockedBooksService.Object, mockedGenresService.Object, mockedMapper.Object);

            // Act
            controller.SearchBooks(submitModel, page);

            // Assert
            mockedBooksService.Verify(x => x.SearchBooks(searchWord, chosenGenresIds, sortBy, page, Constants.BooksPerPage), Times.Once);
        }
        
        [Test]
        public void CallBooksServiceSearchBooksWithPage1WhenPageIsNull()
        {
            // Arrange
            var searchWord = "abcv";
            var sortBy = "property";
            var chosenGenresIds = new List<int> { 1, 7 };

            var mockedBooksService = new Mock<IBooksService>();
            var mockedGenresService = new Mock<IGenresService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            var submitModel = new SearchSubmitModel()
            {
                SearchWord = searchWord,
                SortBy = sortBy,
                ChosenGenresIds = chosenGenresIds
            };

            mockedBooksService.Setup(x => x.SearchBooks(searchWord, chosenGenresIds, sortBy, 1, Constants.BooksPerPage)).Verifiable();

            var controller = new SearchController(mockedBooksService.Object, mockedGenresService.Object, mockedMapper.Object);

            // Act
            controller.SearchBooks(submitModel, null);

            // Assert
            mockedBooksService.Verify(x => x.SearchBooks(searchWord, chosenGenresIds, sortBy, 1, Constants.BooksPerPage), Times.Once);
        }
        
        [Test]
        public void CallBooksServiceGetBooksCountWithCorrectParams()
        {
            // Arrange
            var searchWord = "abcv";
            var chosenGenresIds = new List<int> { 1, 7 };

            var mockedBooksService = new Mock<IBooksService>();
            var mockedGenresService = new Mock<IGenresService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            var submitModel = new SearchSubmitModel()
            {
                SearchWord = searchWord,
                ChosenGenresIds = chosenGenresIds
            };

            mockedBooksService.Setup(x => x.GetBooksCount(searchWord, chosenGenresIds)).Verifiable();

            var controller = new SearchController(mockedBooksService.Object, mockedGenresService.Object, mockedMapper.Object);

            // Act
            controller.SearchBooks(submitModel, null);

            // Assert
            mockedBooksService.Verify(x => x.GetBooksCount(searchWord, chosenGenresIds), Times.Once);
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
            controller.SearchBooks(new SearchSubmitModel(), null);

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
            controller.WithCallTo(c => c.SearchBooks(new SearchSubmitModel(), null))
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
            controller.WithCallTo(c => c.SearchBooks(new SearchSubmitModel(), null))
                .ShouldRenderPartialView("_ResultsPartial")
                .WithModel<SearchResultsViewModel>(x => x.BooksCount == count);
        }
        
        [Test]
        public void ReturnViewModelWithCorrectSubmitModel()
        {
            // Arrange
            var mockedBooksService = new Mock<IBooksService>();
            var mockedGenresService = new Mock<IGenresService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            var submitModel = new SearchSubmitModel();

            var controller = new SearchController(mockedBooksService.Object, mockedGenresService.Object, mockedMapper.Object);

            // Act & Assert
            controller.WithCallTo(c => c.SearchBooks(submitModel, null))
                .ShouldRenderPartialView("_ResultsPartial")
                .WithModel<SearchResultsViewModel>(x => x.SubmitModel == submitModel);
        }
        
        [TestCase(9)]
        [TestCase(16)]
        public void ReturnViewModelWithCorrectPages(int count)
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
            controller.WithCallTo(c => c.SearchBooks(new SearchSubmitModel(), null))
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
            controller.WithCallTo(c => c.SearchBooks(new SearchSubmitModel(), null))
                .ShouldRenderPartialView("_ResultsPartial")
                .WithModel<SearchResultsViewModel>(x => x.Books == mappedBooks);
        }
    }
}
