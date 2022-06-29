using System;
using System.ComponentModel.DataAnnotations;

namespace ProjektSemestralny.Classes
{
    /// <summary>
    /// Tworzy tabele Gry
    /// </summary>
    public class Game
    {
        #region properties 

        /// <summary>
        /// Pole Id tabeli 
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Pole Nazwy gier
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Pole kreatora gry
        /// </summary>
        public string Autor { get; set; }

        /// <summary>
        /// Pole katergorie gier 
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Pole zawierające ceny gier
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Pole zawierające datę wydania gry
        /// </summary>
        public DateTime DateOrRelease { get; set; }

        /// <summary>
        /// Pole zawierające ilość danej gry w bazie danych
        /// </summary>
        public int StorageAmount { get; set; }

        #endregion properties

        /// <summary>
        /// Konstrukator tworzący unikalne Id dla gier
        /// </summary>
        public Game()
        {
            Id = Guid.NewGuid();
        }
    }

    
}
