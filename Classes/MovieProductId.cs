using System;
using System.ComponentModel.DataAnnotations;

namespace ProjektSemestralny.Classes
{
    /// <summary>
    /// Tabla łącząca Filmy i Zamówienia 
    /// </summary>
    public class MovieProductId
    {
        #region properties 
        /// <summary>
        /// Pole Id zamówienia filmu
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Pole zawierające dane filmu
        /// </summary>
        public Movie Movie { get; set; }

        /// <summary>
        /// Pole zawierające dane zamówienia 
        /// </summary>
        public Order Order { get; set; }

        #endregion properties

        /// <summary>
        /// Kontruktor tworzący uniklane Id dla zamówienia filmu
        /// </summary>
        public MovieProductId()
        {
            Id = Guid.NewGuid();
        }
    }
}
