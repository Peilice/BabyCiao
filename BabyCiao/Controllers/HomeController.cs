using BabyCiao.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;

namespace BabyCiao.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BabyCiaoContext _context;


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //string? account_name = HttpContext.Session.GetString("my_account_name");
            //if (account_name!=null)
            //{
            //    ViewBag.AccountName = account_name;
            //}


            return View();
        }
        [Authorize(Roles = "公告編輯,委託單建立")]
        
        public IActionResult Privacy()
        {
            return View();
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
