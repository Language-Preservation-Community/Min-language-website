using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearnLanguagesWebsite.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            // SetSelectedLanguage(AllAvailableLanguages.English);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //[HttpPost]
        //public ActionResult CategoryChosen(AllAvailableLanguages selectedLanguage)
        //{
        //    SetSelectedLanguage(selectedLanguage);

        //    return View();
        //}


        

    }
}