using System;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;
using DomainModel.jqGrid;
using System.Web.Mvc.Ajax;
using System.Linq.Dynamic;
using DomainModel.Abstract;
using System.Collections.Generic;
using BusinessLogic;
using System.Web.Script.Serialization;
using System.Text;
using System.Web.Routing;

namespace WebUI.Controllers
{
    [Authorization]
    public class UsersController : Controller
    {
        #region Private members
        private IUserRepository UserRepository;
        public int PageSize = 5;
        public int currentPage;
        #endregion

        #region Default constructor
        public UsersController(IUserRepository UserRepo)
        {
            UserRepository = UserRepo;
        }
        #endregion

        #region Renders the default view
        /// <summary>
        /// Author:Nipun 
        /// Renders the default view
        /// </summary>
        /// <returns>default view</returns>
        public ViewResult Index()
        {
            return View();
        }
        #endregion

        #region Creates a new User
        /// <summary>
        /// Author: Nipun Aand
        /// creates a new User
        /// </summary>
        /// <returns>View Result</returns>
        public ViewResult Create()
        {
            fillAllUserCreateDetails();
            return View("Edit");
        }
        #endregion

        #region Editing Users
        /// <summary>
        /// Rendering the editing defalut view
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public ViewResult Edit(int Id)
        {
            var User = UserRepository.getUser(Id).ToList().First();
            var UserA = UserRepository.getUser(Id);

            fillAllUserEditDetails(User);
            Session["redirectToEditPage"] = true;
            return View(User);

        }

