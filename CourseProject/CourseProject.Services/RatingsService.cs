using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseProject.Data.Contracts;
using CourseProject.Models;
using CourseProject.Services.Contracts;

namespace CourseProject.Services
{
    public class RatingsService : IRatingsService
    {
        private readonly IBetterReadsData data;

        public RatingsService(IBetterReadsData data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            this.data = data;
        }

        public int GetRating(int bookId, string userId)
        {
            var rating = this.data.Ratings.All.FirstOrDefault(x => x.UserId == userId && x.BookId == bookId);
            if (rating != null)
            {
                return rating.Value;
            }
            else
            {
                return 0;
            }
        }

        public void RateBook(int bookId, string userId, int rate)
        {
            // TODO: Should it check rate range ?

            var rating = this.data.Ratings.All.Where(x => x.BookId == bookId && x.UserId == userId).FirstOrDefault();
            if (rating != null)
            {
                rating.Value = rate;
            }
            else
            {
                // TODO: should check if book and user in database ??
                rating = new Rating()
                {
                    BookId = bookId,
                    UserId = userId,
                    Value = rate
                };
                this.data.Ratings.Add(rating);
            }

            this.data.SaveChanges();
        }
    }
}
