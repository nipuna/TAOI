using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using DomainModel.Concrete;
using DomainModel.Abstract;

namespace WebUI.Controllers
{
    public class UICategoryController : Controller
    {
        private IUICategoryRepository uiCategoryRepository;

        public UICategoryController(IUICategoryRepository uiCategoryRepository)
        {
            this.uiCategoryRepository = uiCategoryRepository;
        }

        public ViewResult Index()
        {
            return View(uiCategoryRepository.UICategories.ToList());
        }
    }
}
