using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace WebUI.Controllers
{
    [Authorization]
    public class GuidesController : Controller
    {
        //
        // GET: /QuickGuides/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Preview()
        {
            return View();
        }

        public ActionResult Sections()
        {
            return View();
        }

    }
}
