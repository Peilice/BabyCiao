using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BabyCiao.Models;

namespace BabyCiao.Controllers
{
    public class PlatformsController : Controller
    {
        private readonly BabyCiaoContext _context;

        public PlatformsController(BabyCiaoContext context)
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

            var platform = await _context.Platforms
                .Include(p => p.AccountUserAccountNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (platform == null)
            {
                return NotFound();
            }

            return View(platform);
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
        public async Task<IActionResult> Create([Bind("Id,AccountUserAccount,ModifiedTime,Title,Content,Type,Display")] Platform platform)
        {
            if (ModelState.IsValid)
            {
                _context.Add(platform);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountUserAccount"] = new SelectList(_context.UserAccounts, "Account", "Account", platform.AccountUserAccount);
            return View(platform);
        }

        // GET: Platforms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var platform = await _context.Platforms.FindAsync(id);
            if (platform == null)
            {
                return NotFound();
            }
            ViewData["AccountUserAccount"] = new SelectList(_context.UserAccounts, "Account", "Account", platform.AccountUserAccount);
            return View(platform);
        }

        // POST: Platforms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AccountUserAccount,ModifiedTime,Title,Content,Type,Display")] Platform platform)
        {
            if (id != platform.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(platform);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlatformExists(platform.Id))
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
            ViewData["AccountUserAccount"] = new SelectList(_context.UserAccounts, "Account", "Account", platform.AccountUserAccount);
            return View(platform);
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
