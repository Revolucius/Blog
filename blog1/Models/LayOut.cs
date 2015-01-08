using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace blog1.Models
{
    public class LayOut
    {
        public LayOut()
        {
            RecentPost = new RecentPost();
            RecentComments = new RecentPost();
        }
        public RecentPost RecentPost { get; set; }
        public RecentPost RecentComments { get; set; }
    }
}