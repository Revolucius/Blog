using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
namespace blog1.Models
{
 
    public class RecentComment
    {
        public RecentComment()
        {
        items = new List<RecentCommentItem>();
                using(var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["mssql"].ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM Comments WHERE PostID > 0 ORDER by Date DESC"))
                {
                    command.Connection = connection;
                     using (var reader = command.ExecuteReader())
                     {
                         for (int i = 0; i < 3; i++)
                         {
                             if (reader.Read())
                                 items.Add(new RecentCommentItem(
                                 reader["comment"].ToString(),
                                 DateTime.Parse(reader["Date"].ToString()),
                                 reader["CommentID"].ToString())
                                 );
                         }
                     }
                }
            }
        }
        public List<RecentCommentItem> items { get; set; }
    }
   
}