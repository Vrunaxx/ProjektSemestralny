using ProjektSemestralny.Classes;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace ProjektSemestralny
{
    /// <summary>
    /// Logika interakcji dla ManageMovies
    /// </summary>
    public partial class ManageMovies
    {
        private ProjektSemestralnyDbContext PSDbContextMovies;
        private ObservableCollection<Movie> ObsvMovies;

        /// <summary>
        /// StartUp dla okna zarządzania filmami
        /// Łączy z bazą danych 
        /// Oraz ustawia zawartość DropBoxa
        /// </summary>
        public ManageMovies()
        {
            this.InitializeComponent();
            SetContext();
            ObsvMovies = new ObservableCollection<Movie>(PSDbContextMovies.Movies);
            MovieList.ItemsSource = ObsvMovies;

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
            var FilterObj = obj as Movie;

            return FilterObj.Title.Contains(FilterTextBox.Text, StringComparison.OrdinalIgnoreCase);
        }

        private bool CategoryFilter(object obj)
        {
            var FilterObj = obj as Movie;

            return FilterObj.Category.Contains(FilterTextBox.Text, StringComparison.OrdinalIgnoreCase);
        }
        private bool AutorFilter(object obj)
        {
            var FilterObj = obj as Movie;

            return FilterObj.Autor.Contains(FilterTextBox.Text, StringComparison.OrdinalIgnoreCase);
        }

        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (FilterTextBox.Text == null)
            {
                MovieList.Items.Filter = null;
            }
            else
            {
                MovieList.Items.Filter = GetFilter();
            }
        }

        private void FilterBy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MovieList.Items.Filter = GetFilter();
        }

        #endregion

        private void SetContext()
        {
            PSDbContextMovies = new ProjektSemestralnyDbContext();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SetContext();

            if (MovieList.SelectedIndex != -1)
            {
                var MovieToUpdate = PSDbContextMovies.Movies.First(Movie => Movie.Id == ((Movie)MovieList.SelectedItem).Id);
                MovieToUpdate.Title = txtTitle.Text;
                MovieToUpdate.Autor = txtAutor.Text;
                MovieToUpdate.Category = txtCat.Text;
                MovieToUpdate.Price = decimal.Parse(txtPrice.Text);
                MovieToUpdate.DateOrRelease = DateTime.ParseExact(txtDoR.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                MovieToUpdate.StorageAmount = Convert.ToInt32(txtSA.Text);
            }
            else if (txtTitle.Text == "")
            {
                MessageBox.Show("Title field can not be empty");
            }
            else if (txtAutor.Text == "")
            {
                MessageBox.Show("Autor field can not be empty");
            }
            else if (txtCat.Text == "")
            {
                MessageBox.Show("Category field can not be empty");
            }
            else if (txtDoR.Text == "")
            {
                MessageBox.Show("Date of release field can not be empty");
            }
            else if (txtSA.Text == "")
            {
                MessageBox.Show("Storage Amount of release field can not be empty");
            }
            else
            {
                var Movie = new Movie
                {
                    Title = txtTitle.Text,
                    Autor = txtAutor.Text,
                    Category = txtCat.Text,
                    Price = decimal.Parse(txtPrice.Text),
                    DateOrRelease = DateTime.ParseExact(txtDoR.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture),
                    StorageAmount = Convert.ToInt32(txtSA.Text)
                };
                PSDbContextMovies.Movies.Add(Movie);
                ObsvMovies.Add(Movie);
                MessageBox.Show("You have added an item");
            }

            PSDbContextMovies.SaveChanges();
            
        }

        private void MovieList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MovieList.SelectedIndex >= 0)
            {
                var selectedItem = ObsvMovies.ElementAt(MovieList.SelectedIndex);
                txtTitle.Text = selectedItem.Title;
                txtAutor.Text = selectedItem.Autor;
                txtCat.Text = selectedItem.Category;
                txtPrice.Text = selectedItem.Price.ToString();
                txtDoR.Text = selectedItem.DateOrRelease.ToString();
                txtSA.Text = selectedItem.StorageAmount.ToString();
                Delete_Button.IsEnabled = true;
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
            if (MovieList.SelectedIndex < 0)
            {
                MessageBox.Show("You have to select an item");
            }
            else
            {
                var MovieToDelete = PSDbContextMovies.Movies.First(Movie => Movie.Id == ((Movie)MovieList.SelectedItem).Id);
                PSDbContextMovies.Movies.Remove(MovieToDelete);
                ObsvMovies.Remove(((Movie)MovieList.SelectedItem));
                PSDbContextMovies.SaveChanges();
                MessageBox.Show("You have deleted an item");
            }
        }

        private void Remove_Sort()
        {
            if (MovieList.Items.SortDescriptions.Any())
                MovieList.Items.SortDescriptions.Remove(MovieList.Items.SortDescriptions.First());
        }

        private void Button_Click_ASC(object sender, RoutedEventArgs e)
        {
            Remove_Sort();
            MovieList.Items.SortDescriptions.Add(new SortDescription("Title", ListSortDirection.Ascending));
        }

        private void Button_Click_DESC(object sender, RoutedEventArgs e)
        {
            Remove_Sort();
            MovieList.Items.SortDescriptions.Add(new SortDescription("Title", ListSortDirection.Descending));
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            MainWindow manage = new MainWindow();
            this.Visibility = Visibility.Hidden;
            manage.Show();
        }
        private void NumberValidationTextBox(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}