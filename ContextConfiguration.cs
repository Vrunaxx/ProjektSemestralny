using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace ProjektSemestralny
{
    /// <summary>
    /// Logika za połączeniem do bazy danych
    /// </summary>
    public class ContextConfiguration : DbConfiguration
    {
        /// <summary>
        /// Ustawia strategię egzekucji połączenia
        /// </summary>
        public ContextConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
        }
    }
}
