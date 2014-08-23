using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using DomainModel.Abstract;
using System.Web.Security;

namespace WebUI.Controllers
{
    public class AdministrationController : Controller
    {
        
        #region Private members
        private IUserRepository UserRepository;
        #endregion

        #region Default constructor
        public AdministrationController(IUserRepository UserRepo)
        {
            UserRepository = UserRepo;
        }
        #endregion

        #region Log In
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult LogIn()
        {
            //ControllerContext.HttpContext.User.Identity.IsAuthenticated
            if (Request.RequestContext.HttpContext.User.Identity.IsAuthenticated)
            {
                Session["userSession"] = "onGoing";
                Session["loginTime"] = DateTime.Now.TimeOfDay.Hours + ":" + DateTime.Now.TimeOfDay.Minutes;
                return RedirectToAction("Dashboard", "Home");
            }
            else
            {
                Session["userId"] = "";
                Session["name"] = "";
                Session["loginTime"] = "";
                Session["userSession"] = "";
                Session["CategoryID"] = 1;
                Session["CategoryName"] = "Log In";
                return View();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult LogIn(string username, string password, string ReturnUrl, bool RememberMe)
        {
            if (!ValidateLogOn(username, password))
            {
                return View();
            } 
            Int32 userId;
            string nameOfUser = "";
            if (ControllerContext.HttpContext.User.Identity.IsAuthenticated)
            {
                Session["userSession"] = "onGoing";
                Session["loginTime"] = DateTime.Now.TimeOfDay.Hours + ":" + DateTime.Now.TimeOfDay.Minutes;
                return RedirectToAction("Dashboard", "Home");
            }
            if (UserRepository.authentucateUser(username, password, out userId, out nameOfUser))
            {
                Session["userId"] = userId;
                Session["name"] = nameOfUser;
                Session["userSession"] = "onGoing";
                Session["loginTime"] = DateTime.Now.TimeOfDay.Hours + ":" + DateTime.Now.TimeOfDay.Minutes;
                FormsAuthentication.SetAuthCookie(username, RememberMe);

                HttpCookie ck = new HttpCookie("userDetails");
                ck.Values["userId"] = userId.ToString();
                ck.Values["name"] = nameOfUser;
                ck.Values["userSession"] = "onGoing";
                ck.Values["loginTime"] = (DateTime.Now.TimeOfDay.Hours + ":" + DateTime.Now.TimeOfDay.Minutes).ToString();
                ck.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Add(ck);

                return RedirectToAction("Dashboard", "Home");
            }
            else
            {
                
                Session["userId"] = "";
                Session["name"] = "";
                Session["loginTime"] = "";
                Session["userSession"] = "";
                ModelState.AddModelError("form", "Login attempt failed");
                ViewData["lastLoginFailed"] = true;
                return View();
            }
        }
        #endregion

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session["userSession"] = "";
            Session["CategoryID"] = 1; 
            return RedirectToAction("LogIn");
        }

        #region Custom Methods
        
        /// <summary>
        /// peforms initial validation of the data put in the text boxes
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private bool ValidateLogOn(string userName, string password)
        {
            if (String.IsNullOrEmpty(userName))
            {
                ModelState.AddModelError("username", "You must specify a username.");
            }
            if (String.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("password", "You must specify a password.");
            }
            if (!String.IsNullOrEmpty(userName) && userName.Length > 50 )
            {
                ModelState.AddModelError("username", "Exceeds max character limit of 50");
            }

            return ModelState.IsValid;
        }

        #endregion

    }
}
