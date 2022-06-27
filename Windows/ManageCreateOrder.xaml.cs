using ProjektSemestralny.Classes;
using ProjektSemestralny.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProjektSemestralny
{
    /// <summary>
    /// Interaction logic for Menage.xaml
    /// </summary>
    public partial class ManageOrder
    {
        private ProjektSemestralnyDbContext PSDbContext;
        private ObservableCollection<Book> ObsvBooks;
        private ObservableCollection<Movie> ObsvMovies;
        private ObservableCollection<Game> ObsvGames;
        private Order order;
        private ItemType itemType;

        public ManageOrder()
        {
            this.InitializeComponent();
            SetContext();
            ObsvBooks = new ObservableCollection<Book>(PSDbContext.Books);
            ObsvMovies = new ObservableCollection<Movie>(PSDbContext.Movies);
            ObsvGames = new ObservableCollection<Game>(PSDbContext.Games);
            StockList.ItemsSource = ObsvBooks;
            order = new Order();
            OrderListBooks.ItemsSource = order.CollectionOfBooks;
            OrderListMovies.ItemsSource = order.CollectionOfMovies;
            OrderListGames.ItemsSource = order.CollectionOfGames;
            itemType = ItemType.Book;

            FilterBy.ItemsSource = new string[] { "Title", "Category", "Autor" };
        }
        private void SetContext()
        {
            PSDbContext = new ProjektSemestralnyDbContext();
        }

        #region Filters
        //public Predicate<object> GetFilter()
        //{
        //    var type = StockList.ItemsSource;

        //    switch (FilterBy.SelectedItem as string)
        //    {
        //        case "Title":
        //            return TitleFilter;

        //        case "Category":
        //            return CategoryFilter;

        //        case "Autor":
        //            return AutorFilter;
        //    }

        //    return TitleFilter;
        //}
        #region Book
        private bool TitleFilterBook(object obj)
        {
            var FilterObj = obj as Book;

            return FilterObj.Title.Contains(FilterTextBox.Text, StringComparison.OrdinalIgnoreCase);
        }

        private bool CategoryFilterBook(object obj)
        {
            var FilterObj = obj as Book;

            return FilterObj.Category.Contains(FilterTextBox.Text, StringComparison.OrdinalIgnoreCase);
        }
        private bool AutorFilterBook(object obj)
        {
            var FilterObj = obj as Book;

            return FilterObj.Autor.Contains(FilterTextBox.Text, StringComparison.OrdinalIgnoreCase);
        }
        #endregion
        #region Games
        private bool TitleFilterGame(object obj)
        {
            var FilterObj = obj as Game;

            return FilterObj.Title.Contains(FilterTextBox.Text, StringComparison.OrdinalIgnoreCase);
        }

        private bool CategoryFilterGame(object obj)
        {
            var FilterObj = obj as Game;

            return FilterObj.Category.Contains(FilterTextBox.Text, StringComparison.OrdinalIgnoreCase);
        }
        private bool AutorFilterGame(object obj)
        {
            var FilterObj = obj as Game;

            return FilterObj.Autor.Contains(FilterTextBox.Text, StringComparison.OrdinalIgnoreCase);
        }
        #endregion
        #region Movies
        private bool TitleFilterMovie(object obj)
        {
            var FilterObj = obj as Movie;

            return FilterObj.Title.Contains(FilterTextBox.Text, StringComparison.OrdinalIgnoreCase);
        }

        private bool CategoryFilterMovie(object obj)
        {
            var FilterObj = obj as Movie;

            return FilterObj.Category.Contains(FilterTextBox.Text, StringComparison.OrdinalIgnoreCase);
        }
        private bool AutorFilterMovie(object obj)
        {
            var FilterObj = obj as Movie;

            return FilterObj.Autor.Contains(FilterTextBox.Text, StringComparison.OrdinalIgnoreCase);
        }
        #endregion

        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (FilterTextBox.Text == null)
            //{
            //    OrderList.Items.Filter = null;
            //}
            //else
            //{
            //    OrderList.Items.Filter = GetFilter();
            //}
        }

        private void FilterBy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //OrderList.Items.Filter = GetFilter();
        }

        #endregion

        #region Sort
        private void Button_Click_ASC(object sender, RoutedEventArgs e)
        {
            Remove_Sort();
            StockList.Items.SortDescriptions.Add(new SortDescription("Title", ListSortDirection.Ascending));
        }

        private void Button_Click_DESC(object sender, RoutedEventArgs e)
        {
            Remove_Sort();
            StockList.Items.SortDescriptions.Add(new SortDescription("Title", ListSortDirection.Descending));
        }

        private void Remove_Sort()
        {
            if (StockList.Items.SortDescriptions.Any())
                StockList.Items.SortDescriptions.Remove(StockList.Items.SortDescriptions.First());
        }

        #endregion

        #region Load
        private void Book_Load(object sender, RoutedEventArgs e)
        {
            StockList.ItemsSource = ObsvBooks;
            itemType = ItemType.Book;
        }

        private void Movie_Load(object sender, RoutedEventArgs e)
        {
            StockList.ItemsSource = ObsvMovies;
            itemType = ItemType.Movie;
        }

        private void Game_Load(object sender, RoutedEventArgs e)
        {
            StockList.ItemsSource = ObsvGames;
            itemType = ItemType.Game;
        }
        #endregion

        #region Add/Remove/Save Order
        private void Add_Order(object sender, RoutedEventArgs e)
        {
            switch (itemType)
            {
                case ItemType.Book:
                    order.CollectionOfBooks.Add((Book)StockList.SelectedItem);
                    break;

                case ItemType.Game:
                    order.CollectionOfGames.Add((Game)StockList.SelectedItem);
                    break;

                case ItemType.Movie:
                    order.CollectionOfMovies.Add((Movie)StockList.SelectedItem);
                    break;
            }
        }

        private void Remove_Order(object sender, RoutedEventArgs e)
        {
            switch (itemType)
            {
                case ItemType.Book:
                    order.CollectionOfBooks.Remove((Book)OrderListBooks.SelectedItem);
                    break;

                case ItemType.Game:
                    order.CollectionOfGames.Remove((Game)OrderListGames.SelectedItem);
                    break;

                case ItemType.Movie:
                    order.CollectionOfMovies.Remove((Movie)OrderListMovies.SelectedItem);
                    break;
            }
        }

        private void Save_Order(object sender, RoutedEventArgs e)
        {
            SetContext();
            PSDbContext.Orders.Add(new Order { Id = order.Id, OrderDate = order.OrderDate, OrderId = PSDbContext.Orders.Max(o => o.OrderId) + 1 });
            PSDbContext.SaveChanges();
            foreach (var book in order.CollectionOfBooks)
            {
                var BookToUpdate = PSDbContext.Books.First(b => b.Id == book.Id);
                BookToUpdate.StorageAmount--;
                PSDbContext.BookProductIds.Add(new BookProductId { Book = BookToUpdate, Order = PSDbContext.Orders.First(o => o.Id == order.Id) });
            }
            foreach (var game in order.CollectionOfGames)
            {
                var GameToUpdate = PSDbContext.Games.First(g => g.Id == game.Id);
                GameToUpdate.StorageAmount--;
                PSDbContext.GameProductIds.Add(new GameProductId { Game = GameToUpdate, Order = PSDbContext.Orders.First(o => o.Id == order.Id) });
            }
            foreach (var movie in order.CollectionOfMovies)
            {
                var MovieToUpdate = PSDbContext.Movies.First(m => m.Id == movie.Id);
                MovieToUpdate.StorageAmount--;
                PSDbContext.MovieProductIds.Add(new MovieProductId { Movie = MovieToUpdate, Order = PSDbContext.Orders.First(o => o.Id == order.Id) });
            }
            PSDbContext.SaveChanges();

            order = new Order();
            OrderListBooks.ItemsSource = order.CollectionOfBooks;
            OrderListMovies.ItemsSource = order.CollectionOfMovies;
            OrderListGames.ItemsSource = order.CollectionOfGames;

            MessageBox.Show($"You have added an order. Your order number is {PSDbContext.Orders.Max(o => o.OrderId)}");
        }
        #endregion

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            MainWindow manage = new MainWindow();
            this.Visibility = Visibility.Hidden;
            manage.Show();
        }
    }
}