using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            
        }

        private void Button_Games(object sender, RoutedEventArgs e)
        {
            ManageGames manage = new ManageGames();
            this.Visibility = Visibility.Hidden;
            manage.Show();
        }

        private void Button_Orders(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
