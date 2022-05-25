using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Services;
using System.Web.Services;
using static LearnLanguagesWebsite.Services.LanguagesInfo;

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

        public ActionResult RedirectWhenLanguageChange()
        {
            switch(SelectedLanguage)
            {
                case AllAvailableLanguages.English:
                    return View("Index");
                default:
                    // Using Relative Path to get into the view
                    return View("../Language/Hakka/Index");
            }
        }

        [HttpPost]
        public JsonResult CategoryChosen(string selectedLanguage)
        {
            // The enum will try to convert the string from the form to the AllAvailableLanguagesEnum
            // If the Enum failed, it won't do anything which it shouldn't happens
            if (Enum.TryParse(selectedLanguage, out AllAvailableLanguages selectedLanguageEnum))
            {
                Console.WriteLine("Successfulll");
                SetSelectedLanguage(selectedLanguageEnum);
            }
            else
            {
                Console.WriteLine("Failed");
            }

            // RedirectToRoute("About", "Home");

            // Depending
            return Json(selectedLanguage);
        }






    }
}