using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainModel.jqGrid;
using DomainModel.Abstract;
using System.Web.Mvc.Ajax;
using System.Linq.Dynamic;
using BusinessLogic;
using System.Web.Script.Serialization;

namespace WebUI.Controllers
{
    public class CWIRangesController : Controller
    {

        #region Private members
        private ICWIRangesRepository CwiRangesRepository;
        private JavaScriptSerializer sr = new JavaScriptSerializer();
        public int PageSize = 5;
        public int currentPage;
        #endregion

        #region Default constructor
        public CWIRangesController(ICWIRangesRepository cwiRangesRepo)
        {
            CwiRangesRepository = cwiRangesRepo;

        }
        #endregion

        #region Renders the default view
        /// <summary>
        /// Author:Nipun 
        /// Renders the default view
        /// </summary>
        /// <returns>default view</returns>
        [HttpGet]
        public ActionResult Index()
        {
            fillCWIRangesDetails();
            return View();
        }
        #endregion

        #region Saves the changes done using the default view
        /// <summary>
        /// Author:Nipun 
        /// Saves the changes done using the default view
        /// </summary>
        /// <returns>default view</returns>
        [HttpPost]
        public ActionResult Index(Int32 Device, Int32 Product, rangesDetail[] rangesChoosen , List<string> IsLatest)
        {
            Int32 status = CwiRangesRepository.editCWIRange(Device, Product, rangesChoosen);
            fillCWIRangesDetails();
            return View();
        }
        #endregion

        #region Custom methods
        public void fillCWIRangesDetails()
        {

            #region drop down select list
            var allBrands = CwiRangesRepository.getBrandsForALLDevices();
            List<drpDetails> brandsForDpDwn = new List<drpDetails>();
            brandsForDpDwn.Add(new drpDetails(0, "Select", false));
            foreach (var brand in allBrands)
            {
                int? id = brand.ID;
                brandsForDpDwn.Add(new drpDetails(brand.ID, brand.Name.ToString(), false));
            }
            var brandItems = new SelectList(brandsForDpDwn, "ID", "Name", 1);

            List<drpDetails> selectForDpDwn = new List<drpDetails>();
            selectForDpDwn.Add(new drpDetails(0, "Select Brand", false));
            var selectItem = new SelectList(selectForDpDwn, "ID", "Name", 0);

            #endregion

            #region drop down select list
            var allBrandsForProducts = CwiRangesRepository.getBrandsForProducts(0);
            List<drpDetails> brandsForProductsDpDwn = new List<drpDetails>();
            brandsForProductsDpDwn.Add(new drpDetails(0, "Select", false));
            foreach (var brand in allBrandsForProducts)
            {
                int? id = brand.ID;
                brandsForProductsDpDwn.Add(new drpDetails(brand.ID, brand.Name.ToString(), false));
            }

            var brandsForProductsItems = new SelectList(brandsForProductsDpDwn, "ID", "Name", 0);

            #endregion

            ViewData["VehicleBrand"] = brandItems;
            ViewData["VehicleBrandForProducts"] = brandsForProductsItems;
            ViewData["VehicleDevice"] = selectItem;
            ViewData["VehicleProduct"] = selectItem;

        }

        #region getBrandsForProducts
        /// <summary>
        /// get Brands For which Products exist 
        /// </summary>
        /// <param name="brandId">id of the brand</param>
        /// <returns>IQuearble list of devices</returns>
        public string getBrandsForProducts(Int32 brandId)
        {

            var allBrandsForProducts = CwiRangesRepository.getBrandsForProducts(brandId).ToList();

            string sDevices = sr.Serialize(allBrandsForProducts);

            return sDevices;
            //List<drpDetails> brandsForProductsDpDwn = new List<drpDetails>();
            //brandsForProductsDpDwn.Add(new drpDetails(0, "Select", false));
            //foreach (var brand in allBrandsForProducts)
            //{
            //    int? id = brand.ID;
            //    brandsForProductsDpDwn.Add(new drpDetails(brand.ID, brand.Name.ToString(), false));
            //}

            //var brandsForProductsItems = new SelectList(brandsForProductsDpDwn, "ID", "Name", 0);

            //List<drpDetails> selectForDpDwnProduct = new List<drpDetails>();
            //selectForDpDwnProduct.Add(new drpDetails(0, "Select Brand", false));
            //var selectItemProduct = new SelectList(selectForDpDwnProduct, "ID", "Name", 0);
            //#endregion

            //ViewData["VehicleBrandForProducts"] = brandsForProductsItems;
            //ViewData["VehicleProduct"] = selectItemProduct;
        }
        #endregion
        #region Returns Devices for Brand in JSON format
        /// <summary>
        /// get Devices For Brand
        /// </summary>
        /// <param name="brandId"></param>
        /// <returns></returns>
        public string getDevicesForbrand(int brandId)
        {
            var deivces = CwiRangesRepository.getDevicesForbrand(brandId).ToList();

            string sDevices = sr.Serialize(deivces);

            return sDevices;
        }

