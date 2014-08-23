using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainModel.Concrete;

namespace WebUI.Controllers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class AuthorizationAttribute : AuthorizeAttribute
    {

        public AuthorizationAttribute()
        {

        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            try
            {

                NavigationHelper repo = new NavigationHelper();
                string controller = httpContext.Request.Url.AbsolutePath.Split('/')[1];
                Int32 userId = Convert.ToInt32(httpContext.Request.Cookies["userDetails"]["userId"]);
                bool status = repo.getCategoryStatusForUser(userId, controller);
                if (!status)
                {
                    httpContext.Response.Redirect("/Errors/Index?error=Ooops! The page that you are trying to access is out of bounds. Please contact an administrator to allow you to view the page.");
                    return false;
                }
                else
                {
                    return base.AuthorizeCore(httpContext);
                }

            }
            catch (Exception)
            {
                httpContext.Response.Redirect("/Administration/LogOut");
                return false;
            }
        }

    }
}