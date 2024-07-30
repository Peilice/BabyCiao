using BabyCiao_Client.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BabyCiao_Client.Areas.Match.Controllers
{
    [Area("Match")]

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult MatchIndex()
            {
                return View();
            }

            public IActionResult NannyInformation()
            {
                return View();
            }

            public IActionResult Babyinformation()
            {
                return View();
            }

            public IActionResult NannyResume()
            {
                return View();
            }
            public IActionResult BabyResume()
            {
                return View();
            }
            public IActionResult IncreaseNannyResume()
            {
                return View();
            }
            public IActionResult IncreaseBabyResume()
            {
                return View();
            }
            public IActionResult NannyApply()
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

    
