using ProjektSemestralny.Classes;
using ProjektSemestralny.Enums;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ProjektSemestralny
{
    /// <summary>
    /// Logika interakcji dla towrzenia zamówień
    /// </summary>
    public partial class ManageOrder
    {
        private ProjektSemestralnyDbContext PSDbContext;
        private ObservableCollection<Book> ObsvBooks;
        private ObservableCollection<Movie> ObsvMovies;
        private ObservableCollection<Game> ObsvGames;
        private Order order;
        private ItemType itemType;

        /// <summary>
        /// Starup dla zarządzania zamówieniami
        /// Łączy książki, filmy i gry z podglądem
        /// Inicjalizuje typ jako domyslny książki
        /// Oraz ustawia zawartość DropBoxa
        /// </summary>
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
        /// <summary>
        /// Decyduje jaki filter zastosować na podstawie załadowane typu do podglądu oraz wybranego filtra
        /// </summary>
        /// <returns></returns>
        public Predicate<object> GetFilter()
        {

            switch (itemType)
            {
                case ItemType.Book:
                    switch (FilterBy.SelectedItem as string)
                    {
                        case "Title":
                            return TitleFilterBook;

                        case "Category":
                            return CategoryFilterBook;

                        case "Autor":
                            return AutorFilterBook;
                    }
                    return TitleFilterBook;

                case ItemType.Movie:
                    switch (FilterBy.SelectedItem as string)
                    {
                        case "Title":
                            return TitleFilterMovie;

                        case "Category":
                            return CategoryFilterMovie;

                        case "Autor":
                            return AutorFilterMovie;
                    }
                    return TitleFilterMovie;

                case ItemType.Game:
                    switch (FilterBy.SelectedItem as string)
                    {
                        case "Title":
                            return TitleFilterGame;

                        case "Category":
                            return CategoryFilterGame;

                        case "Autor":
                            return AutorFilterGame;
                    }
                    return TitleFilterGame;
            }

            return TitleFilterGame;
        }
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
        #region Game
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
        #region Movie
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
            if (FilterTextBox.Text == null)
            {
                StockList.Items.Filter = null;
            }
            else
            {
                StockList.Items.Filter = GetFilter();
            }
        }

        private void FilterBy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StockList.Items.Filter = GetFilter();
        }

        private void RemoveFilter()
        {
            FilterTextBox.Text = "";
            StockList.Items.Filter = null;
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
            RemoveFilter();
            StockList.ItemsSource = ObsvBooks;
            itemType = ItemType.Book;
        }

        private void Movie_Load(object sender, RoutedEventArgs e)
        {
            RemoveFilter();
            StockList.ItemsSource = ObsvMovies;
            itemType = ItemType.Movie;
        }

        private void Game_Load(object sender, RoutedEventArgs e)
        {
            RemoveFilter();
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
                    if (((Book)StockList.SelectedItem).StorageAmount > 0)
                    {
                        order.CollectionOfBooks.Add((Book)StockList.SelectedItem);
                        ObsvBooks.First(b => b.Id == ((Book)StockList.SelectedItem).Id).StorageAmount--;
                    }
                    else
                    {
                        MessageBox.Show($"{((Book)StockList.SelectedItem).Title} Out of Stock ");
                    }

                    break;

                case ItemType.Game:
                    if (((Game)StockList.SelectedItem).StorageAmount > 0)
                    {
                        order.CollectionOfGames.Add((Game)StockList.SelectedItem);
                        ObsvGames.First(b => b.Id == ((Game)StockList.SelectedItem).Id).StorageAmount--;
                    }
                    else
                    {
                        MessageBox.Show($"{((Game)StockList.SelectedItem).Title} Out of Stock ");
                    }

                    break;

                case ItemType.Movie:
                    if (((Movie)StockList.SelectedItem).StorageAmount > 0)
                    {
                        order.CollectionOfMovies.Add((Movie)StockList.SelectedItem);
                        ObsvMovies.First(b => b.Id == ((Movie)StockList.SelectedItem).Id).StorageAmount--;
                    }
                    else
                    {
                        MessageBox.Show($"{((Movie)StockList.SelectedItem).Title} Out of Stock ");
                    }

                    break;
            }
        }

        private void Remove_Order(object sender, RoutedEventArgs e)
        {
            switch (itemType)
            {
                case ItemType.Book:
                    ObsvBooks.First(b => b.Id == ((Book)StockList.SelectedItem).Id).StorageAmount++;
                    order.CollectionOfBooks.Remove((Book)OrderListBooks.SelectedItem);
                    break;

                case ItemType.Game:
                    if (OrderListGames.SelectedIndex > 0)
                    {
                        Remove_Button.IsEnabled = true;
                    }
                    ObsvGames.First(b => b.Id == ((Game)StockList.SelectedItem).Id).StorageAmount++;
                    order.CollectionOfGames.Remove((Game)OrderListGames.SelectedItem);
                    break;

                case ItemType.Movie:
                    if (OrderListMovies.SelectedIndex > 0)
                    {
                        Remove_Button.IsEnabled = true;
                    }
                    ObsvMovies.First(b => b.Id == ((Movie)StockList.SelectedItem).Id).StorageAmount++;
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
        #region Selection changed
        private void StockList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StockList.SelectedIndex > -1)
            {
                Add_Button.IsEnabled = true;
            }
        }

        private void OrderListBooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OrderListBooks.SelectedIndex > -1)
            {
                Remove_Button.IsEnabled = true;
            }
        }

        private void OrderListMovies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OrderListMovies.SelectedIndex > -1)
            {
                Remove_Button.IsEnabled = true;
            }
        }

        private void OrderListGames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OrderListGames.SelectedIndex > -1)
            {
                Remove_Button.IsEnabled = true;
            }
        }
        #endregion
    }
}