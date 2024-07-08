using BabyCiao.GlobarVal;
using BabyCiao.Models;
using BabyCiao.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol;
using System.Runtime.Intrinsics.X86;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace BabyCiao.Controllers
{
    public class andy_loginController : Controller
    {
        private readonly BabyciaoContext _context;
        


        public  andy_loginController(BabyciaoContext context)
        {
            _context= context;
        }

        

        //Get: andy_login/login
        
        public IActionResult login()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult login([Bind("name,password")] andy_loginViewModel my_account)
        {
            var accounts=_context.UserAccounts.Where(m=>m.Account== my_account.name && m.Password== my_account.password).FirstOrDefault();
           

            if (accounts != null)
            {
                if (ModelState.IsValid)
                {
                    ViewBag.ErrorMessage = "帳號登錄驗證成功";

                    //將登錄者姓名、權限加入到Claim類別中
                    List<string> user_roles = getKeysByAccountName(accounts.Account);
                    var varClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name,accounts.Account),

                    };
                    foreach (string role in user_roles) {
                        varClaims.Add(new Claim(ClaimTypes.Role, role));
                    }
                    OnlineUsers.AddOnlineUser(accounts.Account);

                    var varClaimsIdentity = new ClaimsIdentity(varClaims, CookieAuthenticationDefaults.AuthenticationScheme);

                    HttpContextAccessor httpContextAccessor= new HttpContextAccessor();

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(varClaimsIdentity));
                    TempData["statue"] = "1";

                    return RedirectToAction("Index","Home", TempData["statue"]);

                }
                ViewBag.ErrorMessage = "防偽標籤驗證失敗";
                return View();
            }
            else
            {
                // 登入失敗，顯示錯誤訊息或其他處理
                ViewBag.ErrorMessage = "帳號或密碼錯誤";
                return View();
            }

        }
        List<string> getKeysByAccountName(string name)
        {
            //獲得登錄者的權限編號
            var user = _context.UserAccounts.Where(u=>u.Account==name).FirstOrDefault();
            var user_Permission = user.Permissions;

            //將權限群組及功能項目等2項table合併(join)
            var authGroup = _context.FunctionSettings.Join(_context.SystemFunctions, f => f.FunctionCodeSystemFunction, s => s.FunctionId, (f, s) => new
            {
                權限群組=f.GroupIdAuthGroup,
                功能名稱=s.FunctionName
            });
            //藉由權限編號，獲得登錄者的功能細項
            var keys = authGroup.Where(k => k.權限群組 == user_Permission).ToList();

            //將登錄者的功能細項(string)加到list中
            List<string> keysOfFunction = new List<string>();
            foreach (var key in keys) {
                keysOfFunction.Add(key.功能名稱);
            }

            return keysOfFunction;
        }

        
        public IActionResult logout()
        {
            string name = HttpContext.User.Identity.Name;
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            OnlineUsers.RemoveOnlineUser(name);
            TempData["statue"] = "2";


            return RedirectToAction("Index", "Home", TempData["statue"]);
        }

        public IActionResult andy_register()
        {
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> andy_register([Bind("name,password,confirmPassword")]andy_registerViewModel avm)
        {
            var s = _context.UserAccounts.Where(u => u.Account == avm.name).FirstOrDefault();
            
            if (s!=null) {
                ViewBag.errMSG = "已有此帳號，請填寫其他帳號名稱";
                return View(avm);
            }

            if (avm.password != avm.confirmPassword)
            {
                ViewBag.errMSG = "密碼錯誤，請確認密碼輸入是否一致";
                return View(avm);
            }
           
            if (ModelState.IsValid)
            {
                UserAccount userAccount = new UserAccount();
                userAccount.Account = avm.name;
                userAccount.Password = avm.confirmPassword;
                _context.Add(userAccount);
                await _context.SaveChangesAsync();

                List<string> user_roles = getKeysByAccountName(avm.name);
                var varClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name,avm.name),

                    };
                foreach (string role in user_roles)
                {
                    varClaims.Add(new Claim(ClaimTypes.Role, role));
                }
                OnlineUsers.AddOnlineUser(avm.name);

                var varClaimsIdentity = new ClaimsIdentity(varClaims, CookieAuthenticationDefaults.AuthenticationScheme);

                HttpContextAccessor httpContextAccessor = new HttpContextAccessor();

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(varClaimsIdentity));
                TempData["statue"] = "3";

                return RedirectToAction("Index", "Home", TempData["statue"]);
            }
            ViewBag.errMSG = "驗證失敗，請聯絡客服人員";
            return View(avm);
        }

       
    }
}
