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
    public class EvaluatesController : Controller
    {
        private readonly BabyciaoContext _context;

        public EvaluatesController(BabyciaoContext context)
        {
            _context = context;
        }

        // GET: Evaluates
        public async Task<IActionResult> Index()
        {
            var babyciaoContext = _context.Evaluates.Include(e => e.AppraiseeUserAccountNavigation).Include(e => e.EvaluatorUserAccountNavigation);
            return View(await babyciaoContext.ToListAsync());
        }

        // GET: Evaluates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evaluate = await _context.Evaluates
                .Include(e => e.AppraiseeUserAccountNavigation)
                .Include(e => e.EvaluatorUserAccountNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evaluate == null)
            {
                return NotFound();
            }

            return View(evaluate);
        }

        // GET: Evaluates/Create
        public IActionResult Create()
        {
            ViewData["AppraiseeUserAccount"] = new SelectList(_context.UserAccounts, "Account", "Account");
            ViewData["EvaluatorUserAccount"] = new SelectList(_context.UserAccounts, "Account", "Account");
            return View();
        }

        // POST: Evaluates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EvaluatorUserAccount,AppraiseeUserAccount,EvaluateTime,Score,Memo,Display")] Evaluate evaluate)
        {
            if (ModelState.IsValid)
            {
                _context.Add(evaluate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppraiseeUserAccount"] = new SelectList(_context.UserAccounts, "Account", "Account", evaluate.AppraiseeUserAccount);
            ViewData["EvaluatorUserAccount"] = new SelectList(_context.UserAccounts, "Account", "Account", evaluate.EvaluatorUserAccount);
            return View(evaluate);
        }

        // GET: Evaluates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evaluate = await _context.Evaluates.FindAsync(id);
            if (evaluate == null)
            {
                return NotFound();
            }
            ViewData["AppraiseeUserAccount"] = new SelectList(_context.UserAccounts, "Account", "Account", evaluate.AppraiseeUserAccount);
            ViewData["EvaluatorUserAccount"] = new SelectList(_context.UserAccounts, "Account", "Account", evaluate.EvaluatorUserAccount);
            return View(evaluate);
        }

        // POST: Evaluates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EvaluatorUserAccount,AppraiseeUserAccount,EvaluateTime,Score,Memo,Display")] Evaluate evaluate)
        {
            if (id != evaluate.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(evaluate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EvaluateExists(evaluate.Id))
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
            ViewData["AppraiseeUserAccount"] = new SelectList(_context.UserAccounts, "Account", "Account", evaluate.AppraiseeUserAccount);
            ViewData["EvaluatorUserAccount"] = new SelectList(_context.UserAccounts, "Account", "Account", evaluate.EvaluatorUserAccount);
            return View(evaluate);
        }

        // GET: Evaluates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evaluate = await _context.Evaluates
                .Include(e => e.AppraiseeUserAccountNavigation)
                .Include(e => e.EvaluatorUserAccountNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evaluate == null)
            {
                return NotFound();
            }

            return View(evaluate);
        }

        // POST: Evaluates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var evaluate = await _context.Evaluates.FindAsync(id);
            if (evaluate != null)
            {
                _context.Evaluates.Remove(evaluate);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EvaluateExists(int id)
        {
            return _context.Evaluates.Any(e => e.Id == id);
        }
    }
}
