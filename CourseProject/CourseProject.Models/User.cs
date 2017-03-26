using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CourseProject.Models
{
    public class User : IdentityUser
    {
        private ICollection<Rating> ratings;
        private ICollection<UserBook> userBooks;

        public User()
        {
            this.ratings = new HashSet<Rating>();
            this.userBooks = new HashSet<UserBook>();
        }
        
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

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);           
            
            // Add custom user claims here
            return userIdentity;
        }
    }
}
