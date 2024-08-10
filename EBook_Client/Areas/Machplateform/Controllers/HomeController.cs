using Microsoft.AspNetCore.Mvc;

namespace BabyCiao_Client.Areas.Machplateform.Controllers
{
    
    [Area("Machplateform")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
           return View();
        }
        public IActionResult MatchIndex()
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

        public IActionResult Notification()
        {
            return View();
        }

        public IActionResult NannyApply()
        {
            return View();
        }
        public IActionResult MyIndex()
        {
            return View();
        }
        public IActionResult ContactNTB()
        {
            return View();
        }

        public IActionResult SignContract()
        {
            return View();
        }

    }
}
