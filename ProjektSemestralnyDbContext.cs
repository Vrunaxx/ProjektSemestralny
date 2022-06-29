using ProjektSemestralny.Classes;
using System.Data.Entity;

namespace ProjektSemestralny
{
    /// <summary>
    /// Code First bazy danych oraz definicje dla tabel
    /// </summary>
    [DbConfigurationType(typeof(ContextConfiguration))]
    public class ProjektSemestralnyDbContext : DbContext
    {
        /// <summary>
        /// Definicja tabeli Zamówień
        /// </summary>
        public virtual IDbSet<Order> Orders { get; set; }

        /// <summary>
        /// Definicja tabeli łączącej Filmy i Zamówienia
        /// </summary>
        public virtual IDbSet<MovieProductId> MovieProductIds{ get; set; }

        /// <summary>
        /// Definicja tabeli łączącej Gry i Zamówienia
        /// </summary>
        public virtual IDbSet<GameProductId> GameProductIds { get; set; }

        /// <summary>
        /// Definicja tabeli łączącej Książki i Zamówienia
        /// </summary>
        public virtual IDbSet<BookProductId> BookProductIds { get; set; }

        /// <summary>
        /// Definicja tabeli Filmy
        /// </summary>
        public virtual IDbSet<Movie> Movies { get; set; }

        /// <summary>
        /// Definicja tabeli Gry
        /// </summary>
        public virtual IDbSet<Game> Games { get; set; }

        /// <summary>
        /// Definicja tabeli Książek
        /// </summary>
        public virtual IDbSet<Book> Books { get; set; }
    }
}
