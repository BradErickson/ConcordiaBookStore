using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConcordiaBookApp.Models
{
    public class BookRental
    {
            [Key]
            public int BookId { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public double Version { get; set; }
            public int ISBN { get; set; }
            public string Genre { get; set; }
            public double RentingPrice { get; set; }
            public virtual ICollection<Author> Authors { get; set; }
            public virtual UserProfile owner { get; set; }
    }
}