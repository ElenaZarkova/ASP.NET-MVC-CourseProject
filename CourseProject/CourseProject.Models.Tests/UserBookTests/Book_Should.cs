using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CourseProject.Models.Tests.UserBookTests
{
    [TestFixture]
    public class Book_Should
    {
        [Test]
        public void BeVirtualProperty()
        {
            var userBook = new UserBook();

            var isVirtual = userBook.GetType()
                .GetProperty("Book")
                .GetAccessors()[0].IsVirtual;

            Assert.IsTrue(isVirtual);
        }

        [Test]
        public void HaveGetAndSet()
        {
            var userBook = new UserBook();
            var book = new Book();

            userBook.Book = book;

            Assert.AreEqual(book, userBook.Book);

        }
    }
}
