using BabyCiao_Client.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BabyCiao_Client.ViewModels;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace BabyCiao_Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _httpcontextAccessor;
        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpcontextAccessor)
        {
            _logger = logger;
            _httpcontextAccessor = httpcontextAccessor;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        



        //[Route("Home/GetUserName")]
        //[HttpGet]
        //public async Task<ActionResult<string>> GetUserName()
        //{
        //    var user = _httpcontextAccessor.HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Name);
        //    return user;
        //}
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
