using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConcordiaBookApp.Models
{
    public class BooksInStore
    {
        [Key]
        public int BookInStoreId { get; set; }
        public virtual Book Book {get;set;}
        public virtual UserProfile user { get; set; }
    }
}