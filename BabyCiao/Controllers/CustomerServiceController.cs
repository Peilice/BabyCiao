using Microsoft.AspNetCore.Mvc;

namespace BabyCiao.Controllers
{
    public class CustomerServiceController : Controller
    {
        public IActionResult CustomerService()
        {
            return View();
        }
    }
}
