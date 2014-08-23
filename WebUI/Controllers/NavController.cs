using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using DomainModel.Abstract;
using System.Web.Routing;
using System.Web.UI;

namespace WebUI.Controllers
{
    public class NavController : Controller
    {
        //
        // GET: /Nav/

        private IUICategoryRepository uiCategoryRepository;

        private INavigationHelper navHelper;

        public NavController(IUICategoryRepository uiCategoryRepository, INavigationHelper navHelper)
        {
            this.uiCategoryRepository = uiCategoryRepository;
            this.navHelper = navHelper;
        }

        //[OutputCache(Duration = 3600, VaryByParam = "none", Location = OutputCacheLocation.Client)]
        public ActionResult Navigation()
        {
            DomainModel.Concrete.SQLUserRepository repo = new DomainModel.Concrete.SQLUserRepository();
            Int32 userId = Convert.ToInt32(Request.Cookies["userDetails"]["userId"]); //Convert.ToInt32(Session["userId"])
            var userNav = repo.getUser(userId).Select(u => u.UICategories).ToList();
            List<DomainModel.Entities.UICategory> nav = uiCategoryRepository.UICategoriesForUser(userId);
            if (Session["CategoryID"] == null)
            {
                Session["CategoryID"] = 1;
            }
            Session["CategoryName"] = nav.Where(c => c.ID == Convert.ToInt32(Session["CategoryID"])).Single().Name;
            nav = navHelper.SetSelectedCategory((int)Session["CategoryID"], nav);
            #region menu HTML
            string menus = "";
            var toplevelItems = nav.Where(l => l.LevelInTree == 1).ToList();

            foreach (var topMenu in toplevelItems)
            {
                Int32 level = 1;
                //trees = trees + "<ul style=\"font-size:11px;\" class=\"unorderedlisttree\" id=\"" + topMenu.Name.Replace(" ", "") + "\" >";
                menus = menus + "<li>";
                menus = menus + "<a class=\"" + (topMenu.IsSelected == true ? "selected" : "") + "\" href=\"/Nav/Menu?uiCategory=" + topMenu.ID + "&uiController=" + topMenu.Controller + "&uiAction=" + topMenu.Action + "\" >";
                menus = menus + topMenu.Name;
                menus = menus + "</a>";
                level = ++level;
                var nextlevelItems = nav.Where(l => l.ParentID == topMenu.ID && l.LevelInTree == level).ToList();
                menus = menus + "<ul>";

                foreach (var sndMenu in nextlevelItems)
                {
                    menus = menus + "<li>";
                    menus = menus + "<a class=\"" + (sndMenu.IsSelected == true ? "selected" : "") + "\" href=\"/Nav/Menu?uiCategory=" + sndMenu.ID + "&uiController=" + sndMenu.Controller + "&uiAction=" + sndMenu.Action + "\" >";
                    //"<a class="" href="/Nav/Menu?uiCategory=9&amp;uiController=Testing&amp;uiAction=Define">
                    menus = menus + sndMenu.Name;
                    menus = menus + "</a>";
                    menus = menus + "</li>";
                }

                menus = menus + "</ul>";
                menus = menus + "</li>";

            }
            #endregion
            ViewData["menus"] = menus;
            return PartialView(nav);
        }

        public RedirectToRouteResult Menu(int uiCategory, string uiController, string uiAction)
        {
            Session["CategoryID"] = uiCategory;

            return RedirectToRoute("", new { controller = uiController, action = uiAction });
        }

        #region menus
        public string menus(List<userRightdetails> listInfo)
        {
            string trees = "";
            trees = trees + "<div style=\"width:100%;height: 160px; float: left;overflow: auto;\" >";
            var toplevelItems = listInfo.Where(l => l.LevelInTree == 1).ToList();

            foreach (var topMenu in toplevelItems)
            {
                //int highestLevel = listInfo.Where(l => l.LevelInTree > 1).Max().LevelInTree;
                Int32 level = 1;
                trees = trees + "<div style=\"width:200px;float:left;margin-left:3px;margin-right:3px;margin-top:10px;margin-bottom:10px;height:60px;\" >";
                //trees = trees + "<h2>Check Children and Parents</h2>";
                trees = trees + "<ul style=\"font-size:11px;\" class=\"unorderedlisttree\" id=\"" + topMenu.Name.Replace(" ", "") + "\" >";
                trees = trees + "<li style=\"font-size:12px;\" >";
                //if (topMenu.Status == true)
                //{
                //    trees = trees + "<input checked=\"checked\" type=\"checkbox\" name=\"checkboxtree\" value=\"" + topMenu.ID + "\" />"; // + topMenu.Name + "</input>";
                //}
                //else
                //{
                //    trees = trees + "<input type=\"checkbox\" name=\"checkboxtree\" value=\"" + topMenu.ID + "\" />"; //+ topMenu.Name + "</input>";
                //}
                //trees = trees + "<label>";
                trees = trees + topMenu.Name;
                // "</label>";

                level = ++level;
                var nextlevelItems = listInfo.Where(l => l.ParentID == topMenu.ID && l.LevelInTree == level).ToList();
                trees = trees + "<ul  style=\"font-size: 11px;display:inline;\" >";

                foreach (var sndMenu in nextlevelItems)
                {
                    trees = trees + "<li  style=\"font-size: 10px;display:inline;\" >";
                    //if (sndMenu.Status == true)
                    //{
                    //    trees = trees + "<input checked=\"checked\" type=\"checkbox\" name=\"checkboxtree\" value=\"" + sndMenu.ID + "\" />"; //+ sndMenu.Name + "</input>";
                    //}
                    //else
                    //{
                    //    trees = trees + "<input type=\"checkbox\" name=\"checkboxtree\" value=\"" + sndMenu.ID + "\" />"; //+ sndMenu.Name + "</input>";
                    //}
                    //trees = trees + "<label>";
                    trees = trees + sndMenu.Name; // +"</label>";
                    trees = trees + "</li>";
                }

                trees = trees + "</ul>";

                trees = trees + "</li>";
                trees = trees + "</ul>";
                trees = trees + "</div>";

            }
            trees = trees + "</div>";

            return trees;
        }

        #endregion
    }

}
