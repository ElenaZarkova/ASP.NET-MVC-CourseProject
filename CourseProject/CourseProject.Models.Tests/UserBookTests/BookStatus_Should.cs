using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CourseProject.Models.Tests.UserBookTests
{
    [TestFixture]
    public class BookStatus_Should
    {
        [Test]
        public void HaveGetAndSet()
        {
            var userBook = new UserBook();

            userBook.BookStatus = BookStatus.CurrentlyReading;

            Assert.AreEqual(BookStatus.CurrentlyReading, userBook.BookStatus);

        }
    }
}
