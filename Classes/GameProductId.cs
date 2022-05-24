using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProjektSemestralny.Classes
{
    public class GameProductId
    {

        #region properties 

        [Key]
        public Guid Id { get; set; }

        public Game Game { get; set; }

        public Order Order { get; set; }

        #endregion properties

    }
}
