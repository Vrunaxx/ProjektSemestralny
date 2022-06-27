using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProjektSemestralny.Classes
{
    public class Order
    {
        #region properties 

        [Key]
        public Guid Id { get; set; }
        public int OrderId { get; set; }

        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }
        public ObservableCollection<Book> CollectionOfBooks { get; set; }
        public ObservableCollection<Game> CollectionOfGames { get; set; }
        public ObservableCollection<Movie> CollectionOfMovies { get; set; }

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
