using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace CoreMVCCategoryProduct.Controllers
{
    public class BaseController : Controller
    {
        public ActionResult SetLanguage(string CultureName, string ReturnUrl)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(CultureName)),
                new CookieOptions { Expires=DateTime.Now.AddYears(30)});
            return LocalRedirect(ReturnUrl);
        }
    }
}