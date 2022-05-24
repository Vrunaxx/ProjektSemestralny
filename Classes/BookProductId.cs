using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProjektSemestralny.Classes
{
    public class BookProductId
    {

        #region properties 

        [Key]
        public Guid Id { get; set; }

        public Book Book { get; set; }

        public Order Order { get; set; }

        #endregion properties

    }
}
