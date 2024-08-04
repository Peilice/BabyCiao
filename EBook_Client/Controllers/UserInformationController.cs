using Microsoft.AspNetCore.Mvc;

namespace BabyCiao_Client.Controllers
{
    public class UserInformationController : Controller
    {
        public IActionResult UserInformation()
        {
            return View();
        }
    }
}
