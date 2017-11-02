using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConcordiaBookApp.Models
{
    public class GetMessageDTO
    {
        public int MessageThreadID { get; set; }
        public string FromName { get; set; }
        public string SubjectLine { get; set;}
        public string MessageBody { get; set; }
    }
}