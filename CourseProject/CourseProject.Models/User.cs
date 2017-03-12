using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CourseProject.Models
{
    public class User : IdentityUser
    {
        // TODO: Should it be one table with additional field
        private ICollection<Book> wantToRead;
        private ICollection<Book> currentlyReading;
        private ICollection<Book> read;
        private ICollection<Rating> ratings;

        public User()
        {
            this.wantToRead = new HashSet<Book>();
            this.currentlyReading = new HashSet<Book>();
            this.read = new HashSet<Book>();
            this.ratings = new HashSet<Rating>();
        }

        public virtual ICollection<Book> WantToRead
        {
            get { return this.wantToRead; }
            set { this.wantToRead = value;  }
        }

        public virtual ICollection<Book> CurrentlyReading
        {
            get { return this.currentlyReading; }
            set { this.currentlyReading = value; }
        }

        public virtual ICollection<Book> Read
        {
            get { return this.read; }
            set { this.read = value; }
        }

        public virtual ICollection<Rating> Ratings
        {
            get { return this.ratings; }
            set { this.ratings = value; }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);           
            // Add custom user claims here
            return userIdentity;
        }
    }
}
