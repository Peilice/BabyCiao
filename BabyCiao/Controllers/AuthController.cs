using System;
using System.Linq;
using System.Net.Security;
using System.Text.RegularExpressions;
using BabyCiao.Models;
using BabyCiao.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
		//GET: Auth/Index 
		public IActionResult Index()
		{

			//return View();
			var authDTOs = from ag in _context.AuthGroups
						   select new AuthDTO
						   {
							   GroupId = ag.GroupId,
							   GroupDescription = ag.GroupDescription,
							   ModifiedPersonUserAccount = ag.ModifiedPersonUserAccount,
							   ModifiedDate = ag.ModifiedDate,
							   ModifiedDateStringOld = ag.ModifiedDate.ToString("yyyy-MM-dd HH:mm"),

							   settings = (from fs in _context.FunctionSettings
										   where fs.GroupIdAuthGroup == ag.GroupId
										   select new FunctionSettingDTO
										   {
											   GroupId = fs.GroupIdAuthGroup,

											   FunctionId = fs.FunctionCodeSystemFunction,
											   FunctionName = fs.FunctionCodeSystemFunctionNavigation.FunctionName

										   }).ToList()
						   };

			return View(authDTOs);
		}

		//GET: Auth/Detail/{id}
		public IActionResult Detail(int GroupId)
		{
			var authDTO = (from ag in _context.AuthGroups
						   where ag.GroupId == GroupId
						   select new AuthDTO
						   {
							   GroupId = ag.GroupId,
							   GroupDescription = ag.GroupDescription,
							   ModifiedDate = ag.ModifiedDate,
							   ModifiedDateStringOld = ag.ModifiedDate.ToString("yyyy-MM-dd HH:mm"),
							   ModifiedPersonUserAccount = ag.ModifiedPersonUserAccount,
							   settings = (from fs in _context.FunctionSettings
										   where fs.GroupIdAuthGroup == ag.GroupId
										   select new FunctionSettingDTO
										   {
											   GroupId = fs.GroupIdAuthGroup,

											   FunctionId = fs.FunctionCodeSystemFunction,
											   FunctionName = fs.FunctionCodeSystemFunctionNavigation.FunctionName

										   }).ToList()

						   }).FirstOrDefault();

			if (authDTO == null)
			{
				return NotFound();
			}
			return View(authDTO);
		}

		//GET: Auth/Create
		public IActionResult Create()
		{

			var functions = (from sf in _context.SystemFunctions
							 select new FunctionSettingDTO
							 {

								 FunctionId = sf.FunctionId,
								 FunctionName = sf.FunctionName,
							 }).ToList();


            // 創建 AuthDTO 並設置 Settings 屬性
            var model = new AuthDTO
            {
                settings = functions,
                ModifiedDate = DateTime.Now,
                ModifiedDateStringOld = DateTime.Now.ToString("yyyy-MM-dd HH:mm")
            };

            // 傳遞模型給視圖
            return View(model);
            //ViewData["Function"] = functions;
            //ViewData["CreateTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            //return View();
		}

		//POST: Auth/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([FromForm]AuthDTO authDTO)
		{
			if (authDTO == null)
			{
				return NotFound();
			}
			if (ModelState.IsValid)
			{
				var authGroup = new AuthGroup
				{
					GroupId = authDTO.GroupId,
					GroupDescription = authDTO.GroupDescription,
					ModifiedPersonUserAccount = authDTO.ModifiedPersonUserAccount,
					ModifiedDate = DateTime.Now,
				};
				_context.Add(authGroup);

                await _context.SaveChangesAsync();
				var newId = authGroup.GroupId;

                for (int i = 0; i < authDTO.settings.Count(); i++)
				{
					if (authDTO.settings[i].IsExsited)
					{
						var newFuncSet = new FunctionSetting
						{
							GroupIdAuthGroup = newId,
							ModifiedDate = DateTime.Now,
							FunctionCodeSystemFunction = authDTO.settings[i].FunctionId
						};
						_context.Add(newFuncSet);

					}
				}
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(authDTO);
		}


		//GET: Auth/Edit/{id}
		[HttpGet]
		public async Task<IActionResult> Edit(int GroupId)
		{
			if (GroupId == null)
			{
				return NotFound();
			}
			var systemFunctions = _context.SystemFunctions.ToList(); // 這個就是全部的功能
			var functionSettings = _context.FunctionSettings.Where(fs => fs.GroupIdAuthGroup == GroupId).ToList();//有的功能

			//List<FunctionSettingDTO> functionSettingDTOs = new List<FunctionSettingDTO>(); this is too slow kiki
			var settings = (from sf in systemFunctions
							let fs = functionSettings.FirstOrDefault(f => f.FunctionCodeSystemFunction == sf.FunctionId)
							select new FunctionSettingDTO
							{
								IsExsited = fs != null,
								//有功能是true 沒有false
								FunctionId = sf.FunctionId,
								FunctionName = sf.FunctionName,
								GroupId = GroupId
							}).ToList();

			var authDTO = await (from ag in _context.AuthGroups
								 where ag.GroupId == GroupId
								 select new AuthDTO
								 {
									 GroupId = ag.GroupId,
									 GroupDescription = ag.GroupDescription,
									 ModifiedPersonUserAccount = ag.ModifiedPersonUserAccount,
									 ModifiedDateStringNew = DateTime.Now.ToString("yyyy-MM-dd HH:mm"),
									 ModifiedDateStringOld = ag.ModifiedDate.ToString("yyyy-MM-dd HH:mm"),
									 ModifiedDate = DateTime.Now,
									 settings = settings

								 }).FirstOrDefaultAsync(m => m.GroupId == GroupId);//

			if (authDTO == null)
			{
				return NotFound();
			}

			return View(authDTO);
		}

		//GET: Auth/Edit/{id}
		[HttpPost]
		[ValidateAntiForgeryToken]
		//public async Task<IActionResult> Edit(int GroupId, [Bind("GroupId,GroupDescription,ModifiedPersonUserAccount,ModifiedDate,settings")] AuthDTO authDTO)
		public async Task<IActionResult> Edit(int GroupId, [FromForm] AuthDTO authDTO)
		{

			if (authDTO == null)
			{
				return NotFound();
			}
			if (ModelState.IsValid)
			{
				var authGroup = new AuthGroup
				{
					GroupId = authDTO.GroupId,
					GroupDescription = authDTO.GroupDescription,
					ModifiedPersonUserAccount = authDTO.ModifiedPersonUserAccount,
					ModifiedDate = authDTO.ModifiedDate,
				};
				_context.Update(authGroup);
				var funcSet = await _context.FunctionSettings.Where(s => s.GroupIdAuthGroup == GroupId).ToListAsync();
				if (funcSet != null)
				{
					_context.FunctionSettings.RemoveRange(funcSet);
				}

				for (int i = 0; i < authDTO.settings.Count(); i++)
				{
					if (authDTO.settings[i].IsExsited)
					{
						var newFuncSet = new FunctionSetting
						{
							GroupIdAuthGroup = authDTO.GroupId,
							ModifiedDate = DateTime.Now,
							FunctionCodeSystemFunction = authDTO.settings[i].FunctionId
						};
						_context.Add(newFuncSet);

					}
				}
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(authDTO);
		}
        // GET: Auth/Delete/5
        public IActionResult Delete(int? GroupId)
        {
            var authDTO = (from ag in _context.AuthGroups
                           where ag.GroupId == GroupId
                           select new AuthDTO
                           {
                               GroupId = ag.GroupId,
                               GroupDescription = ag.GroupDescription,
                               ModifiedDate = ag.ModifiedDate,
                               ModifiedDateStringOld = ag.ModifiedDate.ToString("yyyy-MM-dd HH:mm"),
                               ModifiedPersonUserAccount = ag.ModifiedPersonUserAccount,
                               settings = (from fs in _context.FunctionSettings
                                           where fs.GroupIdAuthGroup == ag.GroupId
                                           select new FunctionSettingDTO
                                           {
                                               GroupId = fs.GroupIdAuthGroup,

                                               FunctionId = fs.FunctionCodeSystemFunction,
                                               FunctionName = fs.FunctionCodeSystemFunctionNavigation.FunctionName

                                           }).ToList()

                           }).FirstOrDefault();
			

            if (authDTO == null)
            {
                return NotFound();
            }
            return View(authDTO);
        }
        // POST: GroupBuy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int GroupId)
	{

            var authGroup = await _context.AuthGroups.Where(g => g.GroupId == GroupId).FirstAsync();
			if (authGroup != null)
			{
            _context.Remove(authGroup);
			}
            var funcSet = await _context.FunctionSettings.Where(s => s.GroupIdAuthGroup == GroupId).ToListAsync();
            if (funcSet != null)
            {
                _context.FunctionSettings.RemoveRange(funcSet);
            }
        
            await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}


}
}
