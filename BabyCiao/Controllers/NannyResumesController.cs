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
        private readonly BabyCiaoContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public NannyResumesController(BabyCiaoContext context, IWebHostEnvironment hostEnvironment)
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
                    await SaveFilesAsync(nannyResume, ProfessionalPortrait, InternalPhoto1, InternalPhoto2, InternalPhoto3, InternalPhoto4, InternalPhoto5);
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
                _context.NannyResumes.Remove(nannyResume);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool NannyResumeExists(int id)
        {
            return _context.NannyResumes.Any(e => e.Id == id);
        }

        private async Task SaveFilesAsync(NannyResume nannyResume, IFormFile? professionalPortrait, IFormFile? internalPhoto1, IFormFile? internalPhoto2, IFormFile? internalPhoto3, IFormFile? internalPhoto4, IFormFile? internalPhoto5)
        {
            var uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "Nannyphoto");
            Directory.CreateDirectory(uploadsFolder);

            if (professionalPortrait != null && professionalPortrait.Length > 0)
            {
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(professionalPortrait.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await professionalPortrait.CopyToAsync(fileStream);
                }
                nannyResume.ProfessionalPortrait = "/Nannyphoto/" + uniqueFileName;
            }

            if (internalPhoto1 != null && internalPhoto1.Length > 0)
            {
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(internalPhoto1.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await internalPhoto1.CopyToAsync(fileStream);
                }
                nannyResume.InternalPhoto1 = "/Nannyphoto/" + uniqueFileName;
            }

            if (internalPhoto2 != null && internalPhoto2.Length > 0)
            {
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(internalPhoto2.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await internalPhoto2.CopyToAsync(fileStream);
                }
                nannyResume.InternalPhoto2 = "/Nannyphoto/" + uniqueFileName;
            }

            if (internalPhoto3 != null && internalPhoto3.Length > 0)
            {
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(internalPhoto3.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await internalPhoto3.CopyToAsync(fileStream);
                }
                nannyResume.InternalPhoto3 = "/Nannyphoto/" + uniqueFileName;
            }

            if (internalPhoto4 != null && internalPhoto4.Length > 0)
            {
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(internalPhoto4.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await internalPhoto4.CopyToAsync(fileStream);
                }
                nannyResume.InternalPhoto4 = "/Nannyphoto/" + uniqueFileName;
            }

            if (internalPhoto5 != null && internalPhoto5.Length > 0)
            {
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(internalPhoto5.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await internalPhoto5.CopyToAsync(fileStream);
                }
                nannyResume.InternalPhoto5 = "/Nannyphoto/" + uniqueFileName;
            }
        }
    }
}
