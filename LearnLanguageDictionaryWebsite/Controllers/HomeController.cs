using LearnLanguageDictionaryWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using static LearnLanguagesDictionaryWebsite.Services.LanguagesInfo;

namespace LearnLanguageDictionaryWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult RedirectWhenLanguageChange()
        {
            switch (SelectedLanguage)
            {
                case AllAvailableLanguages.English:
                    return View("Index");
                default:
                    // Using Relative Path to get into the view
                    return View("../Language/Hakka/Index");
            }
        }

        //public ActionResult Language(string language)
        //{
        //    if (language.Equals(AllAvailableLanguages.English.ToString()))
        //    {

        //    }

        //    switch (language)
        //    {
        //        case "AHHH":
        //            break;

        //        case AllAvailableLanguages.English.ToString():
        //            return View("Index");
        //        default:
        //            // Using Relative Path to get into the view
        //            return View("../Language/Hakka/Index");
        //    }
        //}

        [HttpPost]
        public JsonResult CategoryChosen([FromBody]string selectedLanguage)
        {
            Debug.WriteLine(selectedLanguage.ToString());
            // The enum will try to convert the string from the form to the AllAvailableLanguagesEnum
            // If the Enum failed, it won't do anything which it shouldn't happens
            if (Enum.TryParse(selectedLanguage.ToString(), out AllAvailableLanguages selectedLanguageEnum))
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
