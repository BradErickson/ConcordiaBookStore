using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ConcordiaBookApp.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Version { get; set; }
        public int ISBN { get; set; }
        public int Quantity { get; set; }
        public string Genre { get; set; }
        public double SellingPrice { get; set; }
        public double RentingPrice { get; set; }
        public bool AvailableTrade { get; set; }
        public string PhotoUrl { get; set; }
        public virtual ICollection<Author> Authors { get; set; }
        public virtual ICollection<BookRental> Rentals { get; set; }
        public virtual string BookSellerId { get; set; }
    }
}