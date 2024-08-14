using Microsoft.AspNetCore.Mvc;

namespace BabyCiao.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
