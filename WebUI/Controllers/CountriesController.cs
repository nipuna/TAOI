using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using DomainModel.Abstract;
using DomainModel.jqGrid;
using System.Collections.Generic;


namespace WebUI.Controllers
{
    [Authorization]
    public class CountriesController : Controller
    {
        private ICountryRepository CountryRepository;
        public int PageSize = 10;
        public int currentPage;

        #region Default constructor
        public CountriesController(ICountryRepository countryRepo)
        {
            CountryRepository = countryRepo;
        }
        #endregion

        #region Renders the default view
        /// <summary>
        /// Author:Nipun Anand
        /// Renders the default view
        /// </summary>
        /// <returns>default view</returns>
        public ViewResult Index()
        {
            return View();
        }
        #endregion

        #region Creates a new Country
        /// <summary>
        /// Author: Nipun Anand
        /// creates a new Country
        /// </summary>
        /// <returns>View Result</returns>
        public ViewResult Create()
        {
            ViewData["Id"] = "";
            ViewData["CountryId"] = "";
            ViewData["CountryName"] = "";
            ViewData["CountryLogo"] = "";
            return View("Edit");
        }
        
        #endregion

        #region Editing Countries
        /// <summary>
        /// Rendering the editing defalut view
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ViewResult Edit(int Id)
        {
            var Country = CountryRepository.getCountry(Id).ToList();
            ViewData["Id"] = Id;
            ViewData["CountryId"] = Country[0].ID;
            ViewData["CountryName"] = Country[0].Name;
            ViewData["CountryLogo"] =  Country[0].Logo;
            return View();
        }

        /// <summary>
        /// Handling the postback for editing
        /// </summary>
        /// <param name="name">name of the Country to be edited</param>
        /// <param name="Id">id of the Country to be edited</param>
        /// <param name="bdLogo">existing logo path of the Country to be edited</param>
        /// <param name="logo">new logo which is required to be replace to the existing logo</param>
        /// <returns></returns>
        [HttpPost]
        public ViewResult Edit(string name, Int32 Id, string cntLogo, HttpPostedFileBase logo)
        {
            #region Validations
            if (name == "")
            {
                ModelState.AddModelError("Name", "Name is required");
            }
            if (Id == -1 && logo == null)
            {
                ModelState.AddModelError("logo", "Choose Logo");
            }
            if (Id != -1 && !ModelState.IsValid)
            {
                var Country = CountryRepository.getCountry(Id).ToList();
                ViewData["Id"] = Id;
                ViewData["CountryId"] = Country[0].ID;
                ViewData["CountryName"] = Country[0].Name;
                ViewData["CountryLogo"] = Country[0].Logo;
                return View();
            }
            else if (Id == -1 && !ModelState.IsValid)
            {
                ViewData["CountryId"] = "";
                return View();
            }
            #endregion
            if (Id == -1 )
            {
                #region Creating New Country
                try
                {
                    string imgpath = "";
                    imgpath = "/Content/Images/flags/" + name + logo.FileName.Substring(logo.FileName.LastIndexOf("."), logo.FileName.Length - logo.FileName.LastIndexOf("."));
                    FileStream fs = new FileStream(Server.MapPath("~" + imgpath), FileMode.Create);
                    BinaryWriter bw = new BinaryWriter(fs);
                    Byte[] buffer = new Byte[logo.ContentLength];
                    logo.InputStream.Read(buffer, 0, logo.ContentLength);
                    bw.Write(buffer);
                    bw.Close();
                    fs.Close();
                    int status = CountryRepository.createCountry(name, imgpath);
                    if (status == -1)
                    {
                        ModelState.AddModelError("name", "Brand Name Already Exists");
                        ViewData["BrandId"] = "";
                        return View();
                    }
                    return View("Index");
                }
                catch (Exception e)
                {
                    //ViewData["CountryId"] = ""; 
                    ModelState.AddModelError("exc", e.Message);
                    return View();
                }
                
                #endregion
            }
            else
            {
                #region Editing New Country
                try
                {
                    string imgpath = "";
                    
                    if (logo != null)
                    {
                        imgpath = "/Content/Images/flags/" + name + logo.FileName.Substring(logo.FileName.LastIndexOf("."), logo.FileName.Length - logo.FileName.LastIndexOf("."));
                        //Deleting the old file
                        if (!String.IsNullOrEmpty(cntLogo))
                        {
                            System.IO.File.Delete(Server.MapPath("~" + cntLogo));
                        }
                        //Creating the new file
                        FileStream fs = new FileStream(Server.MapPath("~" + imgpath), FileMode.Create);
                        BinaryWriter bw = new BinaryWriter(fs);
                        Byte[] buffer = new Byte[logo.ContentLength];
                        logo.InputStream.Read(buffer, 0, logo.ContentLength);
                        bw.Write(buffer);
                        bw.Close();
                        fs.Close();
                        CountryRepository.saveCountry(Id, name, imgpath);
                    }
                    else
                    {
                        CountryRepository.saveCountry(Id, name, cntLogo);
                    }
                    
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

        #region Deleting Countries
        /// <summary>
        /// Author:Nipun Anand
        /// Deletes an already existing Country
        /// </summary>
        /// <param name="Id">ID of the Country to be deleted</param>
        /// <returns>Redirects to the Index action</returns>
        public ViewResult delete(int Id)
        {
            int status;
            status = CountryRepository.deleteCountry(Id);
            if (status == -1)
            {
                ModelState.AddModelError("country", "Cannot delete the Country.");
            }
            return View("Index");
        }
        #endregion

        #region Custom methods

        #region To format Country data in JSON format
        /// <summary>
        /// Author:Nipun Anand
        /// Returns the Country data in JSON format sutiable for the JqGrid to display
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
            var Countries = CountryRepository.getCountries();
            IQueryable<countryDisplay> rowsNew = CountryRepository.getCountriesForDisplay(Countries);

            var jsonDataNew = rowsNew.OrderBy(sidx + " " + sord).ToJqGridData(pageNoInQueryString, rows, null, search,
                new[] { "Name" });
            return Json(jsonDataNew, JsonRequestBehavior.AllowGet);


        }
        #endregion
        
        #endregion

    }
}
