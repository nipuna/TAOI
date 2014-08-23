using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    [Authorization]
    public class ReportsController : Controller
    {
        //
        // GET: /Report/

        public ActionResult IOCompatibility()
        {
            return View();
        }

        public ActionResult FeatureResults()
        {
            return View();
        }

        public ActionResult TestComments()
        {
            return View();
        }

        public ActionResult Translations()
        {
            return View();
        }

    }
}
