using CourseProject.Models;

namespace CourseProject.Services.Tests.BooksServiceTests.Mocks
{
    public class BookMockedRating : Book
    {
        private double ratingCalculated;

        public override double RatingCalculated
        {
            get
            {
                return this.ratingCalculated;
            }
        }

        public void SetRating(double rating)
        {
            this.ratingCalculated = rating;
        }
    }
}
