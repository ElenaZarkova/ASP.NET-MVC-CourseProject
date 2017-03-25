using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.AspNet.Identity.EntityFramework;
using CourseProject.Models;
using CourseProject.Data.Contracts;

namespace CourseProject.Data
{
    public class BetterReadsDbContext : IdentityDbContext<User>, IBetterReadsDbContext
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

        IDbSet<T> IBetterReadsDbContext.Set<T>()
        {
            return this.Set<T>();
        }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
