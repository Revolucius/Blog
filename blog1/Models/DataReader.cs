using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace blog1.Models
{
    public class DataReader
    {
        public Article GetArticle(string title)
        {
             PostModel post = null;
             List<Comments> comments = new List<Comments>();
           // var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["mssql"].ConnectionString)

             using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["mssql"].ConnectionString))
             {
                 connection.Open();
                 using (var command = new SqlCommand("SELECT * FROM Posts WHERE Title = @title"))
                 {
                     command.Connection = connection;
                     command.Parameters.Add(new SqlParameter("title", title));
                     using (var reader = command.ExecuteReader())
                     {
                         if (reader.Read())
                         {
                             post = new PostModel(
                                 reader["Title"].ToString(),
                                 reader["Text"].ToString(),
                                 DateTime.Parse(reader["date"].ToString())
                                 );
                         }
                     }
                 }
                 using (var command = new SqlCommand("SELECT  * FROM Comments INNER JOIN Posts ON Comments.PostID = Posts.PostID WHERE Posts.Title = @title"))
                 {
                     command.Connection = connection;
                     command.Parameters.Add(new SqlParameter("title", title));
                     using (var dataReader = command.ExecuteReader())
                     {
                         while (dataReader.Read())
                         {
                             comments.Add(new Comments(
                                 dataReader["Username"].ToString(),
                             dataReader["Comment"].ToString(),
                             DateTime.Parse(dataReader["Date"].ToString()),
                             dataReader["CommentID"].ToString()
                             ));
                         }
                     }
                 }
             }

             return new Article(post, comments);
        }

        public void AddPost(string title, string text, string date)
        {
            using (var sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["mssql"].ConnectionString))
            {
                using (var sqlCommand = new SqlCommand("INSERT INTO Posts VALUES(@title , @text , @date)"))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("Text", text));
                    sqlCommand.Parameters.Add(new SqlParameter("Date", date));
                    sqlCommand.Parameters.Add(new SqlParameter("Title", title));
                    sqlCommand.Connection = sqlConnection;
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }




        public void AddComment(string username, string title, string comment, string date)
        {
            using (var sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["mssql"].ConnectionString))
            {
                using (var sqlCommand = new SqlCommand("INSERT INTO Comments SELECT PostID, @username, @comment, @date AS MyPost FROM Posts WHERE Title = @title"))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("username", username));
                    sqlCommand.Parameters.Add(new SqlParameter("comment", comment));
                    sqlCommand.Parameters.Add(new SqlParameter("date", date));
                    sqlCommand.Parameters.Add(new SqlParameter("title", title));
                    sqlCommand.Connection = sqlConnection;
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }
    }
}