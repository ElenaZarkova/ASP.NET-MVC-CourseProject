using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.AspNet.Identity.EntityFramework;
using CourseProject.Models;

namespace CourseProject.Data
{
    public class BetterReadsDbContext : IdentityDbContext<User>
    {
        public BetterReadsDbContext()
            : base("BetterReads")
        {
        }

        public IDbSet<Book> Books { get; set; }

        public IDbSet<Rating> Ratings { get; set; }

        public IDbSet<Genre> Genres { get; set; }

        public static BetterReadsDbContext Create()
        {
            return new BetterReadsDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(x => x.WantToRead).WithMany(x => x.UsersWhoWantToRead);
            modelBuilder.Entity<User>().HasMany(x => x.CurrentlyReading).WithMany(x => x.UsersCurrentlyReading);
            modelBuilder.Entity<User>().HasMany(x => x.Read).WithMany(x => x.UsersRead);

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
