using BabyCiao.Models;
using BabyCiao.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using NuGet.Configuration;

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
            //return View();
            var authDTOs = from pg in _context.PermissionGroups
                           select new AuthDTO
                           {
                               GroupCode = pg.GroupCode,
                               GroupDescription = pg.GroupDescription,
                               ModifiedPersonUserAccount = pg.ModifiedPersonUserAccount,
                               ModifiedDate = pg.ModifiedDate,
                               settings = (from fs in _context.FunctionSettings
                                           where fs.GroupCodePermissionGroup == pg.GroupCode
                                           select new FunctionSettingDTO
                                           {
                                               FunctionCode = fs.FunctionCodeSystemFunction,
                                               FunctionName = fs.FunctionCodeSystemFunctionNavigation.FunctionName,
                                               GroupCode = fs.GroupCodePermissionGroup
                                           }).ToList()
                           };

            return View(authDTOs);
        }
    }
}
