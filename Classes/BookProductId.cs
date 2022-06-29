using System;
using System.ComponentModel.DataAnnotations;

namespace ProjektSemestralny.Classes
{
    /// <summary>
    /// Tabela łącząca Książki i Zamówienia 
    /// </summary>
    public class BookProductId
    {

        #region properties 

        /// <summary>
        /// Pole Id Zamówienia ksiązki
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Pole zawierające dane książki
        /// </summary>
        public Book Book { get; set; }

        /// <summary>
        /// Pole zawierające dane zamówienia 
        /// </summary>
        public Order Order { get; set; }

        #endregion properties

        /// <summary>
        /// Konrtruktor tworzący nowe Id dp zamówienia książek
        /// </summary>
        public BookProductId()
        {
            Id = Guid.NewGuid();
        }

    }
}
