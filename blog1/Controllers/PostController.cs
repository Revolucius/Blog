using blog1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace blog1.Controllers
{
    public class PostController : Controller
    {
        //
        // GET: /Post/
        public ActionResult Recent(int? i)
        {
            var model = new RecentPost(i);
            return View(model);
        }

    }
}
