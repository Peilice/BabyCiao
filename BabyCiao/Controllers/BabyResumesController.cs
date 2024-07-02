using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BabyCiao.Models;
using Microsoft.AspNetCore.Hosting;

namespace BabyCiao.Controllers
{
    public class BabyResumesController : Controller
    {
        private readonly BabyCiaoContext _context;
        private readonly string _imagePath;

        public BabyResumesController(BabyCiaoContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _imagePath = Path.Combine(webHostEnvironment.WebRootPath, "uploads");
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
            ViewData["DaycareTypes"] = new SelectList(new List<string>
            {
                "半日托育",
                "日間托育(平日)",
                "全日托育",
                "夜間托育",
                "臨時托育(平日)",
                "臨時托育(假日)"
            });
            return View();
        }

        // POST: BabyResumes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AccountUserAccount,FirstName,City,District,ApplyDate,RequireDate,Babyago,TypeOfDaycare,TimeSlot,Memo,Display")] BabyResume babyResume, IFormFile? Photo)
        {
            if (ModelState.IsValid)
            {
                if (Photo != null && Photo.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(Photo.FileName);
                    var filePath = Path.Combine(_imagePath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await Photo.CopyToAsync(stream);
                    }

                    babyResume.Photo = "/uploads/" + fileName;
                }

                _context.Add(babyResume);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountUserAccount"] = new SelectList(_context.UserAccounts, "Account", "Account", babyResume.AccountUserAccount);
            ViewData["DaycareTypes"] = new SelectList(new List<string>
            {
                "半日托育",
                "日間托育(平日)",
                "全日托育",
                "夜間托育",
                "臨時托育(平日)",
                "臨時托育(假日)"
            });
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
            ViewData["DaycareTypes"] = new SelectList(new List<string>
            {
                "半日托育",
                "日間托育(平日)",
                "全日托育",
                "夜間托育",
                "臨時托育(平日)",
                "臨時托育(假日)"
            }, babyResume.TypeOfDaycare);
            return View(babyResume);
        }

        // POST: BabyResumes/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AccountUserAccount,FirstName,City,District,ApplyDate,RequireDate,Babyago,TypeOfDaycare,TimeSlot,Memo,Display")] BabyResume babyResume, IFormFile? Photo)
        {
            if (id != babyResume.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (Photo != null && Photo.Length > 0)
                    {
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(Photo.FileName);
                        var filePath = Path.Combine(_imagePath, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await Photo.CopyToAsync(stream);
                        }

                        babyResume.Photo = "/uploads/" + fileName;
                    }
                    else if (_context.Entry(babyResume).Property(p => p.Photo).OriginalValue == null)
                    {
                        // 保持原來的照片路徑為null
                        babyResume.Photo = null;
                    }
                    else
                    {
                        // 保持原來的照片路徑
                        _context.Entry(babyResume).Property(p => p.Photo).IsModified = false;
                    }

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
            ViewData["DaycareTypes"] = new SelectList(new List<string>
            {
                "半日托育",
                "日間托育(平日)",
                "全日托育",
                "夜間托育",
                "臨時托育(平日)",
                "臨時托育(假日)"
            }, babyResume.TypeOfDaycare);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var babyResume = await _context.BabyResumes.FindAsync(id);
            if (babyResume != null)
            {
                _context.BabyResumes.Remove(babyResume);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool BabyResumeExists(int id)
        {
            return _context.BabyResumes.Any(e => e.Id == id);
        }
    }
}
