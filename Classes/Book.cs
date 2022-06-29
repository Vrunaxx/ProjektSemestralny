using System;
using System.ComponentModel.DataAnnotations;

namespace ProjektSemestralny.Classes
{
    /// <summary>
    /// Tworzy tabele Book zawierającą pola poniżej.
    /// </summary>
    public class Book
    {

        #region properties 

        /// <summary>
        /// Pole Id tabeli 
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Pole Tytuły książek
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Pole Autorzy książek
        /// </summary>
        public string Autor { get; set; }

        /// <summary>
        /// Pole Kategorie książek
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Pole Ceny książek
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Pole Daty wydania ksiązek 
        /// </summary>
        public DateTime DateOrRelease { get; set; }

        /// <summary>
        /// Pole zawierające ilość danej książki w bazie danych
        /// </summary>
        public int StorageAmount { get; set; }

        #endregion properties

        /// <summary>
        /// Kontrukator tworzący uniklane Id
        /// </summary>
        public Book()
        {
            Id = Guid.NewGuid();
        }

    }
}
