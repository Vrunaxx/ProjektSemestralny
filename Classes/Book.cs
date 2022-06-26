using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProjektSemestralny.Classes
{
    public class Book
    {

        #region properties 

        [Key]
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Autor { get; set; }

        public string Category { get; set; }

        public decimal Price { get; set; }

        public DateTime DateOrRelease { get; set; }

        public int StorageAmount { get; set; }

        #endregion properties

        public Book()
        {
            Id = Guid.NewGuid();
        }

    }
}
