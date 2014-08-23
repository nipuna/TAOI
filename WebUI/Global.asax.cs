using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle;

namespace WebUI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                null,   // Don't bother giving this route entry a name 
                "",     // Matches the root URL, i.e. ~/ 
                new { controller = "Administration", action = "LogIn" } // Defaults 
              );

            routes.MapRoute(
                "adminIndex", // Don't bother giving this route entry a name         
                "{controller}", // URL pattern,                 
                new { action = "Index" } // Defaults 
            );

            routes.MapRoute(
                "admin", // Don't bother giving this route entry a name         
                "{controller}/{action}" // URL pattern,                 
            );

            routes.MapRoute(
                null, // Don't bother giving this route entry a name         
                "{controller}/Edit/{Id}", // URL pattern, e.g. ~/Page683                 
                new { controller = "Brands", action = "Edit", Id = "1" }, // Defaults 
                new { Id = @"\d+" } // Constraints: page must be numerical 
            );

            routes.MapRoute(null, "{controller}/{action}");

            Route myRoute = new Route("{controller}/{action}/{id}", new MvcRouteHandler())
            {
                Defaults = new RouteValueDictionary(new
                {
                    controller = "Home",
                    action = "Index",
                    id = ""
                })
            };
            routes.Add("Default", myRoute);

        }

        protected void Application_Start()
        {
            RegisterRoutes(RouteTable.Routes);
            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory());
            //ModelBinders.Binders.DefaultBinder = new Microsoft.Web.Mvc.DataAnnotations.DataAnnotationsModelBinder();
        }
    }
}