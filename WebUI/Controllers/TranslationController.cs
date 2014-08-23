using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace WebUI.Controllers
{
    [Authorization]
    public class TranslationController : Controller
    {
        //
        // GET: /Translation/

        public ActionResult Comments()
        {
            return View();
        }

        public ActionResult Features()
        {
            return View();
        }

        public ActionResult Guides()
        {
            return View();
        }

        public ActionResult Sections()
        {
            return View();
        }

    }
}
