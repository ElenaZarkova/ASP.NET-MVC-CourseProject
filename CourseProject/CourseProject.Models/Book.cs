using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace CourseProject.Models
{
    public class Book
    {
        private ICollection<User> usersWhoWantToRead;
        private ICollection<User> usersCurrentlyReading;
        private ICollection<User> usersRead;
        private ICollection<Rating> ratings;
        
        public Book()
        {
            this.usersWhoWantToRead = new HashSet<User>();
            this.usersCurrentlyReading = new HashSet<User>();
            this.usersRead = new HashSet<User>();
            this.ratings = new HashSet<Rating>();
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
        public DateTime PublishedOn { get; set; }

        [Required]
        public string CoverFile { get; set; }
         
        public int GenreId { get; set; }

        public virtual Genre Genre { get; set; }

        public virtual ICollection<User> UsersWhoWantToRead
        {
            get { return this.usersWhoWantToRead; }
            set { this.usersWhoWantToRead = value; }
        }

        public virtual ICollection<User> UsersCurrentlyReading
        {
            get { return this.usersCurrentlyReading; }
            set { this.usersCurrentlyReading = value; }
        }

        public virtual ICollection<User> UsersRead
        {
            get { return this.usersRead; }
            set { this.usersRead = value; }
        }

        public virtual ICollection<Rating> Ratings
        {
            get { return this.ratings; }
            set { this.ratings = value; }
        }

        [NotMapped]
        public double RatingCalculated
        {
            get
            {
                return this.Ratings.Count / this.Ratings.Sum(x => x.Value);
            }
        }
    }
}
