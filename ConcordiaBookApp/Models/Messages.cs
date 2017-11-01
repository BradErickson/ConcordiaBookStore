using System.ComponentModel.DataAnnotations;

namespace ConcordiaBookApp.Models
{
    public class Messages
    {
        [Key]
        public int MessageID { get; set; }
        public string Title { get; set; }
        public string MessageBody { get; set; }
        public string FromId { get; set; }
        public UserProfile User { get; set; }
    }
}