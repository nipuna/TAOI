using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using DomainModel.Abstract;
using DomainModel.Entities;

namespace WebUI.Controllers
{

    [Authorization]
    public class LanguagesController : Controller
    {
        //
        // GET: /Languages/

        private ICultureRepository cultureRepository;

        public LanguagesController(ICultureRepository cultureRepository)
        {
            this.cultureRepository = cultureRepository;
        }

        public ActionResult Index()
        {
            List<DomainModel.Entities.Culture> lang = cultureRepository.Cultures.ToList(); 
            return View(lang);
        }

        public ActionResult Toggle(int id)
        {
            cultureRepository.ToggleSupported(id);
            return RedirectToAction("Index"); 
        }
    }
}
