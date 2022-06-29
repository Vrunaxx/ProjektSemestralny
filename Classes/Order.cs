using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace ProjektSemestralny.Classes
{
    /// <summary>
    /// Tabela zamówień
    /// </summary>
    public class Order
    {
        #region properties 

        /// <summary>
        /// Pole Id zamówienia 
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Pole upraszczający wygląd Id z "1jkhkh-1j4g1hg-12j3bhgh" do "10001"
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Pole daty utworzenia zamówienia
        /// </summary>
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// Zbiornik który przechowuje Książki w zamówieniu 
        /// </summary>
        public ObservableCollection<Book> CollectionOfBooks { get; set; }

        /// <summary>
        /// Zbiornik który przechowuje Gry w zamówieniu 
        /// </summary>
        public ObservableCollection<Game> CollectionOfGames { get; set; }

        /// <summary>
        /// Zbiornik który przechowuje Filmy w zamówieniu 
        /// </summary>
        public ObservableCollection<Movie> CollectionOfMovies { get; set; }

        /// <summary>
        /// Konstruktor tworzący uniklane Id, Ustawia datę zamówienia na bierzącą oraz tworzy nowe zbiorniki na Ksiażki, Gry i Filmy
        /// </summary>
        public Order()
        {
            Id = Guid.NewGuid();
            OrderDate = DateTime.Now;

            CollectionOfBooks = new ObservableCollection<Book>();
            CollectionOfGames = new ObservableCollection<Game>();
            CollectionOfMovies = new ObservableCollection<Movie>();
        }

        #endregion properties
    }
}
