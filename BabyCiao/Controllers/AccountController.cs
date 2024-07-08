using Microsoft.AspNetCore.Mvc;
using BabyCiao.ViewModels;
using BabyCiao.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace BabyCiao.Controllers
{
    public class AccountController : Controller
    {
        private readonly BabyciaoContext _context;

        public AccountController(BabyciaoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserAccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.UserAccounts
                    .FirstOrDefault(u => u.Account == model.Account && u.Password == model.Password);

                if (user != null)
                {
                    // 成功登入邏輯
                    HttpContext.Session.SetString("UserAccount", user.Account);

                    // 重定向到儀表板或主頁，這個頁面使用 _Layout
                    return RedirectToAction("Index", "BabyResumes");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "無效的登入嘗試");
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserRegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = _context.UserAccounts.FirstOrDefault(u => u.Account == model.Account);
                if (existingUser != null)
                {
                    ModelState.AddModelError(string.Empty, "用戶名已經存在");
                    return View(model);
                }

                var newUser = new UserAccount
                {
                    Account = model.Account,
                    Password = model.Password,
                    Permissions = 1,
                    Vip = false
                };

                _context.UserAccounts.Add(newUser);
                _context.SaveChanges();

                return RedirectToAction("Login");
            }

            return View(model);
        }
    }
}
