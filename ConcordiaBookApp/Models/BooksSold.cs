using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConcordiaBookApp.Models
{
    public class BooksSold
    {
        [Key]
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Version { get; set; }
        public string ISBN { get; set; }
        public int Quantity { get; set; }
        public string Genre { get; set; }
        public double SellingPrice { get; set; }
        public double RentingPrice { get; set; }
        public bool AvailableTrade { get; set; }
        public string PhotoUrl { get; set; }
        public virtual ICollection<Author> Authors { get; set; }
        public virtual UserProfile user { get; set; }
    }
}