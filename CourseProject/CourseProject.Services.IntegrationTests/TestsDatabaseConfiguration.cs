using System.Data.Entity.Migrations;

namespace CourseProject.Services.IntegrationTests
{
    public sealed class TestsDatabaseConfiguration : DbMigrationsConfiguration<CourseProject.Data.BetterReadsDbContext>
    {
        public TestsDatabaseConfiguration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }
    }
}
