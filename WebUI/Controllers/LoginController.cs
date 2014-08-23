using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Security;

namespace WebUI.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult StatusDisplay()
        {
            try
            {

                if (ControllerContext.HttpContext.User.Identity.IsAuthenticated)
                {
                    ViewData["status"] = "true";
                    ViewData["linkText"] = "Log Out";
                    ViewData["actioName"] = "LogOut";
                    ViewData["controller"] = "Administration";
                    ViewData["name"] = Session["name"].ToString();
                    ViewData["logintime"] = "Logged in at " + Session["loginTime"].ToString();
                    ViewData["pendingActions"] = "";
                }
                else
                {
                    ViewData["status"] = "false";
                    ViewData["linkText"] = "Log In";
                    ViewData["actioName"] = "LogIn";
                    ViewData["controller"] = "Administration";
                    ViewData["name"] = "";
                    ViewData["logintime"] = "";
                    ViewData["pendingActions"] = "";
                }
                return View();
            }
            catch (Exception)
            {
               return View();
            }
        }

    }

}
