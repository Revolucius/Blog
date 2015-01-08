using blog1.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace blog1.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
      public  string title;
        [HttpPost]
      public ActionResult Index(AddComment model)
        {
            var readers = new DataReader();

            title = model.title;
            if(model.username!=null && model.comments!=null)
            readers.AddComment(model.username, model.title, model.comments, DateTime.Now.ToString());
            ModelState.Clear();
            return View(readers.GetArticle(title));
         // return RedirectToAction("index","home");
        //  return View(new Article());
        }

        public ActionResult CommentLink(string title)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["mssql"].ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(@"select PostID from comments where Commentid = @id"))
                {
                    command.Connection = connection;
                    command.Parameters.Add(new SqlParameter("id", title));
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            title = reader["PostID"].ToString();
                        }
                    }
                }
                using (var command = new SqlCommand("select title from Posts where PostID = @id"))
                {
                    command.Connection = connection;
                    command.Parameters.Add(new SqlParameter("id", title));
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            title = reader["title"].ToString();
                        }
                    }
                }
            }
            var readers = new DataReader();
            return View(readers.GetArticle(title));
        }


        public ActionResult Admin(string Name, string Password)
        {
            if (Name == "admin" && Password == "007")
            {
                FormsAuthentication.SetAuthCookie("Admin", false);
                
                //FormsAuthentication.SetAuthCookie("Admin", false);
            }
            return View();
        }


        public ActionResult AdminExit()
        {
           
                FormsAuthentication.SetAuthCookie("neAdmin", true);

                //FormsAuthentication.SetAuthCookie("Admin", false);

                return RedirectToAction("Index", "Home");
        }


        public ActionResult AddPost(AddPost model)
        {
            if (model.title != null)
            {
                var readers = new DataReader();
                readers.AddPost(model.title, model.text, DateTime.Now.ToString());
                ModelState.Clear();
            }
            return View();
        }


        public ActionResult DeletePost(string title)
        {
            DeletePosts obj = new DeletePosts();
            obj.DeletePost(title.Replace("_", " "));
            return RedirectToAction("Index","Home");
        }

        public ActionResult DeleteComment(string commentid, string title)
        {
            DeleteComments obj = new DeleteComments();
            obj.DeleteComment(commentid);
            //return RedirectToAction("Index");
            var readers = new DataReader();
            return View(readers.GetArticle(title));
        }



         [HttpGet]
        public ActionResult Index(string titlenew)
        {
            if (titlenew == null)
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["mssql"].ConnectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("SELECT * FROM POSTS WHERE PostID > 0 ORDER by Date DESC"))
                    {
                        command.Connection = connection;
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                title = reader["Title"].ToString();
                            }
                        }
                    }
                }
            }
            else
                title = titlenew.Replace("_", " ");
            var readers = new DataReader();
             if(title!=null)
            return View(readers.GetArticle(title));
             else
                 return View("Empty");
        }

    }
}
