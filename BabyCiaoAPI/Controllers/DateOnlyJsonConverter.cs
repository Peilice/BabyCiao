using Microsoft.AspNetCore.Mvc;

namespace BabyCiaoAPI.Controllers
{
    public class DateOnlyJsonConverter : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
