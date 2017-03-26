using NUnit.Framework;
using System.Data.Entity;
using CourseProject.Data;
using CourseProject.Services.IntegrationTests;

// no namespace so that it works for the whole assembly and not per namespace

[SetUpFixture]
public class AssemblyInitializer
{
    [OneTimeSetUp]
    public void Setup()
    {
        Database.SetInitializer(new MigrateDatabaseToLatestVersion<BetterReadsDbContext, TestsDatabaseConfiguration>());
    }
}
