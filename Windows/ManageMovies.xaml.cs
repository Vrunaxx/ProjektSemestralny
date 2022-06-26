using ProjektSemestralny.Classes;
using System;
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
    public partial class ManageMovies
    {
        private ProjektSemestralnyDbContext PSDbContextMovies;
        private ObservableCollection<Movie> ObsvMovies;

        public ManageMovies()
        {
            this.InitializeComponent();
            SetContext();
            ObsvMovies = new ObservableCollection<Movie>(PSDbContextMovies.Movies);
            MovieList.ItemsSource = ObsvMovies;

            FilterBy.ItemsSource = new string[] { "Title", "Category", "Autor" };
        }

        #region Filters
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
            }

            PSDbContextMovies.SaveChanges();
            MessageBox.Show("You have added an item");
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


    }
}