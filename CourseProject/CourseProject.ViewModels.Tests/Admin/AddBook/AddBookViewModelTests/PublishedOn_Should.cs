using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using NUnit.Framework;
using CourseProject.ViewModels.Admin.AddBook;

namespace CourseProject.ViewModels.Tests.Account.AddBookViewModelTests
{
    [TestFixture]
    public class PublishedOn_Should
    {
        [Test]
        public void HaveRequiredAttribute()
        {
            var viewModel = new AddBookViewModel();

            var hasAttr = viewModel.GetType()
                .GetProperty("PublishedOn")
                .GetCustomAttributes(typeof(RequiredAttribute), false)
                .Any();

            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void HaveDisplayAttribute()
        {
            var viewModel = new AddBookViewModel();

            var hasAttr = viewModel.GetType()
                .GetProperty("PublishedOn")
                .GetCustomAttributes(typeof(DisplayAttribute), false)
                .Any();

            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void HaveDisplayAttributeWithCorrectName()
        {
            var viewModel = new AddBookViewModel();

            var attr = viewModel.GetType()
                .GetProperty("PublishedOn")
                .GetCustomAttributes(typeof(DisplayAttribute), false)[0]
                as DisplayAttribute;

            Assert.AreEqual("Published on", attr.Name);
        }

        [Test]
        public void HaveRangeAttribute()
        {
            var viewModel = new AddBookViewModel();

            var hasAttr = viewModel.GetType()
                .GetProperty("PublishedOn")
                .GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.RangeAttribute), false)
                .Any();

            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void HaveRangeAttributeWithCorrectType()
        {
            var viewModel = new AddBookViewModel();

            var attr = viewModel.GetType()
                .GetProperty("PublishedOn")
                .GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.RangeAttribute), false)[0]
                as System.ComponentModel.DataAnnotations.RangeAttribute;

            Assert.AreEqual("DateTime", attr.OperandType.Name);
        }

        [Test]
        public void HaveRangeAttributeWithCorrectMinValue()
        {
            var viewModel = new AddBookViewModel();

            var attr = viewModel.GetType()
                .GetProperty("PublishedOn")
                .GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.RangeAttribute), false)[0]
                as System.ComponentModel.DataAnnotations.RangeAttribute;

            Assert.AreEqual("1/1/1400", attr.Minimum);
        }

        [Test]
        public void HaveRangeAttributeWithCorrectMaxValue()
        {
            var viewModel = new AddBookViewModel();

            var attr = viewModel.GetType()
                .GetProperty("PublishedOn")
                .GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.RangeAttribute), false)[0]
                as System.ComponentModel.DataAnnotations.RangeAttribute;

            Assert.AreEqual("1/1/3000", attr.Maximum);
        }

        [Test]
        public void HaveRangeAttributeWithCorrectErrorMessage()
        {
            var viewModel = new AddBookViewModel();

            var attr = viewModel.GetType()
                .GetProperty("PublishedOn")
                .GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.RangeAttribute), false)[0]
                as System.ComponentModel.DataAnnotations.RangeAttribute;

            Assert.AreEqual("Published on must be a date between years 1400 and 3000", attr.ErrorMessage);
        }

        [Test]
        public void HaveDisplayFormatAttribute()
        {
            var viewModel = new AddBookViewModel();

            var hasAttr = viewModel.GetType()
                .GetProperty("PublishedOn")
                .GetCustomAttributes(typeof(DisplayFormatAttribute), false)
                .Any();

            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void HaveDisplayFormatAttributeWithCorrectDataFormatString()
        {
            var viewModel = new AddBookViewModel();

            var attr = viewModel.GetType()
                .GetProperty("PublishedOn")
                .GetCustomAttributes(typeof(DisplayFormatAttribute), false)[0]
                as DisplayFormatAttribute;

            Assert.AreEqual("{0:dd/MM/yyyy}", attr.DataFormatString);
        }

        [Test]
        public void HaveDisplayFormatAttributeWithCorrectEditMode()
        {
            var viewModel = new AddBookViewModel();

            var attr = viewModel.GetType()
                .GetProperty("PublishedOn")
                .GetCustomAttributes(typeof(DisplayFormatAttribute), false)[0]
                as DisplayFormatAttribute;

            Assert.AreEqual(true, attr.ApplyFormatInEditMode);
        }

        [Test]
        public void HaveGetAndSet()
        {
            var viewModel = new AddBookViewModel();
            var date = new DateTime(2014, 5, 7);

            viewModel.PublishedOn = date;

            Assert.AreEqual(date, viewModel.PublishedOn);
        }
    }
}
