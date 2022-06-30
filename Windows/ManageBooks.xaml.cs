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
    /// Logika interakcji dla ManageBooks
    /// </summary>
    public partial class ManageBooks
    {
        private ProjektSemestralnyDbContext PSDbContextBooks;
        private ObservableCollection<Book> ObsvOrders;

        /// <summary>
        /// Startup Okna do zarządzania książkami
        /// Tworzy konener dla podglądu książek z bazy danych
        /// Oraz ustawia zawartość DropBoxa
        /// </summary>
        public ManageBooks()
        {
            this.InitializeComponent();
            SetContext();
            ObsvOrders = new ObservableCollection<Book>(PSDbContextBooks.Books);
            BookList.ItemsSource = ObsvOrders;
            
            FilterBy.ItemsSource = new string[] { "Title", "Category", "Autor"};
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
            var FilterObj = obj as Book;

            return FilterObj.Title.Contains(FilterTextBox.Text, StringComparison.OrdinalIgnoreCase);
        }

        private bool CategoryFilter(object obj)
        {
            var FilterObj = obj as Book;

            return FilterObj.Category.Contains(FilterTextBox.Text, StringComparison.OrdinalIgnoreCase);
        } 
        private bool AutorFilter(object obj)
        {
            var FilterObj = obj as Book;

            return FilterObj.Autor.Contains(FilterTextBox.Text, StringComparison.OrdinalIgnoreCase);
        }

        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (FilterTextBox.Text == null)
            {
                BookList.Items.Filter = null;
            }
            else
            {
                BookList.Items.Filter = GetFilter();
            }
        }

        private void FilterBy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BookList.Items.Filter = GetFilter();
        }

        #endregion

        private void SetContext()
        {
            PSDbContextBooks = new ProjektSemestralnyDbContext();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SetContext();

            if (BookList.SelectedIndex != -1)
            {
                var bookToUpdate = PSDbContextBooks.Books.First(book => book.Id == ((Book)BookList.SelectedItem).Id);
                bookToUpdate.Title = txtTitle.Text;
                bookToUpdate.Autor = txtAutor.Text;
                bookToUpdate.Category = txtCat.Text;
                bookToUpdate.Price = decimal.Parse(txtPrice.Text);
                bookToUpdate.DateOrRelease = DateTime.ParseExact(txtDoR.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                bookToUpdate.StorageAmount = Convert.ToInt32(txtSA.Text);

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
                var book = new Book
                {
                    Title = txtTitle.Text,
                    Autor = txtAutor.Text,
                    Category = txtCat.Text,
                    Price = decimal.Parse(txtPrice.Text),
                    DateOrRelease = DateTime.ParseExact(txtDoR.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture),
                    StorageAmount = Convert.ToInt32(txtSA.Text)
                };
                PSDbContextBooks.Books.Add(book);
                ObsvOrders.Add(book);
                MessageBox.Show("You have added an item");
            }

            PSDbContextBooks.SaveChanges();
            
        }

        private void BookList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BookList.SelectedIndex >= 0)
            {
                var selectedItem = ObsvOrders.ElementAt(BookList.SelectedIndex);
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
            if (BookList.SelectedIndex < 0)
            {
                MessageBox.Show("You have to select an item");
            }
            else
            {
                var bookToDelete = PSDbContextBooks.Books.First(book => book.Id == ((Book)BookList.SelectedItem).Id);
                PSDbContextBooks.Books.Remove(bookToDelete);
                ObsvOrders.Remove(((Book)BookList.SelectedItem));
                PSDbContextBooks.SaveChanges();
                MessageBox.Show("You have deleted an item");
            }
        }

        private void Remove_Sort()
        {
            if (BookList.Items.SortDescriptions.Any())
            BookList.Items.SortDescriptions.Remove(BookList.Items.SortDescriptions.First());
        }

        private void Button_Click_ASC(object sender, RoutedEventArgs e)
        {
            Remove_Sort();
            BookList.Items.SortDescriptions.Add(new SortDescription("Title", ListSortDirection.Ascending));
        }

        private void Button_Click_DESC(object sender, RoutedEventArgs e)
        {
            Remove_Sort();
            BookList.Items.SortDescriptions.Add(new SortDescription("Title", ListSortDirection.Descending));
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