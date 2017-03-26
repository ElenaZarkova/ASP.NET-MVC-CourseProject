using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using NUnit.Framework;

namespace CourseProject.Models.Tests.BookTests
{
    [TestFixture]
    public class CoverFile_Should
    {
        [Test]
        public void HaveRequiredAttribute()
        {
            var book = new Book();

            var hasAttr = book.GetType()
                .GetProperty("CoverFile")
                .GetCustomAttributes(typeof(RequiredAttribute), false)
                .Any();

            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void HaveGetAndSet()
        {
            var book = new Book();

            book.CoverFile = "pic.jpg";

            Assert.AreEqual("pic.jpg", book.CoverFile);
        }
    }
}