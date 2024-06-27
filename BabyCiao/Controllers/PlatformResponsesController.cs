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
    public class PlatformResponsesController : Controller
    {
        private readonly BabyCiaoContext _context;

        public PlatformResponsesController(BabyCiaoContext context)
        {
            _context = context;
        }

        // GET: PlatformResponses
        public async Task<IActionResult> Index()
        {
            var babyCiaoContext = _context.PlatformResponses.Include(p => p.IdPlatformNavigation);
            return View(await babyCiaoContext.ToListAsync());
        }

        // GET: PlatformResponses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var platformResponse = await _context.PlatformResponses
                .Include(p => p.IdPlatformNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (platformResponse == null)
            {
                return NotFound();
            }

            return View(platformResponse);
        }

        // GET: PlatformResponses/Create
        public IActionResult Create()
        {
            ViewData["IdPlatform"] = new SelectList(_context.Platforms, "Id", "Id");
            return View();
        }

        // POST: PlatformResponses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdPlatform,AccountUserAccount,Content,ModifiedTime,Display")] PlatformResponse platformResponse)
        {

            if (ModelState.IsValid)
            {
                _context.Add(platformResponse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPlatform"] = new SelectList(_context.Platforms, "Id", "Id", platformResponse.IdPlatform);
            return View(platformResponse);
        }

        // GET: PlatformResponses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var platformResponse = await _context.PlatformResponses.FindAsync(id);
            if (platformResponse == null)
            {
                return NotFound();
            }
            ViewData["IdPlatform"] = new SelectList(_context.Platforms, "Id", "Id", platformResponse.IdPlatform);
            return View(platformResponse);
        }

        // POST: PlatformResponses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdPlatform,AccountUserAccount,Content,ModifiedTime,Display")] PlatformResponse platformResponse)
        {
            if (id != platformResponse.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(platformResponse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlatformResponseExists(platformResponse.Id))
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
            ViewData["IdPlatform"] = new SelectList(_context.Platforms, "Id", "Id", platformResponse.IdPlatform);
            return View(platformResponse);
        }

        // GET: PlatformResponses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var platformResponse = await _context.PlatformResponses
                .Include(p => p.IdPlatformNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (platformResponse == null)
            {
                return NotFound();
            }

            return View(platformResponse);
        }

        // POST: PlatformResponses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var platformResponse = await _context.PlatformResponses.FindAsync(id);
            if (platformResponse != null)
            {
                _context.PlatformResponses.Remove(platformResponse);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlatformResponseExists(int id)
        {
            return _context.PlatformResponses.Any(e => e.Id == id);
        }
    }
}
