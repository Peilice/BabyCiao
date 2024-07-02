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
    public class BabyResumesController : Controller
    {
        private readonly BabyCiaoContext _context;

        public BabyResumesController(BabyCiaoContext context)
        {
            _context = context;
        }

        // GET: BabyResumes
        public async Task<IActionResult> Index()
        {
            var babyCiaoContext = _context.BabyResumes.Include(b => b.AccountUserAccountNavigation);
            return View(await babyCiaoContext.ToListAsync());
        }

        // GET: BabyResumes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var babyResume = await _context.BabyResumes
                .Include(b => b.AccountUserAccountNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (babyResume == null)
            {
                return NotFound();
            }

            return View(babyResume);
        }

        // GET: BabyResumes/Create
        public IActionResult Create()
        {
            ViewData["AccountUserAccount"] = new SelectList(_context.UserAccounts, "Account", "Account");
            return View();
        }

        // POST: BabyResumes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AccountUserAccount,Photo,FirstName,City,District,ApplyDate,RequireDate,BabyBirthday,TypeOfDaycare,TimeSlot,Memo,Display")] BabyResume babyResume)
        {
            if (ModelState.IsValid)
            {
                _context.Add(babyResume);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountUserAccount"] = new SelectList(_context.UserAccounts, "Account", "Account", babyResume.AccountUserAccount);
            return View(babyResume);
        }

        // GET: BabyResumes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var babyResume = await _context.BabyResumes.FindAsync(id);
            if (babyResume == null)
            {
                return NotFound();
            }
            ViewData["AccountUserAccount"] = new SelectList(_context.UserAccounts, "Account", "Account", babyResume.AccountUserAccount);
            return View(babyResume);
        }

        // POST: BabyResumes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AccountUserAccount,Photo,FirstName,City,District,ApplyDate,RequireDate,BabyBirthday,TypeOfDaycare,TimeSlot,Memo,Display")] BabyResume babyResume)
        {
            if (id != babyResume.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(babyResume);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BabyResumeExists(babyResume.Id))
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
            ViewData["AccountUserAccount"] = new SelectList(_context.UserAccounts, "Account", "Account", babyResume.AccountUserAccount);
            return View(babyResume);
        }

        // GET: BabyResumes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var babyResume = await _context.BabyResumes
                .Include(b => b.AccountUserAccountNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (babyResume == null)
            {
                return NotFound();
            }

            return View(babyResume);
        }

        // POST: BabyResumes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var babyResume = await _context.BabyResumes.FindAsync(id);
            if (babyResume != null)
            {
                _context.BabyResumes.Remove(babyResume);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BabyResumeExists(int id)
        {
            return _context.BabyResumes.Any(e => e.Id == id);
        }
    }
}
