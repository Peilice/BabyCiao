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
    public class NannyRequirmentsController : Controller
    {
        private readonly BabyCiaoContext _context;

        public NannyRequirmentsController(BabyCiaoContext context)
        {
            _context = context;
        }

        // GET: NannyRequirments
        public async Task<IActionResult> Index()
        {
            var babyCiaoContext = _context.NannyRequirments.Include(n => n.NannyAccountUserAccountNavigation);
            return View(await babyCiaoContext.ToListAsync());
        }

        // GET: NannyRequirments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nannyRequirment = await _context.NannyRequirments
                .Include(n => n.NannyAccountUserAccountNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nannyRequirment == null)
            {
                return NotFound();
            }

            return View(nannyRequirment);
        }

        // GET: NannyRequirments/Create
        public IActionResult Create()
        {
            ViewData["NannyAccountUserAccount"] = new SelectList(_context.UserAccounts, "Account", "Account");
            return View();
        }

        // POST: NannyRequirments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RequirementDate,NannyAccountUserAccount,PoliceCriminalRecordCertificate,ChildCareCertificate,NationalIdentificationCard,AddressesOfAgencies,ValidPeriodsOfCertificates,Statement")] NannyRequirment nannyRequirment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nannyRequirment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NannyAccountUserAccount"] = new SelectList(_context.UserAccounts, "Account", "Account", nannyRequirment.NannyAccountUserAccount);
            return View(nannyRequirment);
        }

        // GET: NannyRequirments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nannyRequirment = await _context.NannyRequirments.FindAsync(id);
            if (nannyRequirment == null)
            {
                return NotFound();
            }
            ViewData["NannyAccountUserAccount"] = new SelectList(_context.UserAccounts, "Account", "Account", nannyRequirment.NannyAccountUserAccount);
            return View(nannyRequirment);
        }

        // POST: NannyRequirments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RequirementDate,NannyAccountUserAccount,PoliceCriminalRecordCertificate,ChildCareCertificate,NationalIdentificationCard,AddressesOfAgencies,ValidPeriodsOfCertificates,Statement")] NannyRequirment nannyRequirment)
        {
            if (id != nannyRequirment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nannyRequirment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NannyRequirmentExists(nannyRequirment.Id))
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
            ViewData["NannyAccountUserAccount"] = new SelectList(_context.UserAccounts, "Account", "Account", nannyRequirment.NannyAccountUserAccount);
            return View(nannyRequirment);
        }

        // GET: NannyRequirments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nannyRequirment = await _context.NannyRequirments
                .Include(n => n.NannyAccountUserAccountNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nannyRequirment == null)
            {
                return NotFound();
            }

            return View(nannyRequirment);
        }

        // POST: NannyRequirments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nannyRequirment = await _context.NannyRequirments.FindAsync(id);
            if (nannyRequirment != null)
            {
                _context.NannyRequirments.Remove(nannyRequirment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NannyRequirmentExists(int id)
        {
            return _context.NannyRequirments.Any(e => e.Id == id);
        }
    }
}
