using ProjektSemestralny.Classes;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ProjektSemestralny
{
    /// <summary>
    /// Interaction logic for Menage.xaml
    /// </summary>
    public partial class ManageViewOrder
    {
        private ProjektSemestralnyDbContext PSDbContext;
        private ObservableCollection<Order> ObsvOrder;


        public ManageViewOrder()
        {
            this.InitializeComponent();
            SetContext();
            ObsvOrder = new ObservableCollection<Order>(PSDbContext.Orders); 
            StockList.ItemsSource = ObsvOrder;

            foreach (var order in ObsvOrder)
            {
                order.CollectionOfBooks = new ObservableCollection<Book>(PSDbContext.BookProductIds.Where(bpi => bpi.Order.Id == order.Id).Select(s => s.Book));
                order.CollectionOfGames = new ObservableCollection<Game>(PSDbContext.GameProductIds.Where(bpi => bpi.Order.Id == order.Id).Select(s => s.Game));
                order.CollectionOfMovies = new ObservableCollection<Movie>(PSDbContext.MovieProductIds.Where(bpi => bpi.Order.Id == order.Id).Select(s => s.Movie));
            }
            
        }
        private void SetContext()
        {
            PSDbContext = new ProjektSemestralnyDbContext();
        }

        #region Filters
        public Predicate<object> GetFilter()
        {
            var type = StockList.ItemsSource;

            return TitleFilterId;
        }

        private bool TitleFilterId(object obj)
        {
            var FilterObj = obj as Order;

            return FilterObj.OrderId.ToString().Contains(FilterTextBox.Text);
        }

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

        #endregion

        #region Sort
        private void Button_Click_ASC(object sender, RoutedEventArgs e)
        {
            Remove_Sort();
            StockList.Items.SortDescriptions.Add(new SortDescription("OrderId", ListSortDirection.Ascending));
        }

        private void Button_Click_DESC(object sender, RoutedEventArgs e)
        {
            Remove_Sort();
            StockList.Items.SortDescriptions.Add(new SortDescription("OrderId", ListSortDirection.Descending));
        }

        private void Remove_Sort()
        {
            if (StockList.Items.SortDescriptions.Any())
                StockList.Items.SortDescriptions.Remove(StockList.Items.SortDescriptions.First());
        }

        #endregion

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            MainWindow manage = new MainWindow();
            this.Visibility = Visibility.Hidden;
            manage.Show();
        }

        private void StockList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StockList.SelectedIndex != -1)
            {
                OrderListBooks.ItemsSource = ((Order)StockList.SelectedItem).CollectionOfBooks;
                OrderListMovies.ItemsSource = ((Order)StockList.SelectedItem).CollectionOfMovies;
                OrderListGames.ItemsSource = ((Order)StockList.SelectedItem).CollectionOfGames;
            }

        }
    }
}