using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Text;

namespace ProjektSemestralny.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ProjektSemestralnyDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }
    }
}
