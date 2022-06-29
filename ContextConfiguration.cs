using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace ProjektSemestralny
{
    public class ContextConfiguration : DbConfiguration
    {
        public ContextConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
        }
    }
}
