using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using DomainModel.Abstract;
using System.Web.UI;

namespace WebUI.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        private IBrandRepository brandRepository;
        private int PageSize = 5;


        public AdminController(IBrandRepository brandRepo)
        {
            brandRepository = brandRepo;
            ViewData["fistPage"] = 1;
            ViewData["LastPage"] = 3;
        }

        public ActionResult Brands(int page)
        {
            int numBrands;
            var brands = brandRepository.getBrands(page, PageSize, out numBrands);
            ViewData["TotalPages"] = (int)Math.Ceiling((double)numBrands / PageSize);
            ViewData["CurrentPage"] = page;
            return View(brands.ToList());
        }

        public ActionResult Countries(int page)
        {
           
            return View();
  
        }

        public ActionResult Customers(int page)
        {

            return View();

        }

        public ActionResult Languages()
        {
            return View();
        }

        public ActionResult Users()
        {
            return View();
        }

    }
}
