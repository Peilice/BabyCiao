using BabyCiao.Models;
using Microsoft.AspNetCore.Mvc;

namespace BabyCiao.Controllers
{
    public class AuthController : Controller
    {
        private readonly BabyciaoContext _context;
        public AuthController(BabyciaoContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
