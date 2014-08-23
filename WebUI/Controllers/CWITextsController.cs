using System;
using System.IO;
using System.Web;
using System.Linq;
using System.Web.Mvc;
using DomainModel.jqGrid;
using DomainModel.Abstract;
using System.Web.Mvc.Ajax;
using System.Linq.Dynamic;
using System.Collections.Generic;
using System.Text;
using BusinessLogic;

namespace WebUI.Controllers
{
    [Authorization]
    public class CWITextsController : Controller
    {
        #region Private members
        private ICWITextRepository CWITextRepository;
        public int PageSize = 5;
        public int currentPage;
        #endregion

        #region Default constructor
        public CWITextsController(ICWITextRepository CWITextRepo)
        {
            CWITextRepository = CWITextRepo;
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

        #region Creates a new CWIText
        /// <summary>
        /// Author: Nipun Aand
        /// creates a new CWIText
        /// </summary>
        /// <returns>View Result</returns>
        public ViewResult Create()
        {
            ViewData["CWITextId"] = "";
            ViewData["CWIText"] = "";
            fillAllCWITextCreateDetails();
            return View("Edit");
        }
        #endregion

        #region Editing CWITexts
        /// <summary>
        /// Rendering the editing defalut view
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public ViewResult Edit(int Id)
        {
            var CWIText = CWITextRepository.getCWIText(Id).ToList();
            ViewData["CWITextId"] = CWIText[0].ID;
            ViewData["CWIText"] = CWIText[0].CWIText;
            fillAllCWITextEditDetails(CWIText[0].ID);

            return View();
        }

        /// <summary>
        /// Handling the postback for editing
        /// </summary>
        /// <param name="name">name of the CWIText to be edited</param>
        /// <param name="Id">id of the CWIText to be edited</param>
        /// <param name="bdLogo">existing logo path of the CWIText to be edited</param>
        /// <param name="logo">new logo which is required to be replace to the existing logo</param>
        /// <returns></returns>
        [HttpPost]
        public ViewResult Edit(int Id, string CwiText, List<Int32> brandChoosen)
        {
            if (Id == -1)
            {
                try
                {
                    if (!ValidateUserDetails(CwiText))
                    {
                        fillAllCWITextCreateDetails();
                        ViewData["CWITextId"] = Id;
                        ViewData["CWIText"] = CwiText;
                    }
                    
                    //user.CountryID = Convert.ToInt32(location);
                    //user.CountryID = location == "0" ? Convert.ToInt32("1") : Convert.ToInt32(location);
                    //user = UserRepository.createdUserData(user, brandChoosen, checkboxtree, cultureChoosen, regionChoosen);
                    //user.Contact = contact;

                    //CWITextRepository.createUser(user);
                    CWITextRepository.createdCWITextData(CwiText, brandChoosen);
                    return View("Index");
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
            else
            {

                if (!ValidateUserDetails(CwiText))
                {
                    fillAllCWITextEditDetails(Id);
                    ViewData["CWITextId"] = Id;
                    ViewData["CWIText"] = CwiText;
                }

                CWITextRepository.saveCWIText(Id, CwiText, brandChoosen);

                return View("Index");
            }
            #region
           
            #endregion

        }

        #endregion

        #region Deleting CWITexts
        /// <summary>
        /// Author:Nipun Anand
        /// Deletes an already existing CWIText
        /// </summary>
        /// <param name="Id">ID of the CWIText to be deleted</param>
        /// <returns>Redirects to the Index action</returns>
        public ViewResult delete(int Id)
        {
            int status;
            status = CWITextRepository.deleteCWIText(Id);
            if (status == -1)
            {
                ModelState.AddModelError("CWIText", "Cannot delete the CWIText.");
                //return RedirectToAction("Index", "CWITexts");
            }
            return View("Index");

        }
        #endregion

        #region Custom methods

        #region To format CWIText data in JSON format
        /// <summary>
        /// Author:Nipun Anand
        /// Returns the CWIText data in JSON format sutiable for the JqGrid to display
        /// </summary>
        /// <param name="page">page no of the records to display</param>
        /// <param name="rows">no of rows to display</param>
        /// <param name="search">search criteria or the string which is to be searched</param>
        /// <param name="sidx">id of the column based upon which sorting is to be done</param>
        /// <param name="sord">order of sorting</param>
        /// <returns></returns>
        public ActionResult getJQgridData(int page, int rows, string search, string sidx, string sord)
        {
            PageSize = rows;
            int pageNoInQueryString = Convert.ToInt32(Request.QueryString["page"]);
            var CWITexts = CWITextRepository.getCWITexts();
            IQueryable<CWITextDisplay> rowsNew = CWITextRepository.getCWITextsForDisplay(CWITexts);

            var jsonDataNew = rowsNew.OrderBy(sidx + " " + sord).ToJqGridData(pageNoInQueryString, rows, null, search, new[] { "Name" });
            return Json(jsonDataNew, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region renders the checkboxlist
        /// <summary>
        /// renders the checkboxlist
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="name"> name of the checkboxlist</param>
        /// <param name="listInfo">object with value of ID, checked status and InnerHtml </param>
        /// <param name="htmlAttributes">other style attributes </param>
        /// <returns>string containg checkboxlist tags </returns>
        public string CheckBoxList(string name, List<details> listInfo, IDictionary<string, object> htmlAttributes)
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

        #region fill Brands Drop Down
        /// <summary>
        /// fill Brands Drop Down
        /// </summary>
        /// <returns></returns>
        public string fillBrandsDropDown()
        {
            string checkBoxes = "";
            List<drpDetails> brandsForDpDwn = new List<drpDetails>();
            List<CwiBrandsForDp> brandsWithStatus = CWITextRepository.getBrandsStatusForDropDown();
            foreach (var brand in brandsWithStatus)
            {
                if (brand.Status == false)
                {
                    checkBoxes = checkBoxes + "<input type=\"checkbox\" name=\"brandsCheckboxes\" value=\"" + brand.ID + "\" />"; // + topMenu.Name + "</input>";
                }
                else
                {
                    checkBoxes = checkBoxes + "<input checked=\"checked\" type=\"checkbox\" name=\"brandsCheckboxes\" value=\"" + brand.ID + "\" />"; // + topMenu.Name + "</input>";
                }
                checkBoxes = checkBoxes + "<label>";
                checkBoxes = checkBoxes + brand.Brandname + "</label>";
                checkBoxes = checkBoxes + "<br>";
            }
            return checkBoxes;
        }
        #endregion

        #region filtering For Brand
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="search"></param>
        /// <param name="sidx"></param>
        /// <param name="sord"></param>
        /// <param name="brandId"></param>
        /// <returns></returns>
        public ActionResult filteringForBrand(string brandIds)
        {
            int page = 1;
            int rows = 10;
            string search = null;
            string sidx = "CWIText";
            string sord = "asc";
            PageSize = rows;
            int pageNoInQueryString = Convert.ToInt32(Request.QueryString["page"]);
            System.Web.Script.Serialization.JavaScriptSerializer sr = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Int32> lstBrandIds = (List<Int32>)(sr.Deserialize(brandIds, typeof(List<Int32>)));
            IQueryable<CWITextDisplay> rowsNew = CWITextRepository.getCWITextsForFilterDisplay(lstBrandIds);
            var jsonDataNew = rowsNew.OrderBy(sidx + " " + sord).ToJqGridData(pageNoInQueryString, rows, null, search, new[] { "Name" });
            return Json(jsonDataNew, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region fill All CWI Text Edit Details
        /// <summary>
        /// fill All CWI Text Edit Details
        /// </summary>
        /// <param name="cwiTextId"></param>
        private void fillAllCWITextEditDetails(Int32 cwiTextId)
        {
            var allDetails = CWITextRepository.getAllCwiTextEditDetails(cwiTextId);
            ViewData["brands"] = CheckBoxList("brand", allDetails.Where(detail => detail.Key == "brands").First().Value.ToList(), null);

            List<details> brands = allDetails.Where(detail => detail.Key == "brands").First().Value.ToList();
            
            #region drop down select list
            List<drpDetails> brandsForDpDwn = new List<drpDetails>();
            //var brands = allDetails.Where(detail => detail.Key == "brands").First().Value.ToList();
            foreach (var brand in brands)
            {
                int? id = brand.ID;
                brandsForDpDwn.Add(new drpDetails(brand.ID, brand.Name.ToString(), false));
            }
            brandsForDpDwn[0].Name = "Select";
            var items = new SelectList(brandsForDpDwn, "ID", "Name", 0);
            ViewData["brandsDPList"] = items;
            #endregion

        }
        #endregion

        #region fill All CWI Text Create Details
        private void fillAllCWITextCreateDetails()
        {
            var allDetails = CWITextRepository.getAllCwiTextCreateDetails();
            ViewData["brands"] = CheckBoxList("brand", allDetails.Where(detail => detail.Key == "brands").First().Value.ToList(), null);
        }
        #endregion

        #region Validate User details
        private bool ValidateUserDetails(string cwiText)
        {
            if (String.IsNullOrEmpty(cwiText))
            {
                ModelState.AddModelError("cwiText", "You must specify a CWI Text.");
            }
            return ModelState.IsValid;
        }
        #endregion

        #endregion
    }
}
