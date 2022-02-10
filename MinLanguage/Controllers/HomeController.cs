using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MinLanguage.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MinLanguage.Controllers
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

        public IActionResult About()
        {
            return View();
        }
        public IActionResult News()
        {
            return View();
        }
        public IActionResult Preface()
        {
            return View();
        }
        public IActionResult History()
        {
            return View();
        }
        public IActionResult Guide()
        {
            return View();
        }

        public IActionResult Literature()
        {
            return View();
        }
        public IActionResult Wikitionary_Index()
        {
            return View();
        }
        public IActionResult Start_Learning()
        {
            return View();
        }
        public IActionResult Bibliography()
        {
            return View();
        }

        public IActionResult Tutorial()
        {
            return View();
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
         
        public ActionResult pronunciations()
        {

            return View();
        }



    }
}
