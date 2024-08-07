using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BabyCiao.Models;
using BabyCiao.Models.DTO;
using Microsoft.CodeAnalysis;

namespace BabyCiao.Controllers
{
    public class PlatformsController : Controller
    {
        private readonly BabyciaoContext _context;

        public PlatformsController(BabyciaoContext context)
        {
            _context = context;
        }

        // GET: Platforms
        public async Task<IActionResult> Index()
        {
            var babyCiaoContext = _context.Platforms.Include(p => p.AccountUserAccountNavigation);
            return View(await babyCiaoContext.ToListAsync());
        }

        // GET: Platforms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var platformDTO=await (from pf in _context.Platforms
                            where pf.Id == id
                             select new PlatformsDTO
                            {
                                 PlatformId=pf.Id,
                                 PlatformAccountUserAccount = pf.AccountUserAccount,
                                 PlatformModifiedTime=pf.ModifiedTime,
                                 PlatformTitle=pf.Title,
                                 PlatformContent=pf.Content,
                                 PlatformType=pf.Type,
                                 PlatformDisplay=pf.Display,
                                 Responses = (from pr in _context.PlatformResponses
                                              where pr.IdPlatform == pf.Id
                                              select new PlatformsDTO.Response
                                              {   
                                                ResponseModifiedTime = pr.ModifiedTime,
                                                ResponseContent = pr.Content,
                                                ResponseDisplay = pr.Display,
                                                ResponseAccountUserAccount = pr.AccountUserAccount,
                                              }).ToList()
                    
                             }).FirstOrDefaultAsync();

            if (platformDTO == null)
            {
                return NotFound();
            }

            return View(platformDTO);
        }

        // GET: Platforms/Create
        public IActionResult Create()
        {
            ViewData["AccountUserAccount"] = new SelectList(_context.UserAccounts, "Account", "Account");
            return View();
        }

        // POST: Platforms/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] PlatformsDTO platformDTO)
        {
            if(platformDTO == null)
            {
                return NotFound();
            }

            var newPlatform = new Models.Platform
            {
                AccountUserAccount = platformDTO.PlatformAccountUserAccount,
                Title = platformDTO.PlatformTitle,
                Content = platformDTO.PlatformContent,
                Type = platformDTO.PlatformType,
            };
            _context.Add(newPlatform);
            await _context.SaveChangesAsync();
            ViewData["AccountUserAccount"] = new SelectList(_context.UserAccounts, "Account", "Account");
            return RedirectToAction(nameof(Index));
        }

        // GET: Platforms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var platformDTO = (from pf in _context.Platforms
                               where pf.Id == id
                               select new PlatformsDTO
                               {
                                   PlatformId = pf.Id,
                                   PlatformAccountUserAccount = pf.AccountUserAccount,
                                   PlatformModifiedTime = pf.ModifiedTime,
                                   PlatformTitle = pf.Title,
                                   PlatformContent = pf.Content,
                                   PlatformType = pf.Type,
                                   PlatformDisplay = pf.Display,
                                   Responses = (from pr in _context.PlatformResponses
                                                where pr.IdPlatform == pf.Id
                                                select new PlatformsDTO.Response
                                                {
                                                    ResponseModifiedTime = pr.ModifiedTime,
                                                    ResponseContent = pr.Content,
                                                    ResponseDisplay = pr.Display,
                                                    ResponseAccountUserAccount = pr.AccountUserAccount,
                                                }).ToList()

                               }).FirstOrDefault();

            //var platform = await _context.Platforms.FindAsync(id);
            if (platformDTO == null)
            {
                return NotFound();
            }
            ViewData["AccountUserAccount"] = new SelectList(_context.UserAccounts, "Account", "Account", platformDTO.PlatformAccountUserAccount);
            return View(platformDTO);
        }

        // POST: Platforms/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] PlatformsDTO platformDTO)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var editPlatform = await _context.Platforms.FindAsync(id);
            if (editPlatform == null)
            {
                return NotFound();
            }

            editPlatform.AccountUserAccount = platformDTO.PlatformAccountUserAccount;
            editPlatform.ModifiedTime = DateOnly.FromDateTime(DateTime.Now);
            editPlatform.Title= platformDTO.PlatformTitle;
            editPlatform.Content=platformDTO.PlatformContent;
            editPlatform.Type = platformDTO.PlatformType;
            editPlatform.Display = platformDTO.PlatformDisplay;
            _context.Update(editPlatform);
            await _context.SaveChangesAsync();

            //論壇回應修改
            var editResponse = await _context.PlatformResponses.FirstOrDefaultAsync(x => x.IdPlatform == id);
            editResponse.ModifiedTime = DateTime.Now;
            
            if (editResponse == null)
            {
                return NotFound();
            }


            ViewData["AccountUserAccount"] = new SelectList(_context.UserAccounts, "Account", "Account", platformDTO.PlatformAccountUserAccount);
            return RedirectToAction(nameof(Index));
        }

        // GET: Platforms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var platformDTO = await (from pf in _context.Platforms
                                     where pf.Id == id
                                     select new PlatformsDTO
                                     {
                                         PlatformId = pf.Id,
                                         PlatformAccountUserAccount = pf.AccountUserAccount,
                                         PlatformModifiedTime = pf.ModifiedTime,
                                         PlatformTitle = pf.Title,
                                         PlatformContent = pf.Content,
                                         PlatformType = pf.Type,
                                         PlatformDisplay = pf.Display,
                                         Responses = (from pr in _context.PlatformResponses
                                                      where pr.IdPlatform == pf.Id
                                                      select new PlatformsDTO.Response
                                                      {
                                                          ResponseModifiedTime = pr.ModifiedTime,
                                                          ResponseContent = pr.Content,
                                                          ResponseDisplay = pr.Display,
                                                          ResponseAccountUserAccount = pr.AccountUserAccount,
                                                      }).ToList()

                                     }).FirstOrDefaultAsync();

            if (platformDTO == null)
            {
                return NotFound();
            }

            return View(platformDTO);
        }

        // POST: Platforms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, [FromForm] PlatformsDTO platformDTO)
        {
            var platform = await _context.Platforms.FindAsync(id);
            if (platform != null)
            {
                _context.Platforms.Remove(platform);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //private bool PlatformExists(int id)
        //{
        //    return _context.Platforms.Any(e => e.Id == id);
        //}
    }
}
