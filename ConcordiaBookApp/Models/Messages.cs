using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConcordiaBookApp.Models
{
    public class Messages
    {
        [Key]
        public int MessageID { get; set; }

        public ICollection<MessageThread> MessagesInThread { get; set; }
        public string FromId { get; set; }
        public UserProfile User { get; set; }
    }
}