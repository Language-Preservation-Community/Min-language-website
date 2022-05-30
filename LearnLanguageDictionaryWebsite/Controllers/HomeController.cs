using LearnLanguageDictionaryWebsite.Models;
using LearnLanguagesDictionaryWebsite.Services;
using Microsoft.AspNetCore.Http;
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
            // It will start with English
            // Session acting like cookies and will store user information
            SelectedLanguage = AllAvailableLanguages.English;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        // In the front end, they will pass in the value
        // GET: Home/Language?name=(Hokkien)
        // Ignore the bracket, that is how you go to different pages :D
        public ActionResult Language(string Name)
        {
            switch (Name)
            {
                case nameof(AllAvailableLanguages.English):
                    SetSelectedLanguage(AllAvailableLanguages.English);
                    //HttpContext.Session.SetString("Language", AllAvailableLanguages.Hainanese.ToString());
                    Debug.Print("HttpContextSession" + HttpContext.Session.GetString("Language"));
                    return RedirectToAction("Index", "Home");
                case nameof(AllAvailableLanguages.Hakka):
                    SetSelectedLanguage(AllAvailableLanguages.Hakka);
                    return View("../Language/Hakka/Index");
                default:
                    // Using Relative Path to get into the view
                    SetSelectedLanguage(AllAvailableLanguages.Hokkien);
                    return View("../Language/Hokkien/Index");
            }
        }

        // For future reference as passing data from frontend to backend using ajax, it is not used for now
        //[HttpPost]
        //public JsonResult CategoryChosen([FromBody]string selectedLanguage)
        //{
        //    Debug.WriteLine(selectedLanguage.ToString());
        //    // The enum will try to convert the string from the form to the AllAvailableLanguagesEnum
        //    // If the Enum failed, it won't do anything which it shouldn't happens
        //    if (Enum.TryParse(selectedLanguage.ToString(), out AllAvailableLanguages selectedLanguageEnum))
        //    {
        //        Console.WriteLine("Successfulll");
        //        SetSelectedLanguage(selectedLanguageEnum);
        //    }
        //    else
        //    {
        //        Console.WriteLine("Failed");
        //    }

        //    // RedirectToRoute("About", "Home");

        //    // Depending
        //    return Json(selectedLanguage);
        //}

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
