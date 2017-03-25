using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using CourseProject.Web.Controllers;
using CourseProject.Services.Contracts;
using CourseProject.Data.Contracts;
using CourseProject.Models;
using System.Web.Mvc;
using CourseProject.Web.Areas.Admin.Controllers;
using System.Web;
using System.Web.Caching;
using System.Web.Routing;
using CourseProject.Web.Common;

namespace CourseProject.Web.Tests.Controllers.AddBookControllerTests
{
    [TestFixture]
    public class Index_Should
    {
        //[PostTest]
        //public void PostTest()
        //{
        //    HttpRuntime.Cache.Remove(Constants.GenresCache);
        //}

        //[Test]
        //public void ReturnViewResult()
        //{
        //    // Arrange
        //    var mockedBooksService = new Mock<IBooksService>();
        //    var mockedGenresService = new Mock<IGenresService>();
        //    var mockedContext = new Mock<HttpContextBase>();
        //    mockedContext.Setup(x => x.Cache).Returns(HttpRuntime.Cache);
        //    mockedGenresService.Setup(x => x.GetAllGenres()).Returns(new List<Genre>());

        //    var controller = new AddBookController(mockedBooksService.Object, mockedGenresService.Object);
        //    controller.ControllerContext = new ControllerContext(mockedContext.Object, new RouteData(), controller);
        //    // Act
        //    var result = controller.Index();

        //    // Assert
        //    Assert.IsInstanceOf<ViewResult>(result);
        //}

        //[Test]
        //public void ReturnViewResultWithCorrectModelType()
        //{
        //    // Arrange
        //    var mockedBooksService = new Mock<IBooksService>();
        //    var mockedGenresService = new Mock<IGenresService>();
        //    var mockedContext = new Mock<HttpContextBase>();
        //    mockedContext.Setup(x => x.Cache).Returns(HttpRuntime.Cache);
        //    mockedGenresService.Setup(x => x.GetAllGenres()).Returns(new List<Genre>());

        //    var controller = new AddBookController(mockedBooksService.Object, mockedGenresService.Object);
        //    controller.ControllerContext = new ControllerContext(mockedContext.Object, new RouteData(), controller);

        //    // Act
        //    var result = controller.Index() as ViewResult;

        //    // Assert
        //    Assert.IsInstanceOf<AddBookViewModel>(result.Model);
        //}

        //[Test]
        //public void ReturnViewResultWithCorrectGenres()
        //{
        //    // Arrange
        //    var mockedBooksService = new Mock<IBooksService>();
        //    var mockedGenresService = new Mock<IGenresService>();
        //    var mockedContext = new Mock<HttpContextBase>();
        //    mockedContext.Setup(x => x.Cache).Returns(HttpRuntime.Cache);
        //    var genres = new List<Genre>()
        //    {
        //        new Genre() {Id = 1, Name = "Comedy" },
        //        new Genre() {Id = 2, Name = "Non-fiction" },
        //        new Genre() {Id = 3, Name = "Historical fiction" }
        //    };
        //    mockedGenresService.Setup(x => x.GetAllGenres()).Returns(genres);

        //    var controller = new AddBookController(mockedBooksService.Object, mockedGenresService.Object);
        //    controller.ControllerContext = new ControllerContext(mockedContext.Object, new RouteData(), controller);

        //    // Act
        //    var result = (ViewResult)controller.Index();

        //    // Assert
        //    var model = (AddBookViewModel)result.Model;
        //    var selectList = model.Genres.ToList();
        //    Assert.AreEqual(genres.Count, selectList.Count);
        //    for (int i = 0; i < genres.Count; i++)
        //    {
        //        Assert.AreEqual(genres[i].Id.ToString(), selectList[i].Value);
        //        Assert.AreEqual(genres[i].Name, selectList[i].Text);
        //    }
        //}
    }
}
