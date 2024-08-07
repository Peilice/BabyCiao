using BabyCiao_Client.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BabyCiao_Client.Areas.LilySArea.Controllers
{
    [Area("LilySArea")]
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
        public IActionResult Order()
        {
            return View();
        }
        public IActionResult MyProducts()
        {
            return View();
        }

        public IActionResult MyOrders()
        {
            return View();
        }
        

        public IActionResult MyFavorite()
        {
            return View();
        }
        public IActionResult CreateProduct()
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