        /// <summary>
        /// Handling the postback for editing
        /// </summary>
        /// <param name="name">name of the User to be edited</param>
        /// <param name="Id">id of the User to be edited</param>
        /// <param name="bdLogo">existing logo path of the User to be edited</param>
        /// <param name="logo">new logo which is required to be replace to the existing logo</param>
        /// <returns></returns>
        [HttpPost]
        public ViewResult Edit(int Id, DomainModel.Entities.User user, DomainModel.Entities.Contact contact, List<Int32> checkboxtree, List<Int32> brandChoosen, List<Int32> cultureChoosen, List<Int32> regionChoosen, string location)
        {
            if (Id == -1)
            {
                try
                {
                    if (!ValidateUserDetails(user.Username, user.Password))
                    {
                        user.Contact = contact;
                        fillAllUserCreateDetails();
                        return View(user);
                    }
                    if (!TryUpdateModel(user))
                    {
                        user.Contact = contact;
                        fillAllUserCreateDetails();
                        return View(user);
                    }
                    //user.CountryID = Convert.ToInt32(location);
                    user.CountryID = location == "0" ? Convert.ToInt32("1") : Convert.ToInt32(location);
                    user = UserRepository.createdUserData(user, brandChoosen, checkboxtree, cultureChoosen, regionChoosen);
                    user.Contact = contact;

                    UserRepository.createUser(user);
                    return View("Index");
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
            else
            {

                if (!TryUpdateModel(UserRepository.getUser(user.ID)))
                {
                    fillAllUserEditDetails(user);
                    return View(user);
                }
                user.CountryID = location == "0" ? Convert.ToInt32("1") : Convert.ToInt32(location);
                user = UserRepository.editedUserData(user, brandChoosen, checkboxtree, cultureChoosen, regionChoosen);
                user.Contact = contact;
                
                UserRepository.saveUser(user);

                return View("Index");
            }
            #region
            //#region Validations
            //if (name == "")
            //{
            //    ModelState.AddModelError("Name", "Name is required");
            //}
            //if (Id == -1 && logo == null)
            //{
            //    ModelState.AddModelError("logo", "Choose Logo");
            //}
            //if (Id != -1 && !ModelState.IsValid)
            //{
            //    var User = UserRepository.getUser(Id).ToList();
            //    //ViewData["Id"] = Id;
            //    //ViewData["UserId"] = User[0].ID;
            //    //ViewData["UserName"] = User[0].Name;
            //    //ViewData["UserLogo"] = User[0].Logo;
            //    return View();
            //}
            //else if (Id == -1 && !ModelState.IsValid)
            //{
            //    ViewData["UserId"] = "";
            //    return View();
            //}
            //#endregion
            //if (Id == -1)
            //{
            //    #region Creating New User
            //    try
            //    {
            //        string imgpath = "";
            //        string logoFileName = logo.FileName.Substring(logo.FileName.LastIndexOf("\\") + 1, logo.FileName.Length - logo.FileName.LastIndexOf("\\") - 1);
            //        imgpath = "/Content/Images/" + logoFileName;
            //        //System.IO.File.Exists(
            //        FileStream fs = new FileStream(Server.MapPath("~" + imgpath), FileMode.Create);
            //        BinaryWriter bw = new BinaryWriter(fs);
            //        Byte[] buffer = new Byte[logo.ContentLength];
            //        logo.InputStream.Read(buffer, 0, logo.ContentLength);
            //        bw.Write(buffer);
            //        bw.Close();
            //        fs.Close();
            //        UserRepository.createUser(name, imgpath);
            //        return View("Index");
            //    }
            //    catch (Exception)
            //    {
            //        //ViewData["UserId"] = ""; 
            //        return View("Index");
            //    }

            //    #endregion
            //}
            //else
            //{
            //    #region Editing New User
            //    try
            //    {
            //        string imgpath = "";

            //        if (logo != null)
            //        {
            //            imgpath = "/Content/Images/" + logo.FileName.Substring(logo.FileName.LastIndexOf("\\") + 1, logo.FileName.Length - logo.FileName.LastIndexOf("\\") - 1);
            //            //Deleting the old file
            //            System.IO.File.Delete(Server.MapPath("~" + bdLogo));
            //            //Creating the new file
            //            FileStream fs = new FileStream(Server.MapPath("~" + imgpath), FileMode.Create);
            //            BinaryWriter bw = new BinaryWriter(fs);
            //            Byte[] buffer = new Byte[logo.ContentLength];
            //            logo.InputStream.Read(buffer, 0, logo.ContentLength);
            //            bw.Write(buffer);
            //            bw.Close();
            //            fs.Close();
            //            UserRepository.saveUser(Id, name, imgpath);
            //        }
            //        else
            //        {
            //            UserRepository.saveUser(Id, name, bdLogo);
            //        }

            //return View(User);
            //    }
            //    catch (Exception e)
            //    {
            //        ModelState.AddModelError("exc", e.Message);
            //        return View();
            //    }
            //    #endregion
            //}
            #endregion

        }

        #endregion

        #region Deleting Users
        /// <summary>
        /// Author:Nipun Anand
        /// Deletes an already existing User
        /// </summary>
        /// <param name="Id">ID of the User to be deleted</param>
        /// <returns>Redirects to the Index action</returns>
        public RedirectToRouteResult delete(int Id)
        {
            UserRepository.deleteUser(Id);
            TempData["message"] = "";
            return RedirectToAction("Index", "Users", new { page = 1 });

        }
        #endregion

        #region Custom methods

        #region To format User data in JSON format
        /// <summary>
        /// Author:Nipun Anand
        /// Returns the User data in JSON format sutiable for the JqGrid to display
        /// </summary>
        /// <param name="page">page no of the records to display</param>
        /// <param name="rows">no of rows to display</param>
        /// <param name="search">search criteria or the string which is to be searched</param>
        /// <param name="sidx">index of the column based upon which sorting is to be done</param>
        /// <param name="sord">order of sorting</param>
        /// <returns></returns>
        public ActionResult getJQgridData(int page, int rows, string search, string sidx, string sord)
        {
            PageSize = rows;
            int pageNoInQueryString = 0;
            if (!Convert.ToBoolean(Session["redirectToEditPage"]))
            {
                pageNoInQueryString = Convert.ToInt32(Request.QueryString["page"]);
                Session["pageNo"] = Convert.ToInt32(Request.QueryString["page"]);
            }
            else if (Convert.ToBoolean(Session["redirectToEditPage"]))
            {
                pageNoInQueryString = Convert.ToInt32(Session["pageNo"]);
                Session["redirectToEditPage"] = false;
            }
            var Users = UserRepository.getUsers();
            IQueryable<userDisplay> rowsNew = UserRepository.getUsersForDisplay(Users);

            //var jsonDataNew = rowsNew.OrderBy(sidx + " " + sord).ToJqGridData(pageNoInQueryString, rows, null, search, new[] { "Active", "Name", "Username", "Email", "Phone", "Customer", "Location" });
            var jsonDataNew = rowsNew.OrderBy(sidx + " " + sord).ToJqGridData(pageNoInQueryString, rows, null, search, new[] { "Active", "Name", "Username", "Email", "Brand", "Location" });
            //JavaScriptSerializer sr = new JavaScriptSerializer();
            //string ab = sr.Serialize(jsonDataNew);
            //return ab;
            return Json(jsonDataNew, JsonRequestBehavior.AllowGet);


        }
        #endregion

        #region fill All User Edit Details
        /// <summary>
        /// to get all the details for a particular user when editing his preferences
        /// </summary>
        /// <param name="User"></param>
        private void fillAllUserEditDetails(DomainModel.Entities.User User)
        {
            var allDetails = UserRepository.getAllUserEditDetails(User);
            ViewData["brands"] = CheckBoxList("brand", allDetails.Where(detail => detail.Key == "brands").First().Value.ToList() , null);
            ViewData["cultures"] = CheckBoxList("culture", allDetails.Where(detail => detail.Key == "cultures").First().Value.ToList(), null);
            ViewData["userRights"] = checkBoxTree(UserRepository.getAllUserRightDetails(User));
            List<details> regions = allDetails.Where(detail => detail.Key == "regions").First().Value.ToList();
            ViewData["regions"] = CheckBoxList("region", regions, null); 
            #region drop down select list
            List<drpDetails> countriesForDpDwn = new List<drpDetails>();
            DomainModel.Concrete.SQLCountryRepository CountryRepository = new DomainModel.Concrete.SQLCountryRepository();
            var countries = CountryRepository.getCountries();
            foreach (var country in countries)
            {
                int? id = country.ID;
                countriesForDpDwn.Add(new drpDetails(country.ID, country.Name.ToString(), false));
            }
            countriesForDpDwn[0].Name = "Select";
            var items = new SelectList(countriesForDpDwn, "ID", "Name", User.CountryID);
            #endregion
            ViewData["location"] = items;
            ViewData["userId"] = User.ID;

        }
        #endregion

        #region fill All User Create Details
        /// <summary>
        /// to get all the details for creating a new user
        /// </summary>
        private void fillAllUserCreateDetails()
        {
            var allDetails = UserRepository.getAllUserCreateDetails();
            ViewData["brands"] = CheckBoxList("brand", allDetails.Where(detail => detail.Key == "brands").First().Value.ToList(), null);
            ViewData["cultures"] = CheckBoxList("culture", allDetails.Where(detail => detail.Key == "cultures").First().Value.ToList(), null);
            ViewData["userRights"] = checkBoxTree(UserRepository.getAllUserRightDetails(null));
            List<details> regions = allDetails.Where(detail => detail.Key == "regions").First().Value.ToList();
            ViewData["regions"] = CheckBoxList("region", regions, null); 
            regions[0].Name = "Select";
            #region drop down select list
            List<drpDetails> countriesForDpDwn = new List<drpDetails>();
            DomainModel.Concrete.SQLCountryRepository CountryRepository = new DomainModel.Concrete.SQLCountryRepository();
            var countries = CountryRepository.getCountries();
            foreach (var country in countries)
            {
                int? id = country.ID;
                countriesForDpDwn.Add(new drpDetails(country.ID, country.Name.ToString(), false));
            }
            countriesForDpDwn[0].Name = "Select";
            var items = new SelectList(countriesForDpDwn, "ID", "Name", 0);
            #endregion
            ViewData["location"] = items;
            ViewData["userId"] = "";
        }
        #endregion

        #region Validate User details 
        private bool ValidateUserDetails(string userName, string password)
        {
            if (String.IsNullOrEmpty(userName))
            {
                ModelState.AddModelError("username", "You must specify a username.");
            }
            if (String.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("password", "You must specify a password.");
            }
            return ModelState.IsValid;
        }
        #endregion

        #endregion

        #region
        /// <summary>
        /// renders the checkboxlist
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="name"> name of the checkboxlist</param>
        /// <param name="listInfo">object with value of ID, checked status and InnerHtml </param>
        /// <param name="htmlAttributes">other style attributes </param>
        /// <returns>string containg checkboxlist tags </returns>
        public string CheckBoxList( string name, List<details> listInfo, IDictionary<string, object> htmlAttributes)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentException("The argument must have a value", "name");
            if (listInfo == null)
                throw new ArgumentNullException("listInfo");
            if (listInfo.Count < 1)
                throw new ArgumentException("The list must contain at least one value", "listInfo");

            StringBuilder sb = new StringBuilder();
            //builder.MergeAttribute("value", info.Value);
            foreach (details info in listInfo)
            {
                string newCheckBox = "";
                if (info.Name == "All")
                {
                    //newCheckBox = "<input id=\"" + name + info.ID + "\" name=\"" + name + "All" + "\" value=\"" + info.ID + "\" onclick=\"checkAllCheckBoxes(this ,\"" + name + "\")\" type=\"checkbox\">" + info.Name + "</input>";
                    newCheckBox = "<input id=\"" + name + info.ID + "\" name=\"" + name + "All" + "\" value=\"" + info.ID + "\" onclick=\"checkAllCheckBoxes(this ," + name + ")\" type=\"checkbox\">" + info.Name + "</input>";
                }
                else
                {
                    if (info.Status == true)
                    {
                        newCheckBox = "<input checked=\"checked\" id=\"" + name + info.ID + "\" name=\"" + name + "Choosen" + "\" value=\"" + info.ID + "\" type=\"checkbox\">" + info.Name + "</input>";
                    }
                    else
                    {
                        newCheckBox = "<input id=\"" + name + info.ID + "\" name=\"" + name + "Choosen" + "\" value=\"" + info.ID + "\" type=\"checkbox\">" + info.Name + "</input>";
                    }
                }
                sb.Append("<span id=\"" + name + "Span" + info.ID + "\" class=\"grid_4\">");
                sb.Append(newCheckBox);
                sb.Append("</span>");
            }
            return sb.ToString();
        }
        #endregion

        #region checkBoxTree
        public string checkBoxTree(List<userRightdetails> listInfo)
        { 
            string trees = "";
            trees = trees + "<div style=\"width:100%;height: auto; float: left;overflow: auto;\" >";
            var toplevelItems = listInfo.Where(l => l.LevelInTree == 1).ToList();

            foreach (var topMenu in toplevelItems)
            {
                //int highestLevel = listInfo.Where(l => l.LevelInTree > 1).Max().LevelInTree;
                Int32 level = 1;
                trees = trees + "<div style=\"width:200px;float:left;margin-left:3px;margin-right:3px;margin-top:10px;margin-bottom:10px;height:60px;\" >";
                //trees = trees + "<h2>Check Children and Parents</h2>";
                trees = trees + "<ul style=\"font-size:11px;\" class=\"unorderedlisttree\" id=\"" + topMenu.Name.Replace(" ","") + "\" >";
                trees = trees + "<li style=\"font-size:12px;\" >";
                if (topMenu.Status == true)
                {
                    trees = trees + "<input checked=\"checked\" type=\"checkbox\" name=\"checkboxtree\" value=\"" + topMenu.ID + "\" />"; // + topMenu.Name + "</input>";
                }
                else
                {
                    trees = trees + "<input type=\"checkbox\" name=\"checkboxtree\" value=\"" + topMenu.ID + "\" />"; //+ topMenu.Name + "</input>";
                }
                trees = trees + "<label>";
                trees = trees + topMenu.Name + "</label>";

                level = ++level;
                var nextlevelItems = listInfo.Where(l => l.ParentID == topMenu.ID && l.LevelInTree == level ).ToList();
                trees = trees + "<ul  style=\"font-size: 11px;display:inline;\" >";

                foreach (var sndMenu in nextlevelItems)
                {
                    trees = trees + "<li  style=\"font-size: 10px;display:inline;\" >";
                    if (sndMenu.Status == true)
                    {
                        trees = trees + "<input checked=\"checked\" type=\"checkbox\" name=\"checkboxtree\" value=\"" + sndMenu.ID + "\" />"; //+ sndMenu.Name + "</input>";
                    }
                    else
                    {
                        trees = trees + "<input type=\"checkbox\" name=\"checkboxtree\" value=\"" + sndMenu.ID + "\" />"; //+ sndMenu.Name + "</input>";
                    }
                    trees = trees + "<label>";
                    trees = trees + sndMenu.Name + "</label>";
                    trees = trees + "</li>";
                }

                trees = trees + "</ul>";

                trees = trees + "</li>";
                trees = trees + "</ul>";
                trees = trees + "</div>";

            }
            trees = trees + "</div>";

                        
            #region
                //        <ul class="unorderedlisttree" id="checkchildren">
                //        <li>
                //            <input type="checkbox" name="checkboxtree_demo" value="6a7dc081-1b68-d347-3965-4a26ebdb707f">
                //            <label>
                //                Cars</label>
                //            <ul>
                //                <li>
                //                    <input type="checkbox" name="checkboxtree_demo" value="cc77bc95-6702-e7fe-7429-6e9643c73cc6">
                //                    <label>
                //                        Ford</label>
                //                </li>
                //            </ul>
                //        </li>
                //        <li>
                //            <input type="checkbox" name="checkboxtree_demo" value="b569502f-473b-890f-9fcf-c45b8a227baa">
                //            <label>
                //                Trucks</label>
                //        </li>
                //        <li>
                //            <input type="checkbox" name="checkboxtree_demo" value="2e54dafc-ddf5-40fc-5a7b-36e486e01dc0">
                //            <label>
                //                Airplanes</label>
                //        </li>
                //        <li>
                //            <input type="checkbox" name="checkboxtree_demo" value="69516965-caa2-e105-770f-453ad70d0254">
                //            <label>
                //                Animals</label>
                //            <ul>
                //                <li>
                //                    <input type="checkbox" name="checkboxtree_demo" value="ef16eb9a-d947-6987-857b-b5e38d3930a3">
                //                    <label>
                //                        Horses</label>
                //                </li>
                //                <li>
                //                    <input type="checkbox" name="checkboxtree_demo" value="f46655c3-06b2-bf5a-3879-d2136512e792">
                //                    <label>
                //                        Dogs</label>
                //                    <ul>
                //                        <li>
                //                            <input type="checkbox" name="checkboxtree_demo" value="afa5bef8-7cbf-a32e-9c3a-53e56e79ecb0">
                //                            <label>
                //                                Daschund</label>
                //                        </li>
                //                    </ul>
                //                </li>
                //                <li>
                //                    <input type="checkbox" name="checkboxtree_demo" value="14ecb3e6-47f0-3472-4f0e-2d004c51d6f0">
                //                    <label>
                //                        Cats</label>
                //                    <ul>
                //                        <li>
                //                            <input type="checkbox" name="checkboxtree_demo" value="d18f729e-aef2-d46a-f695-153e86ec8793">
                //                            <label>
                //                                Domestic Longhair</label>
                //                        </li>
                //                        <li>
                //                            <input type="checkbox" name="checkboxtree_demo" value="f86292fa-9618-68f6-7ebe-9a3dbc218970">
                //                            <label>
                //                                Norwegian Forest Car</label>
                //                            <ul>
                //                                <li>
                //                                    <input type="checkbox" name="checkboxtree_demo" value="d18f729e-aef2-d46a-f695-153e86ec8793">
                //                                    <label>
                //                                        White</label>
                //                                </li>
                //                                <li>
                //                                    <input type="checkbox" name="checkboxtree_demo" value="f86292fa-9618-68f6-7ebe-9a3dbc218970">
                //                                    <label>
                //                                        Brown</label>
                //                                </li>
                //                                <li>
                //                                    <input type="checkbox" name="checkboxtree_demo" value="d18f729e-aef2-d46a-f695-153e86ec8793">
                //                                    <label>
                //                                        Ginger</label>
                //                                </li>
                //                                <li>
                //                                    <input type="checkbox" name="checkboxtree_demo" value="f86292fa-9618-68f6-7ebe-9a3dbc218970">
                //                                    <label>
                //                                        Black</label>
                //                                </li>
                //                            </ul>
                //                        </li>
                //                    </ul>
                //                </li>
                //            </ul>
                //        </li>
                //    </ul>
                //    </div>
                        //</div>
            #endregion

            return trees;
        }

        #endregion

    }

}
