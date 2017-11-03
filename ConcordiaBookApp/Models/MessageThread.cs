using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConcordiaBookApp.Models
{
    public class MessageThread
    {
        [Key]
        public int MessageThreadId { get; set; }
        public string Title { get; set; }
        public string MessageBody { get; set; }
        public string SenderName { get; set; }
        public string SenderId { get; set; }
        public int MessageId { get; set; }
    }
}