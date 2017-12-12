using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConcordiaBookApp.Models
{
    public class UserRatings
    {
        [Key]
        public int UserRatingId { get; set; }
        public int Rating { get; set; }
    }
}