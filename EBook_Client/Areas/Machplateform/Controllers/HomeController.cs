using Microsoft.AspNetCore.Mvc;

namespace BabyCiao_Client.Areas.Machplateform.Controllers
{
    
    [Area("Machplateform")]
    public class HomeController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}
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
    }
}
