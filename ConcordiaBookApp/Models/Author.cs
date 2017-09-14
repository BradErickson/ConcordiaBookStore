using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConcordiaBookApp.Models
{
    public class Author
    {
        [Key]
        public int AuthorID { get; set; }
        [Display(Name = "Author Name")]
        public string Name { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}