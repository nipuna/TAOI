using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace WebUI.Controllers
{
    [Authorization]
    public class SystemsController : Controller
    {
        //
        // GET: /System/

        public ActionResult Index()
        {
            return View();
        }

    }
}
