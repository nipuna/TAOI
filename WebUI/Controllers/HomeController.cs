using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace WebUI.Controllers
{
    [Authorization]
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Dashboard()
        {
            return View();
        }

    }
}
