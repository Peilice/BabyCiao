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
    public class OnlineCompetitions1Controller : Controller
    {
        private readonly BabyCiaoContext _context;

        public OnlineCompetitions1Controller(BabyCiaoContext context)
        {
            _context = context;
        }

        // GET: OnlineCompetitions1
        public async Task<IActionResult> Index()
        {
            var babyCiaoContext = _context.OnlineCompetitions.Include(o => o.AccountUserAccountNavigation);
            return View(await babyCiaoContext.ToListAsync());
        }

        // GET: OnlineCompetitions1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var onlineCompetition = await _context.OnlineCompetitions
                .Include(o => o.AccountUserAccountNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (onlineCompetition == null)
            {
                return NotFound();
            }

            return View(onlineCompetition);
        }

        // GET: OnlineCompetitions1/Create
        public IActionResult Create()
        {
            ViewData["AccountUserAccount"] = new SelectList(_context.UserAccounts, "Account", "Account");
            return View();
        }

        // POST: OnlineCompetitions1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CompetitionName,AccountUserAccount,StartTime,EndTime,Content,ModifiedTime,Statement")] OnlineCompetition onlineCompetition)
        {
            //if (ModelState.IsValid)
            //{
            //    _context.Add(onlineCompetition);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            if (ModelState.IsValid != true)
            {
                if (onlineCompetition.AccountUserAccountNavigation == null)
                {
                    ViewData["AccountUserAccount"] = onlineCompetition.AccountUserAccountNavigation;
                    _context.Add(onlineCompetition);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                _context.Add(onlineCompetition);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["AccountUserAccount"] = new SelectList(_context.UserAccounts, "Account", "Account", onlineCompetition.AccountUserAccount);
            return View(onlineCompetition);
        }

        // GET: OnlineCompetitions1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var onlineCompetition = await _context.OnlineCompetitions.FindAsync(id);
            if (onlineCompetition == null)
            {
                return NotFound();
            }
            ViewData["AccountUserAccount"] = new SelectList(_context.UserAccounts, "Account", "Account", onlineCompetition.AccountUserAccount);
            return View(onlineCompetition);
        }

        // POST: OnlineCompetitions1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CompetitionName,AccountUserAccount,StartTime,EndTime,Content,ModifiedTime,Statement")] OnlineCompetition onlineCompetition)
        {
            if (id != onlineCompetition.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(onlineCompetition);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OnlineCompetitionExists(onlineCompetition.Id))
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
            ViewData["AccountUserAccount"] = new SelectList(_context.UserAccounts, "Account", "Account", onlineCompetition.AccountUserAccount);
            return View(onlineCompetition);
        }

        // GET: OnlineCompetitions1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var onlineCompetition = await _context.OnlineCompetitions
                .Include(o => o.AccountUserAccountNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (onlineCompetition == null)
            {
                return NotFound();
            }

            return View(onlineCompetition);
        }

        // POST: OnlineCompetitions1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var onlineCompetition = await _context.OnlineCompetitions.FindAsync(id);
            if (onlineCompetition != null)
            {
                _context.OnlineCompetitions.Remove(onlineCompetition);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OnlineCompetitionExists(int id)
        {
            return _context.OnlineCompetitions.Any(e => e.Id == id);
        }
    }
}
