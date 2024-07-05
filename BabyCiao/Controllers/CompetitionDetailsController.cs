using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BabyCiao.Models;
using BabyCiao.Models.DTO;

namespace BabyCiao.Controllers
{
    public class CompetitionDetailsController : Controller
    {
        private readonly BabyciaoContext _context;

        public CompetitionDetailsController(BabyciaoContext context)
        {
            _context = context;
        }

        // GET: CompetitionDetails
        public async Task<IActionResult> Index()
        {
            var babyciaoContext = _context.CompetitionDetails.Include(c => c.IdOnlineCompetitionNavigation);
            return View(await babyciaoContext.ToListAsync());
        }

        // GET: CompetitionDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competitionDetail = await _context.CompetitionDetails
                .Include(c => c.IdOnlineCompetitionNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (competitionDetail == null)
            {
                return NotFound();
            }

            return View(competitionDetail);
        }

        // GET: CompetitionDetails/Create
        public IActionResult Create()
        {
            ViewData["IdOnlineCompetition"] = new SelectList(_context.OnlineCompetitions, "Id", "Id");
            return View();
        }

        // POST: CompetitionDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdOnlineCompetition,AccountUserAccount,CompetitionPhoto,Content,ModifiedTime")] CompetitionDetail competitionDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(competitionDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdOnlineCompetition"] = new SelectList(_context.OnlineCompetitions, "Id", "Id", competitionDetail.IdOnlineCompetition);
            return View(competitionDetail);
        }

        // GET: CompetitionDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competitionDetail = await _context.CompetitionDetails.FindAsync(id);
            if (competitionDetail == null)
            {
                return NotFound();
            }
            ViewData["IdOnlineCompetition"] = new SelectList(_context.OnlineCompetitions, "Id", "Id", competitionDetail.IdOnlineCompetition);
            return View(competitionDetail);
        }

        // POST: CompetitionDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdOnlineCompetition,AccountUserAccount,CompetitionPhoto,Content,ModifiedTime")] CompetitionDetail competitionDetail)
        {
            if (id != competitionDetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(competitionDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompetitionDetailExists(competitionDetail.Id))
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
            ViewData["IdOnlineCompetition"] = new SelectList(_context.OnlineCompetitions, "Id", "Id", competitionDetail.IdOnlineCompetition);
            return View(competitionDetail);
        }

        // GET: CompetitionDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competitionDetail = await _context.CompetitionDetails
                .Include(c => c.IdOnlineCompetitionNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (competitionDetail == null)
            {
                return NotFound();
            }

            return View(competitionDetail);
        }

        // POST: CompetitionDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var competitionDetail = await _context.CompetitionDetails.FindAsync(id);
            if (competitionDetail != null)
            {
                _context.CompetitionDetails.Remove(competitionDetail);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompetitionDetailExists(int id)
        {
            return _context.CompetitionDetails.Any(e => e.Id == id);
        }
    }
}
