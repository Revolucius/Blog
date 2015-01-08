using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace blog1.Models
{
    public class PostModel
    {
       public  string title, body;
       public  DateTime date;
      
       public PostModel(string title, string body, DateTime date)
        {
            this.title = title;
            this.body = body;
            this.date = date;
            
        }
    

    }
}