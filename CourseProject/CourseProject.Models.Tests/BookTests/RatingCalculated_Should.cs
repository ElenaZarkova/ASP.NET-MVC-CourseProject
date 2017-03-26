using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace CourseProject.Models.Tests.BookTests
{
    [TestFixture]
    public class RatingCalculated_Should
    {
        [Test]
        public void ReturnCorrectValue_WhenThereAreRatings()
        {
            var book = new Book();
            var ratings = new List<Rating>()
            {
                new Rating() { Value = 3 },
                new Rating() { Value = 4 },
                new Rating() { Value = 1 }
            };
            book.Ratings = ratings;

            var result = book.RatingCalculated;

            var expected = ratings.Sum(x => x.Value) / (double)ratings.Count;
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ReturnZero_WhenRatingListIsEmpty()
        {
            var book = new Book();

            var result = book.RatingCalculated;
            
            Assert.AreEqual(0, result);
        }
    }
}
