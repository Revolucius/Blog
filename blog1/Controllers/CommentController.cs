using blog1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace blog1.Controllers
{
    public class CommentController : Controller
    {
        //
        // GET: /Comment/
       public ActionResult Recent()
       {
            var model = new RecentComment();
          return View(model);
      }

    }
}
