using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ConcordiaBookApp.Models
{
    public class UserProfile
    {
        [Key, ForeignKey("User")]
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<BooksInStore> BooksInStore { get; set; }
        public virtual ICollection<BookRental> BookRentals{get; set;}
        public virtual ICollection<BooksSold> BooksSold { get; set; }
        public virtual ICollection<Messages> Messages { get; set; }

    }
}