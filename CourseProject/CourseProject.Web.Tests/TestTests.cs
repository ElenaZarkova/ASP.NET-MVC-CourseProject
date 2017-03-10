using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;

namespace CourseProject.Web.Tests
{
    public interface ITest
    {
        int Number { get; set; }
    }

    [TestFixture]
    public class TestTests
    {
        [Test]
        public void BestTestEveeeeer()
        {
            var mockedObj = new Mock<ITest>();
            mockedObj.Setup(x => x.Number).Returns(42);

            Assert.AreEqual(mockedObj.Object.Number, 42);
        }
    }
}
