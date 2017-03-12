using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CourseProject.Data;
using CourseProject.Data.Migrations;

namespace CourseProject.Web
{
    public class DbConfig
    {
        public static void Intitalize()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BetterReadsDbContext, Configuration>());
            BetterReadsDbContext.Create().Database.Initialize(true);
        }
    }
}