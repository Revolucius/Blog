using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace blog1.Models
{
    public class Comments
    {
        public string username;
        public string comments;
        public string id;
        public DateTime date;
        public Comments(string username, string comments, DateTime date, string id)
        {
           this.username = username;
           this.comments = comments;
            this.date = date;
            this.id = id;
        }
        public string title { get; set; }
        
    }
}