using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace blog1.Models
{
    public class RecentPost
    {
        public RecentPost(int? it)
        {
            items = new List<RecentPostItem>();
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["mssql"].ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM Posts WHERE PostID > 0 ORDER by Date DESC"))
                {
                    command.Connection = connection;
                    using(var reader = command.ExecuteReader())
                    {
                        if(it!=1)
                        {
                           for( int i=0;i<3;i++)
                           {
                               if(reader.Read())
                                   items.Add(new RecentPostItem(
                                       reader["title"].ToString(),
                                       DateTime.Parse(reader["date"].ToString())
                           ));
                           }
                        }
                        else
                        {
                            while(reader.Read())
                            {
                                items.Add(new RecentPostItem(
                                    reader["title"].ToString(),
                                   DateTime.Parse(reader["date"].ToString())
                                    ));
                            }
                        }
                    }
                }
            }
        }
        public List<RecentPostItem> items { get; set; }
    }
}