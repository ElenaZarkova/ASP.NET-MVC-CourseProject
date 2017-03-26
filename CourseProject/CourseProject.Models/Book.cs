using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace CourseProject.Models
{
    public class Book
    {
        private ICollection<Rating> ratings;
        private ICollection<UserBook> userBooks;

        public Book()
        {
            this.ratings = new HashSet<Rating>();
            this.userBooks = new HashSet<UserBook>();
        }

        public int Id { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [MinLength(1)]
        [MaxLength(50)]
        public string Title { get; set; }

        // TODO: maybe separate table
        [Required]
        [MinLength(3)]
        [MaxLength(80)]
        public string Author { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(300)]
        public string Description { get; set; }

        [Required]
        [Range(typeof(DateTime), "1/1/1400", "1/1/3000")]
        public DateTime PublishedOn { get; set; }

        [Required]
        public string CoverFile { get; set; }

        public int GenreId { get; set; }

        public virtual Genre Genre { get; set; }

        public virtual ICollection<Rating> Ratings
        {
            get { return this.ratings; }
            set { this.ratings = value; }
        }

        public virtual ICollection<UserBook> UserBooks
        {
            get { return this.userBooks; }
            set { this.userBooks = value; }
        }

        // Virtual so that it can be tested
        [NotMapped]
        public virtual double RatingCalculated
        {
            get
            {
                var count = this.Ratings.Count;
                if (count > 0)
                {
                    return this.Ratings.Sum(x => x.Value) / (double)count;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
