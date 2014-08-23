using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class ErrorsController : Controller
    {
        //
        // GET: /Errors/

        public ActionResult Index(string error)
        {
            ViewData["errMsg"] = error;
            return View();
        }

    }
}
