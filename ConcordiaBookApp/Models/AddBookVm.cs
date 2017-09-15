using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConcordiaBookApp.Models
{
    public class AddBookVm
    {
        public Book Book { get; set; }
        public Author Author { get; set; }
        public List<Author> AuthorsSelectList { get; set; }
    }
}