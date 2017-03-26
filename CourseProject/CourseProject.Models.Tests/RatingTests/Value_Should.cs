using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CourseProject.Models.Tests.RatingTests
{
    [TestFixture]
    public class Value_Should
    {
        [Test]
        public void HaveRequiredAttribute()
        {
            var rating = new Rating();

            var hasAttr = rating.GetType()
                .GetProperty("Value")
                .GetCustomAttributes(typeof(RequiredAttribute), false)
                .Any();

            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void HaveRangeAttribute()
        {
            var rating = new Rating();

            var hasAttr = rating.GetType()
                .GetProperty("Value")
                .GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.RangeAttribute), false)
                .Any();

            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void HaveRangeAttributeWithCorrectMinValue()
        {
            var rating = new Rating();

            var attr = rating.GetType()
                .GetProperty("Value")
                .GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.RangeAttribute), false)[0]
                as System.ComponentModel.DataAnnotations.RangeAttribute;

            Assert.AreEqual(1, attr.Minimum);
        }

        [Test]
        public void HaveRangeAttributeWithCorrectMaxValue()
        {
            var rating = new Rating();

            var attr = rating.GetType()
                .GetProperty("Value")
                .GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.RangeAttribute), false)[0]
                as System.ComponentModel.DataAnnotations.RangeAttribute;

            Assert.AreEqual(5, attr.Maximum);
        }

        [Test]
        public void HaveGetAndSet()
        {
            var rating = new Rating();

            rating.Value = 3;

            Assert.AreEqual(3, rating.Value);
        }
    }
}
