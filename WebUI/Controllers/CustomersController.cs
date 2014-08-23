using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainModel.jqGrid;
using DomainModel.Abstract;
using DomainModel.Concrete;
using System.Web.Mvc.Ajax;
using System.Linq.Dynamic;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;

namespace WebUI.Controllers
{
    [Authorization]
    public class CustomersController : Controller
    {
        
        #region Private members
        private ICustomerRepository customerRepository;
        public int PageSize = 5;
        public int currentPage; 
        JavaScriptSerializer sr = new JavaScriptSerializer();
        #endregion

        #region Default constructor
        public CustomersController(ICustomerRepository customerRepo)
        {
            customerRepository = customerRepo;
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

        #region Creates a new customer
        /// <summary>
        /// Author: Nipun Aand
        /// creates a new customer
        /// </summary>
        /// <returns>View Result</returns>
        public ViewResult Create()
        {
                         
            ViewData["Id"] = "";
            ViewData["customerId"] = "";
            ViewData["customerName"] = "";
            ViewData["customerLogo"] = "";
            ViewData["website"] = "";
            ViewData["companyNo"] = "";
            ViewData["vatNO"] = "";
            ViewData["customerLogo"] = "";
            ViewData["comment"] = "";
            ViewData["brands"] = "";
            return View("Edit");
        }
        #endregion

        #region Editing customers
        /// <summary>
        /// Rendering the editing defalut view
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public ViewResult Edit(int Id)
        {
            getCustomerDetailForEdit(Id);
            return View();

        }

        


        /// <summary>
        /// Handling the postback for editing
        /// </summary>
        /// <param name="name">name of the customer to be edited</param>
        /// <param name="Id">id of the customer to be edited</param>
        /// <param name="ctLogo">existing logo path of the customer to be edited</param>
        /// <param name="logo">new logo which is required to be replace to the existing logo</param>
        /// <returns></returns>
        [HttpPost]
        public ViewResult Edit(int Id, DomainModel.Entities.Customer customer, List<Int32> brands, HttpPostedFileBase logo)
        {
            #region Validations
            if (customer.Name == null)
            {
                ModelState.AddModelError("name", "Name is required");
            }
            //if (customer.Website == null)
            //{
            //    ModelState.AddModelError("website", "website is required");
            //}
            //if (customer.CompanyNumber == null)
            //{
            //    ModelState.AddModelError("companynumber", "Company Number is required");
            //}
            //if (customer.VATNumber == null)
            //{
            //    ModelState.AddModelError("vatnumber", "VAT Number is required");
            //}
            //if (customer.Comments == null)
            //{
            //    ModelState.AddModelError("comments", "Comments is required");
            //}
            if (customer.ID == -1 && !ModelState.IsValid)
            {
                ViewData["Id"] = "";
                ViewData["customerId"] = "";
                ViewData["customerName"] = "";
                ViewData["customerLogo"] = "";
                ViewData["website"] = "";
                ViewData["companyNo"] = "";
                ViewData["vatNO"] = "";
                ViewData["customerLogo"] = "";
                ViewData["comment"] = "";
                ViewData["brands"] = "";
                return View();
            }
            else if (customer.ID != -1 && !ModelState.IsValid)
            {
                getCustomerDetailForEdit(customer.ID);
                return View();
            }
            #endregion
            if (Id == -1)
            {
                #region Creating New customer
                try
                {
                    customer = customerRepository.createdCustomerData(customer, brands);

                    customerRepository.createCustomer(customer);
                    return View("Index");
                }
                catch (Exception)
                {
                    //ViewData["customerId"] = ""; 
                    return View("Index");
                }

                #endregion
            }
            else
            {
                #region Editing New customer
                try
                {

                    customer = customerRepository.editedCustomerData(customer, brands);

                    customerRepository.saveCustomer(customer);
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

        #region Deleting customers
        /// <summary>
        /// Author:Nipun Anand
        /// Deletes an already existing customer
        /// </summary>
        /// <param name="Id">ID of the customer to be deleted</param>
        /// <returns>Redirects to the Index action</returns>
        public ViewResult delete(int Id)
        {
            int status;
            status = customerRepository.deleteCustomer(Id);
            if (status == -1)
            {
                ModelState.AddModelError("customer", "Cannot delete the customer.");
                //return RedirectToAction("Index", "customers");
            }
            return View("Index");

        }
        #endregion

        #region Custom methods
private void getCustomerDetailForEdit(int Id)
        {
            var customer = customerRepository.getCustomer(Id).ToList();
            ViewData["Id"] = Id;
            ViewData["customerId"] = customer[0].ID;
            ViewData["customerName"] = customer[0].Name;
            ViewData["website"] = customer[0].Website;
            ViewData["companyNo"] = customer[0].CompanyNumber;
            ViewData["vatNO"] = customer[0].VATNumber;
            ViewData["customerLogo"] = "";
            ViewData["comment"] = customer[0].Comments;
            string brands = ""; int i = 1;
            foreach (var brand in customer[0].Brands)
            {
                brands = brands + getBrandsHtml(brand);
                i++;
            }
            ViewData["brands"] = brands;
        }

        public string getBrand(int Id)
        {
            SQLBrandRepository brandRepository = new SQLBrandRepository();
            var brand = brandRepository.getBrand(Id).ToList();
            string sBrand = getBrandsHtml(brand[0]);
            return sBrand;
        }

        private static string getBrandsHtml( DomainModel.Entities.Brand brand)
        {
            string brands = "";
            brands = brands + "<div id=\"imgContainer_" + brand.ID + "\" style=\"width:100px;background-color:Black;float:left;margin-top:2px; margin-bottom:2px;margin-left:5px;margin-right:5px;\" >";
            brands = brands + "<div style=\"float:right;\" >";
            //brands = brands + "<a href=\"#\" class=\"lbAction\" >";
            brands = brands + "<img id=\"close\" onclick=\"removingBrand(" + brand.ID + ")\" style=\"border-width: 0px;\" src=\"/Content/Images/close.gif\" />";
            //brands = brands + "</a>";
            brands = brands + "</div>";
            brands = brands + "<div style=\"text-align:center;\" >";
            brands = brands + "<div id=\"img_" + brand.Name + "\" style=\"margin-top: 8px;margin-bottom: 0px;margin-left: auto; margin-right: auto;width:53px;\" >";
            brands = brands + "<img src=\"" + brand.Logo + "\" style=\"width:60px;height:60px;\" >";
            brands = brands + "</div>";
            brands = brands + "<div id=\"name" + brand.Name + "\" style=\"margin: 0px 10px 8px 10px;color:#fff;text-align:center;\" >";
            brands = brands + brand.Name;
            brands = brands + "</div>";
            brands = brands + "<input id=\"" + brand.ID + "\" name=\"brands\" value=\"" + brand.ID + "\" type=\"hidden\" />";
            brands = brands + "</div>";
            brands = brands + "</div>";
            return brands;
        }

        #region To format customer data in JSON format
        /// <summary>
        /// Author:Nipun Anand
        /// Returns the customer data in JSON format sutiable for the JqGrid to display
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
            var customers = customerRepository.getCustomers();
            IQueryable<customerDisplay> rowsNew = customerRepository.getCustomersForDisplay(customers);

            var jsonDataNew = rowsNew.OrderBy(sidx + " " + sord).ToJqGridData(pageNoInQueryString, rows, null, search, new[] { "Name" });
            return Json(jsonDataNew, JsonRequestBehavior.AllowGet);


        }
        #endregion

        #region get All Brands Like(starting with a set of characters)
        /// <summary>
        /// gets All Brands Like
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        public string getBrandLike(string q)
        {
            //string q = "";
            SQLBrandRepository brandRepository = new SQLBrandRepository();
            var brands = brandRepository.getBrandsLike(q);
            string sBrand = "";
            //foreach (var brand in brands)
            //{
            //    sBrand = sBrand + " " + brand;
            //}
            sBrand = sr.Serialize(brands);
            return sBrand;
        }
        #endregion

        #region Get Brand for name
        /// <summary>
        /// gets Brand for name
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        public string getBrandForName(string name)
        {
            SQLBrandRepository brandRepository = new SQLBrandRepository();
            var brands = brandRepository.getBrandForName(name);
            string sBrand = "";
            sBrand = sr.Serialize(brands);
            return sBrand;
        }
        #endregion

        #endregion

    }
}
