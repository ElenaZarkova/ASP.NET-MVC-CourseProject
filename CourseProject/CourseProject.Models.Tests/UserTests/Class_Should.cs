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
