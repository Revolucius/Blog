using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace blog1.Models
{
    public class Article
    {

        public PostModel post;
        public List<Comments> comments;
        public Article(PostModel post, List<Comments> comments)
            {
                this.post = post;
                this.comments = comments;
            }
    }
}