using BabyCiao.Models;
using BabyCiao.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BabyCiao.Controllers
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
        
        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Contact()
        {
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Contact([Bind("Name,Email,Phone,Title,content")]ContactViewModel cvm)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
                return View(cvm);   

        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult NoLogin()
        {
            return View();
        }
        public IActionResult NoRole()
        {
            return View();
        }
    }
}
