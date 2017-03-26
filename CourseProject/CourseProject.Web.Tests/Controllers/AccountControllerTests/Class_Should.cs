using System;
using System.Web.Mvc;
using NUnit.Framework;
using CourseProject.Web.Controllers;

namespace CourseProject.Web.Tests.Controllers.AccountControllerTests
{
    [TestFixture]
    public class Class_Should
    {
        [Test]
        public void HaveAuthorizeAttribute()
        {
            var attr = Attribute.GetCustomAttribute(typeof(AccountController), typeof(AuthorizeAttribute));

            Assert.IsNotNull(attr);
        }
    }
}
