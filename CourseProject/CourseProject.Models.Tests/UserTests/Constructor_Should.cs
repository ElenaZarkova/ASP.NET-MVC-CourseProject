using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using CourseProject.Models;

namespace CourseProject.Models.Tests.UserTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void InitializeRatings()
        {
            var user = new User();

            Assert.IsNotNull(user.Ratings);
        }

        [Test]
        public void InitializeUserBooks()
        {
            var user = new User();

            Assert.IsNotNull(user.UserBooks);
        }
    }
}
