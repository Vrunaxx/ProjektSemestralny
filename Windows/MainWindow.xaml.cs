using System.Windows;

namespace ProjektSemestralny
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Book(object sender, RoutedEventArgs e)
        {
            ManageBooks manage = new ManageBooks();
            this.Visibility = Visibility.Hidden;
            manage.Show();
        }

        private void Button_Movies(object sender, RoutedEventArgs e)
        {
            ManageMovies manage = new ManageMovies();
            this.Visibility = Visibility.Hidden;
            manage.Show();
        }

        private void Button_Games(object sender, RoutedEventArgs e)
        {
            ManageGames manage = new ManageGames();
            this.Visibility = Visibility.Hidden;
            manage.Show();
        }

        private void Button_Orders(object sender, RoutedEventArgs e)
        {
            ManageOrder manage = new ManageOrder();
            this.Visibility = Visibility.Hidden;
            manage.Show();
        }

        private void Button_ViewOrders(object sender, RoutedEventArgs e)
        {
            ManageViewOrder manage = new ManageViewOrder();
            this.Visibility = Visibility.Hidden;
            manage.Show();
        }
    }
}
