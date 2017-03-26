using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CourseProject.Models.Tests.UserTests
{
    [TestFixture]
    public class Class_Should
    {
        [Test]
        public void InheritIdentityUser()
        {
            Assert.IsTrue(typeof(User).IsSubclassOf(typeof(IdentityUser)));
        }
    }
}
