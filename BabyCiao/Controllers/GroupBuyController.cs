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
    public class GroupBuyController : Controller
    {
        private readonly BabyCiaoContext _context;

        public GroupBuyController(BabyCiaoContext context)
        {
            _context = context;
        }
        // GET: Products/IndexJson
        public async Task<IActionResult> IndexJson()
        //public async Task<JsonResult> IndexJson()
        {
            var groupBuys = from gb in _context.GroupBuyings
                            select new GroupBuyDTO
                            {
                                Id = gb.Id,
                                UserAccount = gb.AccountUserAccount,
                                ProductName = gb.ProductName,
                                ProductDescription = gb.ProductDescription,
                                TargetCount = gb.TargetCount,
                                Statement = gb.Statement,
                                ModifiedTime = gb.ModifiedTime,
                                ModifiedTimeView = gb.ModifiedTime.ToString("yyyy-MM-dd"),
                                Display = gb.Display,
                                //JoinQuantity=from gbd in _context.GroupBuyingDetails.Where(id=>id.).Sum(gbd => gbd.Id)

                            };
            return Json(groupBuys);
        }
    // GET: GroupBuy


    public async Task<IActionResult> Index()
        {
            var groupBuys = from gb in _context.GroupBuyings
                            select new GroupBuyDTO
                            {
                                Id = gb.Id,
                                UserAccount = gb.AccountUserAccount,
                                ProductName = gb.ProductName,
                                ProductDescription = gb.ProductDescription,
                                TargetCount = gb.TargetCount,
                                Statement = gb.Statement,
                                ModifiedTime = gb.ModifiedTime,
                                ModifiedTimeView = gb.ModifiedTime.ToString("yyyy-MM-dd"),
                                Display = gb.Display,
                                //JoinQuantity=from gbd in _context.GroupBuyingDetails.Where(id=>id.).Sum(gbd => gbd.Id)

                            };
            //return View(await groupBuys.ToListAsync());
            return View(groupBuys);
        }

        // GET: GroupBuy/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupBuying = await _context.GroupBuyings
                .Include(g => g.AccountUserAccountNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (groupBuying == null)
            {
                return NotFound();
            }

            return View(groupBuying);
        }

        // GET: GroupBuy/Create
        public IActionResult Create()
        {
            ViewData["AccountUserAccount"] = new SelectList(_context.UserAccounts, "Account", "Account");
            return View();
        }

        // POST: GroupBuy/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AccountUserAccount,ProductName,ProductDescription,TargetCount,Statement,ModifiedTime,Display")] GroupBuying groupBuying)
        {
            if (ModelState.IsValid)
            {
                _context.Add(groupBuying);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountUserAccount"] = new SelectList(_context.UserAccounts, "Account", "Account", groupBuying.AccountUserAccount);
            return View(groupBuying);
        }

        // GET: GroupBuy/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupBuying = await _context.GroupBuyings.FindAsync(id);
            if (groupBuying == null)
            {
                return NotFound();
            }
            ViewData["AccountUserAccount"] = new SelectList(_context.UserAccounts, "Account", "Account", groupBuying.AccountUserAccount);
            return View(groupBuying);
        }

        // POST: GroupBuy/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AccountUserAccount,ProductName,ProductDescription,TargetCount,Statement,ModifiedTime,Display")] GroupBuying groupBuying)
        {
            if (id != groupBuying.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(groupBuying);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupBuyingExists(groupBuying.Id))
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
            ViewData["AccountUserAccount"] = new SelectList(_context.UserAccounts, "Account", "Account", groupBuying.AccountUserAccount);
            return View(groupBuying);
        }

        // GET: GroupBuy/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var groupBuying = await _context.GroupBuyings
                .Include(g => g.AccountUserAccountNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (groupBuying == null)
            {
                return NotFound();
            }

            return View(groupBuying);
        }

        // POST: GroupBuy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var groupBuying = await _context.GroupBuyings.FindAsync(id);
            if (groupBuying != null)
            {
                _context.GroupBuyings.Remove(groupBuying);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GroupBuyingExists(int id)
        {
            return _context.GroupBuyings.Any(e => e.Id == id);
        }
    }
}
