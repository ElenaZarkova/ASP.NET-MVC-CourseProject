using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using NUnit.Framework;
using CourseProject.Web.Areas.Admin.Controllers;
using CourseProject.Web.Common;

namespace CourseProject.Web.Tests.Areas.Admin.Controllers.AddBookControllerTests
{
    [TestFixture]
    public class Class_Should
    {
        [Test]
        public void HaveAuthorizeAttribute()
        {
            var attr = Attribute.GetCustomAttribute(typeof(AddBookController), typeof(AuthorizeAttribute));

            Assert.IsNotNull(attr);
        }

        [Test]
        public void HaveAuthorizeAttributeWithRoleAdmin()
        {
            var attr = Attribute.GetCustomAttribute(typeof(AddBookController), typeof(AuthorizeAttribute)) as AuthorizeAttribute;

            Assert.AreEqual(Constants.AdminRole, attr.Roles);
        }
    }
}
