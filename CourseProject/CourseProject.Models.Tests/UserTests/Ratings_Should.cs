using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;

namespace CourseProject.Models.Tests.UserTests
{
    [TestFixture]
    public class Ratings_Should
    {
        [Test]
        public void BeVirtualProperty()
        {
            var user = new User();
            var isVirtual = user.GetType()
                .GetProperty("Ratings")
                .GetAccessors()[0].IsVirtual;

            Assert.IsTrue(isVirtual);
        }

        [Test]
        public void HaveGetAndSet()
        {
            var user = new User();
            var ratings = new List<Rating>() { new Mock<Rating>().Object, new Mock<Rating>().Object };

            user.Ratings = ratings;

            CollectionAssert.AreEqual(ratings, user.Ratings);
        }
    }
}
