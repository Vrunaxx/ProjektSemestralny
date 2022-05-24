using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProjektSemestralny.Classes
{
    public class Order
    {
        #region properties 

        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }

        #endregion properties
    }
}
