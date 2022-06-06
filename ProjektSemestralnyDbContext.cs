using ProjektSemestralny.Classes;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace ProjektSemestralny
{
    [DbConfigurationType(typeof(ContextConfiguration))]
    public class ProjektSemestralnyDbContext : DbContext
    {
        public virtual IDbSet<Order> Orders { get; set; }

        public virtual IDbSet<MovieProductId> MovieProductIds{ get; set; }

        public virtual IDbSet<GameProductId> GameProductIds { get; set; }

        public virtual IDbSet<BookProductId> BookProductIds { get; set; }

        public virtual IDbSet<Movie> Movies { get; set; }

        public virtual IDbSet<Game> Games { get; set; }

        public virtual IDbSet<Book> Books { get; set; }

}
