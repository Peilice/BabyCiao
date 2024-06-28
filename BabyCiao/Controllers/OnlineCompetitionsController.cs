﻿using System;
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
    public class OnlineCompetitionsController : Controller
    {
        private readonly BabyCiaoContext _context;

        public OnlineCompetitionsController(BabyCiaoContext context)
        {
            _context = context;
        }

        // GET: OnlineCompetitions
        public async Task<IActionResult> Index()
        {
            //var babyCiaoContext = _context.OnlineCompetitions.Include(o => o.AccountUserAccountNavigation);
            //return View(await babyCiaoContext.ToListAsync());
            var babyCiaoContext = _context.OnlineCompetitions;
            return View(await babyCiaoContext.ToListAsync());
        }

        // GET: OnlineCompetitions/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var competitionDTO = from con in _context.OnlineCompetitions
                                 join cp in _context.CompetitionPhotos on con.Id equals cp.IdOnlineCompetition
                                 select new OnlineCompetitionsDTO
                                 {
                                     Id = con.Id,
                                     CompetitionName = con.CompetitionName,
                                     AccountUserAccount = con.AccountUserAccount,
                                     StartTime = con.StartTime,
                                     EndTime = con.EndTime,
                                     Content = con.Content,
                                     Statement = con.Statement,
                                     ModifiedTime = con.ModifiedTime,
                                     PhotoName = cp.PhotoName,

                                 };

            //var onlineCompetition = await _context.OnlineCompetitions
            //    .Include(o => o.AccountUserAccountNavigation)
            //    .FirstOrDefaultAsync(m => m.Id == id);
            if (competitionDTO == null)
            //if (ModelState.IsValid)
            {
                return NotFound();
            }

            return View(competitionDTO);
        }

        // GET: OnlineCompetitions/Create
        public IActionResult Create()
        {
            ViewData["AccountUserAccount"] = new SelectList(_context.UserAccounts, "Account", "Account");
            return View();
        }

        // POST: OnlineCompetitions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CompetitionName,AccountUserAccount,StartTime,EndTime,Content,ModifiedTime,Statement")] OnlineCompetition onlineCompetition)
           
        {
            onlineCompetition.AccountUserAccountNavigation = _context.UserAccounts.FirstOrDefault(user=>user.Account==onlineCompetition.AccountUserAccount);
           
            if (ModelState.IsValid)
            {

                _context.Add(onlineCompetition);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //else
            //{
            //    var errors = ModelState.Values.SelectMany(v => v.Errors);
            //    foreach (var error in errors)
            //    {
            //        Console.WriteLine($"錯誤訊息：{error.ErrorMessage}");
            //    }
            //}
            ViewData["AccountUserAccount"] = new SelectList(_context.UserAccounts, "Account", "Account", onlineCompetition.AccountUserAccount);
            return View(onlineCompetition);
        }

        // GET: OnlineCompetitions/Edit/5
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

        // POST: OnlineCompetitions/Edit/5
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

        // GET: OnlineCompetitions/Delete/5
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

        // POST: OnlineCompetitions/Delete/5
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
