using ProjektSemestralny.Classes;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ProjektSemestralny
{
    /// <summary>
    /// Logika interakcji dla ManageGames
    /// </summary>
    public partial class ManageGames
    {
        private ProjektSemestralnyDbContext PSDbContextGames;
        private ObservableCollection<Game> ObsvGames;

        /// <summary>
        /// StartUp dla okna zarządzania grami
        /// Łączy z bazą danych 
        /// Oraz ustawia zawartość DropBoxa
        /// </summary>
        public ManageGames()
        {
            this.InitializeComponent();
            SetContext();
            ObsvGames = new ObservableCollection<Game>(PSDbContextGames.Games);
            GameList.ItemsSource = ObsvGames;

            FilterBy.ItemsSource = new string[] { "Title", "Category", "Autor" };
        }

        #region Filters
        /// <summary>
        /// Decyduje o tym wg. jakiego pola będzie filtrowany podgląd
        /// </summary>
        /// <returns>
        /// Zwraca filter
        /// Domyślnie filtruje po tytule
        /// </returns>
        public Predicate<object> GetFilter()
        {
            switch (FilterBy.SelectedItem as string)
            {
                case "Title":
                    return TitleFilter;

                case "Category":
                    return CategoryFilter;

                case "Autor":
                    return AutorFilter;
            }

            return TitleFilter;
        }

        private bool TitleFilter(object obj)
        {
            var FilterObj = obj as Game;

            return FilterObj.Title.Contains(FilterTextBox.Text, StringComparison.OrdinalIgnoreCase);
        }

        private bool CategoryFilter(object obj)
        {
            var FilterObj = obj as Game;

            return FilterObj.Category.Contains(FilterTextBox.Text, StringComparison.OrdinalIgnoreCase);
        }
        private bool AutorFilter(object obj)
        {
            var FilterObj = obj as Game;

            return FilterObj.Autor.Contains(FilterTextBox.Text, StringComparison.OrdinalIgnoreCase);
        }

        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (FilterTextBox.Text == null)
            {
                GameList.Items.Filter = null;
            }
            else
            {
                GameList.Items.Filter = GetFilter();
            }
        }

        private void FilterBy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GameList.Items.Filter = GetFilter();
        }

        #endregion

        private void SetContext()
        {
            PSDbContextGames = new ProjektSemestralnyDbContext();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SetContext();

            if (GameList.SelectedIndex != -1)
            {
                var gameToUpdate = PSDbContextGames.Games.First(game => game.Id == ((Game)GameList.SelectedItem).Id);
                gameToUpdate.Title = txtTitle.Text;
                gameToUpdate.Autor = txtAutor.Text;
                gameToUpdate.Category = txtCat.Text;
                gameToUpdate.Price = decimal.Parse(txtPrice.Text);
                gameToUpdate.DateOrRelease = DateTime.ParseExact(txtDoR.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                gameToUpdate.StorageAmount = Convert.ToInt32(txtSA.Text);
            }
            else
            {
                var game = new Game
                {
                    Title = txtTitle.Text,
                    Autor = txtAutor.Text,
                    Category = txtCat.Text,
                    Price = decimal.Parse(txtPrice.Text),
                    DateOrRelease = DateTime.ParseExact(txtDoR.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture),
                    StorageAmount = Convert.ToInt32(txtSA.Text)
                };
                PSDbContextGames.Games.Add(game);
                ObsvGames.Add(game);
            }

            PSDbContextGames.SaveChanges();
            MessageBox.Show("You have added an item");
        }

        private void GameList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GameList.SelectedIndex >= 0)
            {
                var selectedItem = ObsvGames.ElementAt(GameList.SelectedIndex);
                txtTitle.Text = selectedItem.Title;
                txtAutor.Text = selectedItem.Autor;
                txtCat.Text = selectedItem.Category;
                txtPrice.Text = selectedItem.Price.ToString();
                txtDoR.Text = selectedItem.DateOrRelease.ToString();
                txtSA.Text = selectedItem.StorageAmount.ToString();
            }
            else
            {
                txtTitle.Text = "";
                txtAutor.Text = "";
                txtCat.Text = "";
                txtPrice.Text = "";
                txtDoR.Text = "";
                txtSA.Text = "";
            }
        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            if (GameList.SelectedIndex < 0)
            {
                MessageBox.Show("You have to select an item");
            }
            else
            {
                var gameToDelete = PSDbContextGames.Games.First(game => game.Id == ((Game)GameList.SelectedItem).Id);
                PSDbContextGames.Games.Remove(gameToDelete);
                ObsvGames.Remove(((Game)GameList.SelectedItem));
                PSDbContextGames.SaveChanges();
                MessageBox.Show("You have deleted an item");
            }
        }

        private void Remove_Sort()
        {
            if (GameList.Items.SortDescriptions.Any())
                GameList.Items.SortDescriptions.Remove(GameList.Items.SortDescriptions.First());
        }

        private void Button_Click_ASC(object sender, RoutedEventArgs e)
        {
            Remove_Sort();
            GameList.Items.SortDescriptions.Add(new SortDescription("Title", ListSortDirection.Ascending));
        }

        private void Button_Click_DESC(object sender, RoutedEventArgs e)
        {
            Remove_Sort();
            GameList.Items.SortDescriptions.Add(new SortDescription("Title", ListSortDirection.Descending));
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            MainWindow manage = new MainWindow();
            this.Visibility = Visibility.Hidden;
            manage.Show();
        }
    }
}
