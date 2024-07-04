using System;
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
    public class NannyResumesController : Controller
    {
        private readonly BabyciaoContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public NannyResumesController(BabyciaoContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var babyCiaoContext = _context.NannyResumes.Include(n => n.NannyAccountUserAccountNavigation);
            return View(await babyCiaoContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nannyResume = await _context.NannyResumes
                .Include(n => n.NannyAccountUserAccountNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nannyResume == null)
            {
                return NotFound();
            }

            return View(nannyResume);
        }

        public IActionResult Create()
        {
            ViewData["NannyAccountUserAccount"] = new SelectList(_context.UserAccounts, "Account", "Account");
            ViewData["TypeOfDaycare"] = new SelectList(new[]
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NannyAccountUserAccount,City,District,Introduction,TypeOfDaycare,ServiceItems,QuasiPublicChildcare,ChildcareAvailableUnder2,ChildcareAvailableOver2,Language,ServiceCenter,DisplayControl")] NannyResume nannyResume, IFormFile? ProfessionalPortrait, IFormFile? InternalPhoto1, IFormFile? InternalPhoto2, IFormFile? InternalPhoto3, IFormFile? InternalPhoto4, IFormFile? InternalPhoto5)
        {
            if (ModelState.IsValid)
            {
                await SaveFilesAsync(nannyResume, ProfessionalPortrait, InternalPhoto1, InternalPhoto2, InternalPhoto3, InternalPhoto4, InternalPhoto5);
                _context.Add(nannyResume);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NannyAccountUserAccount"] = new SelectList(_context.UserAccounts, "Account", "Account", nannyResume.NannyAccountUserAccount);
            ViewData["TypeOfDaycare"] = new SelectList(new[]
            {
                "半日托育",
                "日間托育(平日)",
                "全日托育",
                "夜間托育",
                "臨時托育(平日)",
                "臨時托育(假日)"
            }, nannyResume.TypeOfDaycare);
            return View(nannyResume);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nannyResume = await _context.NannyResumes.FindAsync(id);
            if (nannyResume == null)
            {
                return NotFound();
            }
            ViewData["NannyAccountUserAccount"] = new SelectList(_context.UserAccounts, "Account", "Account", nannyResume.NannyAccountUserAccount);
            ViewData["TypeOfDaycare"] = new SelectList(new[]
            {
                "半日托育",
                "日間托育(平日)",
                "全日托育",
                "夜間托育",
                "臨時托育(平日)",
                "臨時托育(假日)"
            }, nannyResume.TypeOfDaycare);
            return View(nannyResume);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NannyAccountUserAccount,City,District,Introduction,TypeOfDaycare,ServiceItems,QuasiPublicChildcare,ChildcareAvailableUnder2,ChildcareAvailableOver2,Language,ServiceCenter,DisplayControl")] NannyResume nannyResume, IFormFile? ProfessionalPortrait, IFormFile? InternalPhoto1, IFormFile? InternalPhoto2, IFormFile? InternalPhoto3, IFormFile? InternalPhoto4, IFormFile? InternalPhoto5)
        {
            if (id != nannyResume.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingResume = await _context.NannyResumes.AsNoTracking().FirstOrDefaultAsync(n => n.Id == id);
                    if (existingResume == null)
                    {
                        return NotFound();
                    }

                    await SaveFilesAsync(nannyResume, ProfessionalPortrait, InternalPhoto1, InternalPhoto2, InternalPhoto3, InternalPhoto4, InternalPhoto5, existingResume);

                    _context.Update(nannyResume);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NannyResumeExists(nannyResume.Id))
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
            ViewData["NannyAccountUserAccount"] = new SelectList(_context.UserAccounts, "Account", "Account", nannyResume.NannyAccountUserAccount);
            ViewData["TypeOfDaycare"] = new SelectList(new[]
            {
                "半日托育",
                "日間托育(平日)",
                "全日托育",
                "夜間托育",
                "臨時托育(平日)",
                "臨時托育(假日)"
            }, nannyResume.TypeOfDaycare);
            return View(nannyResume);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nannyResume = await _context.NannyResumes
                .Include(n => n.NannyAccountUserAccountNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nannyResume == null)
            {
                return NotFound();
            }

            return View(nannyResume);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nannyResume = await _context.NannyResumes.FindAsync(id);
            if (nannyResume != null)
            {
                DeletePhotos(nannyResume);
                _context.NannyResumes.Remove(nannyResume);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool NannyResumeExists(int id)
        {
            return _context.NannyResumes.Any(e => e.Id == id);
        }

        private async Task SaveFilesAsync(NannyResume nannyResume, IFormFile? professionalPortrait, IFormFile? internalPhoto1, IFormFile? internalPhoto2, IFormFile? internalPhoto3, IFormFile? internalPhoto4, IFormFile? internalPhoto5, NannyResume? existingResume = null)
        {
            var uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "Nannyphoto");
            Directory.CreateDirectory(uploadsFolder);

            if (professionalPortrait != null && professionalPortrait.Length > 0)
            {
                if (existingResume != null && !string.IsNullOrEmpty(existingResume.ProfessionalPortrait))
                {
                    DeletePhoto(existingResume.ProfessionalPortrait);
                }

                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(professionalPortrait.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await professionalPortrait.CopyToAsync(fileStream);
                }
                nannyResume.ProfessionalPortrait = "/Nannyphoto/" + uniqueFileName;
            }
            else if (existingResume != null)
            {
                nannyResume.ProfessionalPortrait = existingResume.ProfessionalPortrait;
            }

            if (internalPhoto1 != null && internalPhoto1.Length > 0)
            {
                if (existingResume != null && !string.IsNullOrEmpty(existingResume.InternalPhoto1))
                {
                    DeletePhoto(existingResume.InternalPhoto1);
                }

                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(internalPhoto1.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await internalPhoto1.CopyToAsync(fileStream);
                }
                nannyResume.InternalPhoto1 = "/Nannyphoto/" + uniqueFileName;
            }
            else if (existingResume != null)
            {
                nannyResume.InternalPhoto1 = existingResume.InternalPhoto1;
            }

            if (internalPhoto2 != null && internalPhoto2.Length > 0)
            {
                if (existingResume != null && !string.IsNullOrEmpty(existingResume.InternalPhoto2))
                {
                    DeletePhoto(existingResume.InternalPhoto2);
                }

                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(internalPhoto2.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await internalPhoto2.CopyToAsync(fileStream);
                }
                nannyResume.InternalPhoto2 = "/Nannyphoto/" + uniqueFileName;
            }
            else if (existingResume != null)
            {
                nannyResume.InternalPhoto2 = existingResume.InternalPhoto2;
            }

            if (internalPhoto3 != null && internalPhoto3.Length > 0)
            {
                if (existingResume != null && !string.IsNullOrEmpty(existingResume.InternalPhoto3))
                {
                    DeletePhoto(existingResume.InternalPhoto3);
                }

                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(internalPhoto3.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await internalPhoto3.CopyToAsync(fileStream);
                }
                nannyResume.InternalPhoto3 = "/Nannyphoto/" + uniqueFileName;
            }
            else if (existingResume != null)
            {
                nannyResume.InternalPhoto3 = existingResume.InternalPhoto3;
            }

            if (internalPhoto4 != null && internalPhoto4.Length > 0)
            {
                if (existingResume != null && !string.IsNullOrEmpty(existingResume.InternalPhoto4))
                {
                    DeletePhoto(existingResume.InternalPhoto4);
                }

                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(internalPhoto4.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await internalPhoto4.CopyToAsync(fileStream);
                }
                nannyResume.InternalPhoto4 = "/Nannyphoto/" + uniqueFileName;
            }
            else if (existingResume != null)
            {
                nannyResume.InternalPhoto4 = existingResume.InternalPhoto4;
            }

            if (internalPhoto5 != null && internalPhoto5.Length > 0)
            {
                if (existingResume != null && !string.IsNullOrEmpty(existingResume.InternalPhoto5))
                {
                    DeletePhoto(existingResume.InternalPhoto5);
                }

                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(internalPhoto5.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await internalPhoto5.CopyToAsync(fileStream);
                }
                nannyResume.InternalPhoto5 = "/Nannyphoto/" + uniqueFileName;
            }
            else if (existingResume != null)
            {
                nannyResume.InternalPhoto5 = existingResume.InternalPhoto5;
            }
        }

        private void DeletePhoto(string photoPath)
        {
            var filePath = Path.Combine(_hostEnvironment.WebRootPath, photoPath.TrimStart('/'));
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }

        private void DeletePhotos(NannyResume nannyResume)
        {
            if (!string.IsNullOrEmpty(nannyResume.ProfessionalPortrait))
            {
                DeletePhoto(nannyResume.ProfessionalPortrait);
            }

            if (!string.IsNullOrEmpty(nannyResume.InternalPhoto1))
            {
                DeletePhoto(nannyResume.InternalPhoto1);
            }

            if (!string.IsNullOrEmpty(nannyResume.InternalPhoto2))
            {
                DeletePhoto(nannyResume.InternalPhoto2);
            }

            if (!string.IsNullOrEmpty(nannyResume.InternalPhoto3))
            {
                DeletePhoto(nannyResume.InternalPhoto3);
            }

            if (!string.IsNullOrEmpty(nannyResume.InternalPhoto4))
            {
                DeletePhoto(nannyResume.InternalPhoto4);
            }

            if (!string.IsNullOrEmpty(nannyResume.InternalPhoto5))
            {
                DeletePhoto(nannyResume.InternalPhoto5);
            }
        }
    }
}
