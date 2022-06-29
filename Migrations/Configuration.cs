using System.Data.Entity.Migrations;

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
