﻿using ProjektSemestralny.Classes;
using ProjektSemestralny.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
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