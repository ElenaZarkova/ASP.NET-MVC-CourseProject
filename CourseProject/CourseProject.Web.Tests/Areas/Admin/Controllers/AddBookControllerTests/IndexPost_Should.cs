using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using CourseProject.Services.Contracts;
using CourseProject.Web.Common.Providers.Contracts;
using CourseProject.Web.Mapping;
using CourseProject.Web.Areas.Admin.Controllers;
using CourseProject.ViewModels.Admin.AddBook;
using TestStack.FluentMVCTesting;
using CourseProject.Web.Common;
using System.Web.Mvc;
using CourseProject.Models;
using System.Web;

namespace CourseProject.Web.Tests.Areas.Admin.Controllers.AddBookControllerTests
{
    [TestFixture]
    public class IndexPost_Should
    {
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
                mockedBooksService.Object,
                mockedGenresService.Object,
                mockedUserProvider.Object,
                mockedServerProvider.Object,
                mockedCacheProvider.Object,
                mockedMapper.Object);
        }

        [Test]
        public void ReturnViewWithModel_WhenModelStateIsNotValid()
        {
            // Arrange
            this.controller.ModelState.AddModelError("error", "message");

            var submitModel = new AddBookViewModel();

            // Act & Assert
            controller.WithCallTo(c => c.Index(submitModel))
                .ShouldRenderDefaultView()
                .WithModel(submitModel)
                .AndModelError("error");
        }

        [Test]
        public void CallCacheForGenres_WhenModelIsNotValid()
        {
            // Arrange
            this.mockedCacheProvider.Setup(x => x.GetValue(It.IsAny<string>())).Verifiable();
            this.controller.ModelState.AddModelError("error", "message");

            var submitModel = new AddBookViewModel();

            // Act
            this.controller.Index(submitModel);

            // Assert
            this.mockedCacheProvider.Verify(x => x.GetValue(Constants.GenresCache), Times.Once);
        }

        [Test]
        public void ReturnCachedGenres_WhenCacheIsNotEmptyAndModelIsNotValid()
        {
            // Arrange
            var genres = new List<SelectListItem>();
            this.mockedCacheProvider.Setup(x => x.GetValue(Constants.GenresCache)).Returns(genres);
            this.controller.ModelState.AddModelError("error", "message");

            var submitModel = new AddBookViewModel();

            // Act & Assert
            this.controller.WithCallTo(x => x.Index(submitModel))
                .ShouldRenderDefaultView()
                .WithModel<AddBookViewModel>(m => m.Genres == genres);
        }

        [Test]
        public void CallGenresServiceGetGenres_WhenCacheIsEmptyAndModelIsNotValid()
        {
            // Arrange
            this.mockedGenresService.Setup(x => x.GetAllGenres()).Returns(new List<Genre>());
            this.mockedCacheProvider.Setup(x => x.GetValue(Constants.GenresCache)).Returns(null);
            this.controller.ModelState.AddModelError("error", "message");

            var submitModel = new AddBookViewModel();

            // Act
            this.controller.Index(submitModel);

            // Assert
            this.mockedGenresService.Verify(x => x.GetAllGenres(), Times.Once);
        }

        [Test]
        public void ReturnGenresFromGenresService_WhenCacheIsEmptyAndModelIsNotValid()
        {
            // Arrange
            var genres = new List<Genre>()
            {
                new Genre() { Id = 1, Name = "GenreName1" },
                new Genre() { Id = 2, Name = "GenreName2" }
            };
            this.mockedGenresService.Setup(x => x.GetAllGenres()).Returns(genres);

            this.mockedCacheProvider.Setup(x => x.GetValue(Constants.GenresCache)).Returns(null);
            this.controller.ModelState.AddModelError("error", "message");

            var submitModel = new AddBookViewModel();

            // Act
            var result = this.controller.Index(submitModel) as ViewResult;
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
        public void SaveGenresToCache_WhenCacheIsEmptyAndModelIsNotValid()
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
            this.controller.ModelState.AddModelError("error", "message");

            var submitModel = new AddBookViewModel();

            // Act
            var result = this.controller.Index(submitModel);

            // Assert
            for (int i = 0; i < genres.Count; i++)
            {
                Assert.AreEqual(genres[i].Id.ToString(), cacheValue[i].Value);
                Assert.AreEqual(genres[i].Name, cacheValue[i].Text);
            }
        }

        [Test]
        public void SaveGenresToCacheWithCorrectParameters_WhenCacheIsEmptyAndModelIsNotValid()
        {
            // Arrange
            var genres = new List<Genre>()
            {
                new Genre() { Id = 1, Name = "GenreName1" },
                new Genre() { Id = 2, Name = "GenreName2" }
            };
            this.mockedGenresService.Setup(x => x.GetAllGenres()).Returns(genres);

            this.mockedCacheProvider.Setup(x => x.InsertWithSlidingExpiration(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<int>()))
                .Verifiable();
            this.controller.ModelState.AddModelError("error", "message");

            var submitModel = new AddBookViewModel();

            // Act
            var result = this.controller.Index(submitModel);

            // Assert
            this.mockedCacheProvider.Verify(x => x.InsertWithSlidingExpiration(Constants.GenresCache, It.IsAny<object>(), Constants.GenresExpirationInMinutes), Times.Once);
        }
        
        [Test]
        public void ReturnViewWithModelError_WhenFileIsNull()
        {
            // Arrange
            var submitModel = new AddBookViewModel();
            submitModel.CoverFile = null;

            // Act & Assert
            this.controller.WithCallTo(x=>x.Index(submitModel))
                .ShouldRenderDefaultView()
                .WithModel(submitModel)
                .AndModelErrorFor(m => m.CoverFile)
                .ThatEquals(Constants.CoverFileErrorMessage);
        }

        [Test]
        public void ReturnViewWithModelError_WhenFileIsNotImage()
        {
            // Arrange
            var submitModel = new AddBookViewModel();
            var mockedFile = new Mock<HttpPostedFileBase>();
            mockedFile.Setup(x => x.ContentType).Returns("not image");
            submitModel.CoverFile = mockedFile.Object;

            // Act & Assert
            this.controller.WithCallTo(x => x.Index(submitModel))
                .ShouldRenderDefaultView()
                .WithModel(submitModel)
                .AndModelErrorFor(m => m.CoverFile)
                .ThatEquals(Constants.CoverFileErrorMessage);
        }

        [Test]
        public void ReturnModelWithGenres_WhenFileIsNotValid()
        {
            // Arrange
            var genres = new List<SelectListItem>();
            this.mockedCacheProvider.Setup(x => x.GetValue(Constants.GenresCache)).Returns(genres);

            var submitModel = new AddBookViewModel();
            submitModel.CoverFile = null;

            // Act & Assert
            this.controller.WithCallTo(x => x.Index(submitModel))
                .ShouldRenderDefaultView()
                .WithModel<AddBookViewModel>(m => m.Genres == genres);
        }

        [Test]
        public void CheckIfBookExists_WhenFileIsImage()
        {
            // Arrange
            var submitModel = new AddBookViewModel();
            var mockedFile = new Mock<HttpPostedFileBase>();
            mockedFile.Setup(x => x.ContentType).Returns("image/jpg");
            submitModel.CoverFile = mockedFile.Object;

            var title = "BookTitle";
            submitModel.Title = title;

            this.mockedBooksService.Setup(x => x.BookWithTitleExists(It.IsAny<string>())).Verifiable();

            // Act
            this.controller.Index(submitModel);

            // Assert
            this.mockedBooksService.Verify(x => x.BookWithTitleExists(title), Times.Once);
        }

        [Test]
        public void ReturnViewWithModelError_WhenBookTitleExists()
        {
            // Arrange
            var submitModel = new AddBookViewModel();
            var mockedFile = new Mock<HttpPostedFileBase>();
            mockedFile.Setup(x => x.ContentType).Returns("image/jpg");
            submitModel.CoverFile = mockedFile.Object;

            var title = "BookTitle";
            submitModel.Title = title;

            this.mockedBooksService.Setup(x => x.BookWithTitleExists(title)).Returns(true);

            // Act & Assert
            this.controller.WithCallTo(x => x.Index(submitModel))
                .ShouldRenderDefaultView()
                .WithModel(submitModel)
                .AndModelErrorFor(m => m.Title)
                .ThatEquals(Constants.TitleExistsErrorMessage);
        }

        [Test]
        public void ReturnModelWithGenres_WhenBookTitleExists()
        {
            // Arrange
            var submitModel = new AddBookViewModel();
            var mockedFile = new Mock<HttpPostedFileBase>();
            mockedFile.Setup(x => x.ContentType).Returns("image/jpg");
            submitModel.CoverFile = mockedFile.Object;

            var title = "BookTitle";
            submitModel.Title = title;

            this.mockedBooksService.Setup(x => x.BookWithTitleExists(title)).Returns(true);

            var genres = new List<SelectListItem>();
            this.mockedCacheProvider.Setup(x => x.GetValue(Constants.GenresCache)).Returns(genres);

            // Act
            var result = this.controller.Index(submitModel) as ViewResult;
            var model = result.Model as AddBookViewModel;

            // Assert
            Assert.AreEqual(genres, model.Genres);
        }

        [Test]
        public void CallServerProviderMapPathWithCorrectPath_WhenModelIsValid()
        {
            // Arrange
            var submitModel = new AddBookViewModel();
            var mockedFile = new Mock<HttpPostedFileBase>();
            mockedFile.Setup(x => x.ContentType).Returns("image/jpg");

            var filename = "filename.jpg";
            mockedFile.Setup(x => x.FileName).Returns(filename);
            submitModel.CoverFile = mockedFile.Object;

            this.mockedServerProvider.Setup(x => x.MapPath(It.IsAny<string>())).Verifiable();

            // Act
            this.controller.Index(submitModel);

            // Assert
            this.mockedServerProvider.Verify(x => x.MapPath(Constants.ImagesRelativePath + filename), Times.Once);
        }

        [Test]
        public void SaveImageToCorrectLocation_WhenModelIsValid()
        {
            // Arrange
            var submitModel = new AddBookViewModel();
            var mockedFile = new Mock<HttpPostedFileBase>();
            mockedFile.Setup(x => x.ContentType).Returns("image/jpg");

            var filename = "filename.jpg";
            mockedFile.Setup(x => x.FileName).Returns(filename);
            mockedFile.Setup(x => x.SaveAs(It.IsAny<string>())).Verifiable();
            submitModel.CoverFile = mockedFile.Object;

            var absolutePath = "C:\\absolute\\path";
            this.mockedServerProvider.Setup(x => x.MapPath(It.IsAny<string>())).Returns(absolutePath);

            // Act
            this.controller.Index(submitModel);

            // Assert
            mockedFile.Verify(x => x.SaveAs(absolutePath), Times.Once);
        }
        
        [Test]
        public void CallMapperWithCorrectObject_WhenModelIsValid()
        {
            // Arrange
            var submitModel = new AddBookViewModel();
            var mockedFile = new Mock<HttpPostedFileBase>();
            mockedFile.Setup(x => x.ContentType).Returns("image/jpg");
            submitModel.CoverFile = mockedFile.Object;

            this.mockedMapper.Setup(x => x.Map<BookModel>(It.IsAny<AddBookViewModel>())).Verifiable();
            // Act
            this.controller.Index(submitModel);

            // Assert
            this.mockedMapper.Verify(x => x.Map<BookModel>(submitModel), Times.Once);
        }

        [Test]
        public void CallBooksServiceAddBookWithCorrectParams_WhenModelIsValid()
        {
            // Arrange
            var submitModel = new AddBookViewModel();
            var mockedFile = new Mock<HttpPostedFileBase>();
            mockedFile.Setup(x => x.ContentType).Returns("image/jpg");

            var filename = "filename.jpg";
            mockedFile.Setup(x => x.FileName).Returns(filename); 
            submitModel.CoverFile = mockedFile.Object;

            var mockedBookModel = new Mock<BookModel>();
            this.mockedMapper.Setup(x => x.Map<BookModel>(It.IsAny<AddBookViewModel>())).Returns(mockedBookModel.Object);
           
            this.mockedBooksService.Setup(x => x.AddBook(It.IsAny<BookModel>(), It.IsAny<string>())).Verifiable();

            // Act
            this.controller.Index(submitModel);

            // Assert
            this.mockedBooksService.Verify(x => x.AddBook(mockedBookModel.Object, filename), Times.Once);
        }

        [Test]
        public void AddMessageToTempdata_WhenAdditionIsSuccessful()
        {
            // Arrange
            var submitModel = new AddBookViewModel();
            var mockedFile = new Mock<HttpPostedFileBase>();
            mockedFile.Setup(x => x.ContentType).Returns("image/jpg");
            submitModel.CoverFile = mockedFile.Object;
            
            // Act
            this.controller.Index(submitModel);

            // Assert
            Assert.AreEqual(Constants.AddBookSuccessMessage, this.controller.TempData[Constants.AddBookSuccessKey]);
        }

        [Test]
        public void RedirectToCorrectRoute_WhenAdditionIsSuccessful()
        {
            // Arrange
            var submitModel = new AddBookViewModel();
            var mockedFile = new Mock<HttpPostedFileBase>();
            mockedFile.Setup(x => x.ContentType).Returns("image/jpg");
            submitModel.CoverFile = mockedFile.Object;
            
            var id = 5;
            this.mockedBooksService.Setup(x => x.AddBook(It.IsAny<BookModel>(), It.IsAny<string>())).Returns(id);
            
            // Act & Assert
            this.controller.WithCallTo(c => c.Index(submitModel))
                .ShouldRedirectTo("/book/" + id);
        }
    }
}
