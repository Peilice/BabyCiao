using BabyCiao_Client.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BabyCiao_Client.Areas.andy_ebook.Controllers
{
    
    [Area("andy_ebook")]
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
        //GET:andy_ebook/Home/book_record/{ebookId} 
        [HttpGet]
        public IActionResult book_record(int id)
        {
            
            
            return View();
        }

        public IActionResult book_infos()
        {
            return View();
        }

        public IActionResult book_historySearch()
        {
            return View();
        }

        public IActionResult book_activityDaily()
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
