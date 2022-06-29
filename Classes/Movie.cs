using System;
using System.ComponentModel.DataAnnotations;

namespace ProjektSemestralny.Classes
{
    /// <summary>
    /// Tworzu tabele Filmy
    /// </summary>
    public class Movie
    {
        #region properties
        /// <summary>
        /// Pole Id tabeli
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Pole nazwy filmu
        /// </summary>
        public string Title { get; set; }
        
        /// <summary>
        /// Pole reżysera filmu
        /// </summary>
        public string Autor { get; set; }

        /// <summary>
        /// Pole kategorii filmu
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Pole zawierające cenę filmu
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Pole zawierające datę wydania filmu
        /// </summary>
        public DateTime DateOrRelease { get; set; }

        /// <summary>
        /// Pole zaworające ilośc danego filmu w bazie danych
        /// </summary>
        public int StorageAmount { get; set; }

        #endregion properties

        /// <summary>
        /// Konstruktor tworzące uniklane Id dla filmów 
        /// </summary>
        public Movie()
        {
            Id = Guid.NewGuid();
        }
    }
}
