using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace blog1.Models
{
    public class RecentCommentItem
    {
        public RecentCommentItem(string title, DateTime date, string id)
        {
            this.text = title;
            this.date = date;
            this.commentid = id;
        }
        public string text { get; set; }
        public string url { get; set; }
        public string commentid { get; set; }
        public DateTime date { get; set; }
    }
}