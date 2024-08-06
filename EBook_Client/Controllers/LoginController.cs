using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.CommandLine;

namespace BabyCiao_Client.Controllers
{
    
    public class LoginController : Controller
    {

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult information()
        {
            return View();
        }

        [HttpPost]
        public IActionResult 登出()
        {
            // 添加登出邏輯
            return RedirectToAction("Index", "Home");
        }
        public IActionResult resetpassword()
        {
            return View();
        }

        public IActionResult sendVerificationEmail()
        {
            return View();
        }
    }
}
