using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using NUnit.Framework;

namespace CourseProject.Models.Tests.BookTests
{
    [TestFixture]
    public class PublishedOn_Should
    {
        [Test]
        public void HaveRequiredAttribute()
        {
            var book = new Book();

            var hasAttr = book.GetType()
                .GetProperty("PublishedOn")
                .GetCustomAttributes(typeof(RequiredAttribute), false)
                .Any();

            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void HaveRangeAttribute()
        {
            var book = new Book();

            var hasAttr = book.GetType()
                .GetProperty("PublishedOn")
                .GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.RangeAttribute), false)
                .Any();

            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void HaveRangeAttributeWithCorrectType()
        {
            var book = new Book();

            var attr = book.GetType()
                .GetProperty("PublishedOn")
                .GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.RangeAttribute), false)[0]
                as System.ComponentModel.DataAnnotations.RangeAttribute;

            Assert.AreEqual("DateTime", attr.OperandType.Name);
        }

        [Test]
        public void HaveRangeAttributeWithCorrectMinValue()
        {
            var book = new Book();

            var attr = book.GetType()
                .GetProperty("PublishedOn")
                .GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.RangeAttribute), false)[0]
                as System.ComponentModel.DataAnnotations.RangeAttribute;

            Assert.AreEqual("1/1/1400", attr.Minimum);
        }

        [Test]
        public void HaveRangeAttributeWithCorrectMaxValue()
        {
            var book = new Book();

            var attr = book.GetType()
                .GetProperty("PublishedOn")
                .GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.RangeAttribute), false)[0]
                as System.ComponentModel.DataAnnotations.RangeAttribute;

            Assert.AreEqual("1/1/3000", attr.Maximum);
        }

        [Test]
        public void HaveGetAndSet()
        {
            var book = new Book();
            var date = new DateTime(2014, 5, 7);

            book.PublishedOn = date;

            Assert.AreEqual(date, book.PublishedOn);
        }
    }
}
