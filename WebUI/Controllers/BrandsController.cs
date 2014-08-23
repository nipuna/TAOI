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
using System.Web.Script.Serialization;

namespace WebUI.Controllers
{
    [Authorization]
    public class BrandsController : Controller
    {
        #region Private members
        private IBrandRepository brandRepository;
        public int PageSize = 5;
        public int currentPage;
        #endregion

        #region Default constructor
        public BrandsController(IBrandRepository brandRepo)
        {
            brandRepository = brandRepo;
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

        #region Creates a new brand
        /// <summary>
        /// Author: Nipun Aand
        /// creates a new brand
        /// </summary>
        /// <returns>View Result</returns>
        public ViewResult Create()
        {
            ViewData["Id"] = "";
            ViewData["brandId"] = "";
            ViewData["brandName"] = "";
            ViewData["brandLogo"] = "";
            return View("Edit");
        }
        #endregion

        #region Editing Brands
        /// <summary>
        /// Rendering the editing defalut view
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public ViewResult Edit(int Id)
        {
            var brand = brandRepository.getBrand(Id).ToList();
            ViewData["Id"] = Id;
            ViewData["brandId"] = brand[0].ID;
            ViewData["brandName"] = brand[0].Name;
            ViewData["brandLogo"] = brand[0].Logo;
            return View();
        }

        /// <summary>
        /// Handling the postback for editing
        /// </summary>
        /// <param name="name">name of the brand to be edited</param>
        /// <param name="Id">id of the brand to be edited</param>
        /// <param name="bdLogo">existing logo path of the brand to be edited</param>
        /// <param name="logo">new logo which is required to be replace to the existing logo</param>
        /// <returns></returns>
        [HttpPost]
        public ViewResult Edit(string name, Int32 Id, string bdLogo, HttpPostedFileBase logo)
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
                var Brand = brandRepository.getBrand(Id).ToList();
                ViewData["Id"] = Id;
                ViewData["BrandId"] = Brand[0].ID;
                ViewData["BrandName"] = Brand[0].Name;
                ViewData["BrandLogo"] = Brand[0].Logo;
                return View();
            }
            else if (Id == -1 && !ModelState.IsValid)
            {
                ViewData["BrandId"] = "";
                return View();
            }
            #endregion
            if (Id == -1)
            {
                #region Creating New Brand
                try
                {
                    string imgpath = "";
                    string logoFileExtension = logo.FileName.Substring(logo.FileName.LastIndexOf("."), logo.FileName.Length - logo.FileName.LastIndexOf("."));
                    imgpath = "/Content/Images/" + name + logoFileExtension;
                    //System.IO.File.Exists(
                    FileStream fs = new FileStream(Server.MapPath("~" + imgpath), FileMode.Create);
                    BinaryWriter bw = new BinaryWriter(fs);
                    Byte[] buffer = new Byte[logo.ContentLength];
                    logo.InputStream.Read(buffer, 0, logo.ContentLength);
                    bw.Write(buffer);
                    bw.Close();
                    fs.Close();
                    int status = brandRepository.createBrand(name, imgpath);
                    if (status == -1)
                    {
                        ModelState.AddModelError("name", "Brand Name Already Exists");
                        ViewData["BrandId"] = "";
                        return View();
                    }
                    return View("Index");
                }
                catch (Exception)
                {
                    //ViewData["BrandId"] = ""; 
                    return View("Index");
                }

                #endregion
            }
            else
            {
                #region Editing New Brand
                try
                {
                    string imgpath = "";

                    if (logo != null)
                    {
                        imgpath = "/Content/Images/" + name + logo.FileName.Substring(logo.FileName.LastIndexOf("."), logo.FileName.Length - logo.FileName.LastIndexOf("."));
                        //Deleting the old file
                        if (!String.IsNullOrEmpty(bdLogo))
                        {
                            System.IO.File.Delete(Server.MapPath("~" + bdLogo));
                        }
                        //Creating the new file
                        FileStream fs = new FileStream(Server.MapPath("~" + imgpath), FileMode.Create);
                        BinaryWriter bw = new BinaryWriter(fs);
                        Byte[] buffer = new Byte[logo.ContentLength];
                        logo.InputStream.Read(buffer, 0, logo.ContentLength);
                        bw.Write(buffer);
                        bw.Close();
                        fs.Close();
                        brandRepository.saveBrand(Id, name, imgpath);
                    }
                    else
                    {
                        brandRepository.saveBrand(Id, name, bdLogo);
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

        #region Deleting Brands
        /// <summary>
        /// Author:Nipun Anand
        /// Deletes an already existing brand
        /// </summary>
        /// <param name="Id">ID of the brand to be deleted</param>
        /// <returns>Redirects to the Index action</returns>
        public ViewResult delete(int Id)
        {
            int status;
            status = brandRepository.deleteBrand(Id);
            if (status == -1)
            {
                ModelState.AddModelError("Brand", "Cannot delete the Brand.");
                //return RedirectToAction("Index", "Brands");
            }
            return View("Index");

        }
        #endregion

        #region Custom methods

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
        public ActionResult getJQgridData(int page, int rows, string search, string sidx, string sord)
        {
            PageSize = rows;
            int pageNoInQueryString = Convert.ToInt32(Request.QueryString["page"]);
            var Brands = brandRepository.getBrands();
            IQueryable<brandDisplay> rowsNew = brandRepository.getBrandsForDisplay(Brands);
            JavaScriptSerializer sr = new JavaScriptSerializer();
            var jsonDataNew = rowsNew.OrderBy(sidx + " " + sord).ToJqGridData(pageNoInQueryString, rows, null, search, new[] { "Name" });
            return Json(jsonDataNew, JsonRequestBehavior.AllowGet);
            //return sr.Serialize(jsonDataNew.

        }
        #endregion

        public string imageRefresher()
        {
            string strFileName = Path.GetFileName(ControllerContext.HttpContext.Request.Files[0].FileName);
            string strExtension = Path.GetExtension(ControllerContext.HttpContext.Request.Files[0].FileName).ToLower();
            string strSaveLocation = ControllerContext.HttpContext.Server.MapPath("..//Content//images//unknown//") + "" + strFileName;
            ControllerContext.HttpContext.Request.Files[0].SaveAs(strSaveLocation);
            return "/Content/images/unknown/" + strFileName;
        }

        public void imageDownloader()
        {
            string strFileName = Path.GetFileName(ControllerContext.HttpContext.Request.Files[0].FileName);
            string strExtension = Path.GetExtension(ControllerContext.HttpContext.Request.Files[0].FileName).ToLower();
            string strSaveLocation = ControllerContext.HttpContext.Server.MapPath("..//Content//images//") + "" + strFileName;
            ControllerContext.HttpContext.Request.Files[0].SaveAs(strSaveLocation);

            string fname = "/Content/images/" + strFileName;
            string path = fname;
            string name = Path.GetFileName(path);
            string ext = Path.GetExtension(path);
            string type = "";

            if (ext != null)
            {
                switch (ext.ToLower())
                {
                    case ".htm":
                    case ".html":
                        type = "text/HTML";
                        break;

                    case ".jpg":
                        type = "image/jpeg";
                        break;

                    case ".gif":
                        type = "image/gif";
                        break;

                    case ".txt":
                        type = "text/plain";
                        break;

                    case ".doc":
                    case ".rtf":
                        type = "Application/msword";
                        break;
                }
            }
            Response.ContentType = type;

            Response.AppendHeader("content-disposition",
                "attachment; filename=" + name);
            Response.TransmitFile(fname);

        }

        #endregion

    }
}
