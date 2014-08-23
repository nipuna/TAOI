using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace WebUI.Controllers
{
    [Authorization]
    public class TestingController : Controller
    {
        //
        // GET: /Testing/

        public ActionResult Define()
        {
            return View();
        }

        public ActionResult Features()
        {
            return View();
        }

        public ActionResult Perform()
        {
            return View();
        }

    }
}