        #endregion

        #region Returns details for Devices in JSON format
        /// <summary>
        /// get Devices For Brand
        /// </summary>
        /// <param name="brandId"></param>
        /// <returns></returns>
        public string getDetailsForDevice(int deviceId)
        {
            var detail = CwiRangesRepository.getDetailsForDevice(deviceId).ToList();

            string sDevices = sr.Serialize(detail);

            return sDevices;
        }

        #endregion

        #region Returns Devices data in JSON format
        /// <summary>
        /// get Products For Brand
        /// </summary>
        /// <param name="brandId"></param>
        /// <returns></returns>
        public string getProductForBrand(Int32 BrandId)
        {
            DomainModel.Concrete.SQLVehicleRepository vehRepo = new DomainModel.Concrete.SQLVehicleRepository();
            var vehicles = vehRepo.getVehicleForBrand(BrandId).ToList();
            
            string sVehicles = sr.Serialize(vehicles);

            return sVehicles;
        }
        #endregion

        #region Returns CWI Ranges in JSON format
        /// <summary>
        /// get Products For Brand
        /// </summary>
        /// <param name="brandId"></param>
        /// <returns></returns>
        public string getRangesForDeviceSelected(Int32 deviceId, Int32 productId)
        {

            var cwiRanges = CwiRangesRepository.getRangesForDeviceTested(deviceId, productId);
            string rangesHtml = "";
            string script = "<script type=\"text/javascript\" language=\"javascript\" >";
            Int32 i = 1;
            #region adding header 
            rangesHtml = rangesHtml + "<div style=\"margin-left:0px;width:610px;\" >";
            rangesHtml = rangesHtml + "<div style=\"width:160px;text-align:center;font-weight:bold;margin:5px;float:left;\" > Type</div>";
            rangesHtml = rangesHtml + "<div style=\"width:100px;text-align:center;font-weight:bold;margin:5px;float:left;\" > From </div>";
            rangesHtml = rangesHtml + "<div style=\"width:100px;text-align:center;font-weight:bold;float:left;margin:5px;\" > To </div>";
            rangesHtml = rangesHtml + "<div style=\"width:100px;text-align:center;font-weight:bold;float:left;margin:5px;\" > Latest </div>";
            rangesHtml = rangesHtml + "<div style=\"width:100px;text-align:center;font-weight:bold;float:left;margin:5px;\" > Actions </div>";
            rangesHtml = rangesHtml + "</div>";
            #endregion
            foreach (var range in cwiRanges)
            {
                rangesHtml = rangesHtml + "<div id=\"mainRangeDiv" + i + "\" style=\"margin-left:0px;width:610px;\" >";
                rangesHtml = rangesHtml + "<div id=\"rangeType" + i + "\" style=\"width:160px;text-align:center;font-weight:bold;margin:5px;float:left;\" > ";
                //rangesHtml = rangesHtml + "<select id=\"ddRangeType1\" selectedIndex=\"" + range.RegularExpression == null ? "1" : "2" + "\" onchange=\"renderRangeSelector(this.selectedIndex)\" >";
                rangesHtml = rangesHtml + "<select id=\"ddRangeType" + i + "\" onchange=\"renderRangeSelector(" + i + ")\" >";
                rangesHtml = rangesHtml + "<option value='0' >Select</option>";
                rangesHtml = rangesHtml + "<option value='Range' >Range</option>";
                rangesHtml = rangesHtml + "<option value='Expression' >Regular Expression</option>";
                rangesHtml = rangesHtml + "</select>";
                rangesHtml = rangesHtml + "</div>";
                #region adding script
                if (range.RegularExpression == null)
                {
                    script = script + "($(\"#ddRangeType" + i + "\").get()[0]).selectedIndex = 1; ";
                    rangesHtml = rangesHtml + "<div id=\"rangeInserter" + i + "\" style=\"width:220px;font-weight:bold;float:left;\" >";
                    rangesHtml = rangesHtml + "<div id=\"rangeFrom" + i + "\" style=\"width:100px;text-align:center;font-weight:bold;margin:5px;float:left;\" > ";
                    rangesHtml = rangesHtml + "<input type=\"text\" class=\"required\" title=\"*\" maxlength=\"1000\" name=\"rangesChoosen[" + (i - 1) + "].RangeStart\" style=\"width:100px;\" value=\"" + range.RangeStart + "\" />";
                    rangesHtml = rangesHtml + "</div>";
                    rangesHtml = rangesHtml + "<div id=\"rangeTo" + i + "\" style=\"width:100px;text-align:center;font-weight:bold;float:left;margin:5px;\" > ";
                    rangesHtml = rangesHtml + "<input type=\"text\" class=\"required\" title=\"*\" maxlength=\"1000\" name=\"rangesChoosen[" + (i - 1) + "].RangeEnd\" style=\"width:100px;\" value=\"" + range.RangeEnd + "\" />";
                    rangesHtml = rangesHtml + "</div>";
                    rangesHtml = rangesHtml + "</div>";
                }
                else
                {
                    script = script + "($(\"#ddRangeType" + i + "\").get()[0]).selectedIndex = 2; ";
                    rangesHtml = rangesHtml + "<div id=\"rangeInserter" + i + "\" style=\"width:220px;font-weight:bold;float:left;\" >";
                    rangesHtml = rangesHtml + "<div id=\"RegularExpression" + i + "\" style=\"width:200px;text-align:center;font-weight:bold;margin:5px;float:left;\" > ";
                    rangesHtml = rangesHtml + "<input type=\"text\" class=\"required\" title=\"*\" maxlength=\"1000\" name=\"rangesChoosen[" + (i - 1) + "].RegularExpression\" style=\"width:200px;\" value=\"" + range.RegularExpression + "\" />";
                    rangesHtml = rangesHtml + "</div>";
                    rangesHtml = rangesHtml + "</div>";
                }
                #endregion
                rangesHtml = rangesHtml + "<div id=\"rangeLatest" + i + "\" style=\"width:100px;text-align:center;font-weight:bold;float:left;margin:5px;\" > ";
                if (range.IsLatest == true)
                {
                    rangesHtml = rangesHtml + "<input onclick=\"checkUncheckRadioButtons(this)\" type=\"radio\" value=\"true\" name=\"rangesChoosen[" + (i - 1) + "].IsLatest\" checked=\"true\" />";
                }
                else
                {
                    rangesHtml = rangesHtml + "<input onclick=\"checkUncheckRadioButtons(this)\" type=\"radio\" value=\"false\" name=\"rangesChoosen[" + (i - 1) + "].IsLatest\" />";
                }
                rangesHtml = rangesHtml + "</div>";
                rangesHtml = rangesHtml + "<div id=\"rangeActions" + i + "\" style=\"width:100px;text-align:center;font-weight:bold;float:left;margin:5px;\" > ";
                rangesHtml = rangesHtml + "<img src=\"../../Content/Images/Admin/delete.gif\" onclick=\"removeRangeDiv(" + i + ")\" />";
                rangesHtml = rangesHtml + "</div>";
                rangesHtml = rangesHtml + "</div>";
                i = i + 1;
            }
            rangesHtml = rangesHtml + "<div  id=\"createRangeDiv\" style=\"width:100px;float:right;margin-top:5px;\" >";
            rangesHtml = rangesHtml + "<img src=\"../../Content/Images/Admin/createB.gif\" onclick=\"createRangeDiv()\" />";
            rangesHtml = rangesHtml + "</div>";
            script = script + "</script>";
            return rangesHtml + script;
        }
        #endregion

        #region To format Brand data in JSON format
        /// <summary>
        /// Author:Nipun Anand
        /// Returns the Brand data in JSON format sutiable for the JqGrid to display
        /// </summary>
        /// <param name="page">page no of the records to display</param>
        /// <param name="rows">no of rows to display</param>
        /// <param name="search">search criteria or the string which is to be searched</param>
        /// <param name="sidx">id of the column based upon which sorting is to be done</param>
        /// <param name="sord">order of sorting</param>
        /// <returns></returns>
        public ActionResult getJQgridData(int page, int rows, string search, string sidx, string sord, Int32 vehicleId)
        {
            PageSize = rows;
            int pageNoInQueryString = Convert.ToInt32(Request.QueryString["page"]);
            //var Brands = CwiRangesRepository.getBrands();
            IQueryable<cwiRangeDisplay> rowsNew = CwiRangesRepository.getCWIRangesForDisplay(vehicleId);
            rows = rowsNew.Count();
            var jsonDataNew = rowsNew.OrderBy(sidx + " " + sord).ToJqGridData(pageNoInQueryString, rows, null, search, null);
            return Json(jsonDataNew, JsonRequestBehavior.AllowGet);

        }
        #endregion
        
        #endregion

    }
}
