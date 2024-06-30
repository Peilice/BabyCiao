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
            
            var platformDTO= (from pf in _context.Platforms
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
                    
            }).FirstOrDefault();

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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AccountUserAccount,ModifiedTime,Title,Content,Type,Display")] PlatformsDTO platformDTO)
        {

            //if (ModelState.IsValid)
            //{
            //    _context.Add(platform);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}

            if (ModelState.IsValid != true)
            {
                if (platformDTO.PlatformAccountUserAccount == null)
                {
                    ViewData["AccountUserAccount"] = platformDTO.PlatformAccountUserAccount;
                    _context.Add(platformDTO);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                _context.Add(platformDTO);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountUserAccount"] = new SelectList(_context.UserAccounts, "Account", "Account", platformDTO.PlatformAccountUserAccount);
            return View(platformDTO);
        }

        // GET: Platforms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var platformDTO = (from pf in _context.Platforms
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
            //ViewData["AccountUserAccount"] = new SelectList(_context.UserAccounts, "Account", "Account", platform.AccountUserAccount);
            return View(platformDTO);
        }

        // POST: Platforms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AccountUserAccount,ModifiedTime,Title,Content,Type,Display")] PlatformsDTO platformDTO)
        {
            if (id != platformDTO.PlatformId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(platformDTO);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlatformExists(platformDTO.PlatformId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountUserAccount"] = new SelectList(_context.UserAccounts, "Account", "Account", platformDTO.PlatformAccountUserAccount);
            return View(platformDTO);
        }

        // GET: Platforms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var platform = await _context.Platforms
                .Include(p => p.AccountUserAccountNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (platform == null)
            {
                return NotFound();
            }

            return View(platform);
        }

        // POST: Platforms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var platform = await _context.Platforms.FindAsync(id);
            if (platform != null)
            {
                _context.Platforms.Remove(platform);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlatformExists(int id)
        {
            return _context.Platforms.Any(e => e.Id == id);
        }
    }
}
