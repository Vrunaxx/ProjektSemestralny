using System;
using System.ComponentModel.DataAnnotations;

namespace ProjektSemestralny.Classes
{
    /// <summary>
    /// Tabela łącząca Gry i Zamówienia 
    /// </summary>
    public class GameProductId
    {

        #region properties 

        /// <summary>
        /// Pole Id zamówienia gry
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Pole zawierające dane gry 
        /// </summary>
        public Game Game { get; set; }

        /// <summary>
        /// Pole zawierające dane zamówienia 
        /// </summary>
        public Order Order { get; set; }

        #endregion properties
        /// <summary>
        /// Konstruktor tworzący uniklane Id dla zamówień gier 
        /// </summary>
        public GameProductId()
        {
            Id = Guid.NewGuid();
        }

    }
}
