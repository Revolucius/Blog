using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace blog1.Models
{
    public class DeletePosts
    {
        public void DeletePost(string title)
        {
            string postID = null;
            List<string> commentsID = new List<string>();
           // 
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["mssql"].ConnectionString))
            {
                connection.Open();
                
                using (var command = new SqlCommand("select Posts.PostId from Posts where Posts.Title = @Title group by Posts.PostId"))
                {
                    command.Connection = connection;
                    command.Parameters.Add(new SqlParameter("Title", title));
                    
                    using (var dataReader = command.ExecuteReader())
                    {
                        if (dataReader.Read())
                        {
                            postID = dataReader["PostID"].ToString();
                        }
                    }
                    connection.Close();
                }
                using (var sqlCommand = new SqlCommand(@"delete from Comments where PostID = @PostID"))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("PostID", postID));
                    sqlCommand.Connection = connection;
                    connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    connection.Close();
                }
                using (var sqlCommand2 = new SqlCommand(@"delete from Posts where PostID = @PostID"))
                {
                    sqlCommand2.Parameters.Add(new SqlParameter("PostID", postID));
                    sqlCommand2.Connection = connection;
                    connection.Open();
                    sqlCommand2.ExecuteNonQuery();
                }
            }
        }
    }
}