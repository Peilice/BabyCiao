using BabyCiao.Models;
using BabyCiao.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using NuGet.Configuration;

namespace BabyCiao.Controllers
{
    public class AuthController : Controller
    {
        private readonly BabyCiaoContext _context;
        public AuthController(BabyCiaoContext context)
        {
            _context = context;
        }
        public IActionResult Index()
		{
            //return View();
            var authDTOs = from pg in _context.AuthGroups
                           select new AuthDTO
                           {
                               GroupCode = pg.GroupId,
                               GroupDescription = pg.GroupDescription,
                               ModifiedPersonUserAccount = pg.ModifiedPersonUserAccount,
                               ModifiedDate = pg.ModifiedDate,
                               settings = (from fs in _context.FunctionSettings
                                           where fs.GroupIdAuthGroup == pg.GroupId
                                           select new FunctionSettingDTO
                                           {
                                               FunctionCode = fs.FunctionCodeSystemFunction,
                                               FunctionName = fs.FunctionCodeSystemFunctionNavigation.FunctionName,
                                               GroupCode = fs.GroupIdAuthGroup
                                           }).ToList()
                           };

            return View(authDTOs);
        }
    }
}
