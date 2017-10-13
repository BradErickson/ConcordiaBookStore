using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConcordiaBookApp.Models
{
    public class BookRental
    {
            public int BookRentalId { get; set; } 
            public virtual Book RentedBook { get; set; }
            public virtual UserProfile owner { get; set; }
    }
}