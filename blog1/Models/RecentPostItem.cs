using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace blog1.Models
{
    public class RecentPostItem
    {
        public RecentPostItem(string title, DateTime date)
        {
            this.text = title;
            this.url =  title.Replace(" ", "_");
            this.date = date;
        }
        public string text { get; set; }
             public string url {get; set;}
                  public DateTime date {get; set;}
    }
}