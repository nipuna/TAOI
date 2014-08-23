using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainModel.jqGrid;
using DomainModel.Abstract;
using System.Web.Mvc.Ajax;
using System.Linq.Dynamic;
using System.Collections.Generic;
using BusinessLogic;
using System.Web.Script.Serialization;

namespace WebUI.Controllers
{
    public class VehiclesController : Controller
    {
        
        #region Private members
        private IVehicleRepository VehicleRepository;
        public int PageSize = 5;
        public int currentPage;
        #endregion

        #region Default constructor
        public VehiclesController(IVehicleRepository VehicleRepo)
        {
            VehicleRepository = VehicleRepo;
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

        #region Creates a new Vehicle
        /// <summary>
        /// Author: Nipun Aand
        /// creates a new Vehicle
        /// </summary>
        /// <returns>View Result</returns>
        public ViewResult Create()
        {
            ViewData["Id"] = "";
            ViewData["VehicleId"] = "";
            getDropDownListDetails(0);
            return View("Edit");
        }
        #endregion

        #region Editing Vehicles
        /// <summary>
        /// Rendering the editing defalut view
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public ViewResult Edit(int Id)
        {
            getVehicleDetailForEdit(Id);
            return View();
        }


        /// <summary>
        /// Handling the postback for editing
        /// </summary>
        /// <param name="customer">name of the Vehicle to be edited</param>
        /// <param name="Id">id of the Vehicle to be edited</param>
        /// <param name="bdLogo">existing logo path of the Vehicle to be edited</param>
        /// <param name="logo">new logo which is required to be replace to the existing logo</param>
        /// <returns></returns>
        [HttpPost]
        public ViewResult Edit(vehicleDisplay vehicle , HttpPostedFileBase logo)
        {
            #region Validations
            if (vehicle.Model == null)
            {
                ModelState.AddModelError("model", "Model is required");
            }
            if (vehicle.Brand == "0")
            {
                ModelState.AddModelError("Brand", "Brand is required");
            }
            if (vehicle.Customer == "0")
            {
                ModelState.AddModelError("Customer", "Customer is required");
            }
            if (vehicle.ID == -1 && !ModelState.IsValid)
            {
                ViewData["Id"] = "";
                ViewData["VehicleId"] = "";
                getDropDownListDetails(0);
                return View();
            }
            else if (vehicle.ID != -1 && !ModelState.IsValid)
            {
                getVehicleDetailForEdit(vehicle.ID);
                return View();
            }
            #endregion
            if (vehicle.ID == -1)
            {
                #region Creating New Vehicle
                try
                {
                    int status = VehicleRepository.createVehicle(vehicle);
                    if (status == -1)
                    {
                        ModelState.AddModelError("model", "Vehicle Model Already Exists");
                        ViewData["VehicleId"] = "";
                        getDropDownListDetails(0);
                        return View();
                    }
                    return View("Index");
                }
                catch (Exception)
                {
                    //ViewData["VehicleId"] = ""; 
                    return View("Index");
                }

                #endregion
            }
            else
            {
                #region Editing New Vehicle
                try
                {
                    VehicleRepository.saveVehicle(vehicle.ID, vehicle.Model);
                    return View("Index");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("exc", e.Message);
                    return View();
                }
                #endregion
            }
        }
        #endregion

        #region Deleting Vehicles
        /// <summary>
        /// Author:Nipun Anand
        /// Deletes an already existing Vehicle
        /// </summary>
        /// <param name="Id">ID of the Vehicle to be deleted</param>
        /// <returns>Redirects to the Index action</returns>
        public ViewResult delete(int Id)
        {
            int status;
            status = VehicleRepository.deleteVehicle(Id);
            if (status == -1)
            {
                ModelState.AddModelError("Vehicle", "Cannot delete the Vehicle.");
                //return RedirectToAction("Index", "Vehicles");
            }
            return View("Index");

        }
        #endregion

        #region Custom methodsprivate void getVehicleDetailForEdit(int Id)
        {
            var Vehicle = VehicleRepository.getVehicle(Id).ToList();
            ViewData["Id"] = Id;
            ViewData["VehicleId"] = Vehicle[0].ID;
            getDropDownListDetails(Id);
            ViewData["VehicleModel"] = Vehicle[0].Model;
        }

        #region To format Vehicle data in JSON format
        /// <summary>
        /// Author:Nipun Anand
        /// Returns the Vehicle data in JSON format sutiable for the JqGrid to display
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
            var Vehicles = VehicleRepository.getVehicles();
            IQueryable<vehicleDisplay> rowsNew = VehicleRepository.getVehiclesForDisplay(Vehicles);

            var jsonDataNew = rowsNew.OrderBy(sidx + " " + sord).ToJqGridData(pageNoInQueryString, rows, null, search, new[] { "Model" });
            return Json(jsonDataNew, JsonRequestBehavior.AllowGet);
        }
        #endregion

        public void getDropDownListDetails(Int32 vehicleId )
        {
            Dictionary<String, int> Vehicle = VehicleRepository.getCustAndBrandIdForVeh(vehicleId);
            
            DomainModel.Concrete.SQLCustomerRepository cusRepo = new DomainModel.Concrete.SQLCustomerRepository();
            var allCustomer = cusRepo.getCustomers();
            #region drop down select list
            List<drpDetails> customersForDpDwn = new List<drpDetails>();
            customersForDpDwn.Add(new drpDetails(0, "Select", false));
            foreach (var customer in allCustomer)
            {
                int? id = customer.ID;
                customersForDpDwn.Add(new drpDetails(customer.ID, customer.Name.ToString(), false));
            }
            var items = new SelectList(customersForDpDwn, "ID", "Name", Vehicle["customerId"]);
            #endregion
            if (vehicleId != 0)
            {

                DomainModel.Concrete.SQLBrandRepository bndRepo = new DomainModel.Concrete.SQLBrandRepository();
                var allBrands = bndRepo.getBrands();
                #region drop down select list
                List<drpDetails> brandsForDpDwn = new List<drpDetails>();
                brandsForDpDwn.Add(new drpDetails(0, "Select", false));
                foreach (var brand in allBrands)
                {
                    int? id = brand.ID;
                    brandsForDpDwn.Add(new drpDetails(brand.ID, brand.Name.ToString(), false));
                }
                var brandItems = new SelectList(brandsForDpDwn, "ID", "Name", Vehicle["brandId"]);
                #endregion
                ViewData["VehicleBrand"] = brandItems;
            }
            else
            {
                List<drpDetails> brandsForDpDwn = new List<drpDetails>();
                brandsForDpDwn.Add(new drpDetails(0, "Choose Customer", false));
                var brandItems = new SelectList(brandsForDpDwn, "ID", "Name", 0);
                ViewData["VehicleBrand"] = brandItems;
            }
            ViewData["VehicleCustomer"] = items;

        }
        #endregion

        public string getBrandsForCustomer(Int32 customerId)
        {
            DomainModel.Concrete.SQLBrandRepository brndRepo = new DomainModel.Concrete.SQLBrandRepository();
            var brands = brndRepo.getBrandsForCustomer(customerId);

            var brandsCh = (from b in brands
                              select new
                              {
                                  b.ID,
                                  b.Name
                              }).ToArray();
            JavaScriptSerializer sr = new JavaScriptSerializer();
            string nameList = sr.Serialize(brandsCh);
            return nameList;
        }
    }
}
