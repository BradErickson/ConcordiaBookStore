using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConcordiaBookApp.Models
{
    public class PostBook
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public double Version { get; set; }
        public int ISBN { get; set; }
        public string Genre { get; set; }
        public double SellingPrice { get; set; }
        public double RentingPrice { get; set; }
        public bool AvailableTrade { get; set; }
        public string AuthorName { get; set; }
    }
}