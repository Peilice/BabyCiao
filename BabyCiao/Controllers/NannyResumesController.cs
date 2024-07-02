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
    public class NannyResumesController : Controller
    {
        private readonly BabyCiaoContext _context;

        public NannyResumesController(BabyCiaoContext context)
        {
            _context = context;
        }

        // GET: NannyResumes
        public async Task<IActionResult> Index()
        {
            var babyCiaoContext = _context.NannyResumes.Include(n => n.NannyAccountUserAccountNavigation);
            return View(await babyCiaoContext.ToListAsync());
        }

        // GET: NannyResumes/Details/5
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

        // GET: NannyResumes/Create
        public IActionResult Create()
        {
            ViewData["NannyAccountUserAccount"] = new SelectList(_context.UserAccounts, "Account", "Account");
            return View();
        }

        // POST: NannyResumes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NannyAccountUserAccount,City,District,Introduction,TypeOfDaycare,ServiceItems,QuasiPublicChildcare,ChildcareAvailableUnder2,ChildcareAvailableOver2,Language,ServiceCenter,ProfessionalPortrait,InternalPhoto1,InternalPhoto2,InternalPhoto3,InternalPhoto4,InternalPhoto5,DisplayControl")] nannyResume nannyResume)
        {
            if (ModelState.IsValid)
            {
                if (Request.Form.Files["Picture"] != null)
                {
                    await ReadUploadImage(nannyResume);
                }
                _context.Add(nannyResume);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NannyAccountUserAccount"] = new SelectList(_context.UserAccounts, "Account", "Account", nannyResume.NannyAccountUserAccount);
            return View(nannyResume);
        }

        private async Task ReadUploadImage(nannyResume nannyResume)
        {
            using (BinaryReader br = new BinaryReader(Request.Form.Files["PoliceCriminalRecordCertificate || ChildCareCertificate || NationalIdentificationCard"].OpenReadStream()))
            {
                byte[]? Policedata = br.ReadBytes((int)Request.Form.Files["PoliceCriminalRecordCertificate "].Length);
                byte[]? Nannydata = br.ReadBytes((int)Request.Form.Files["ChildCareCertificate"].Length);
                byte[]? IDdata = br.ReadBytes((int)Request.Form.Files["NationalIdentificationCard"].Length);

                nannyResume.PoliceCriminalRecordCertificate = Policedata;
                nannyResume.childCareCertificate = Nannydata;
                nannyResume.NationalIdentificationCard = IDdata;

            }
        }

        // GET: NannyResumes/Edit/5
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

        // POST: NannyResumes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NannyAccountUserAccount,City,District,Introduction,TypeOfDaycare,ServiceItems,QuasiPublicChildcare,ChildcareAvailableUnder2,ChildcareAvailableOver2,Language,ServiceCenter,ProfessionalPortrait,InternalPhoto1,InternalPhoto2,InternalPhoto3,InternalPhoto4,InternalPhoto5,DisplayControl")] nannyResume nannyResume)
        {
            if (id != nannyResume.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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

        // GET: NannyResumes/Delete/5
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

        // POST: NannyResumes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nannyResume = await _context.NannyResumes.FindAsync(id);
            if (nannyResume != null)
            {
                _context.NannyResumes.Remove(nannyResume);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NannyResumeExists(int id)
        {
            return _context.NannyResumes.Any(e => e.Id == id);
        }
        public async Task<FileResult> GetPicture(int id, string type)
        {
            nannyResume? nannyResume = await _context.NannyResumes.FindAsync(id);

            if (nannyResume == null)
            {
                return null; // or handle the case when nannyResume is not found
            }

            byte[]? content = type switch
            {
                "PoliceCriminalRecordCertificate" => nannyResume.PoliceCriminalRecordCertificate,
                "ChildCareCertificate" => nannyResume.childCareCertificate,
                "NationalIdentificationCard" => nannyResume.NationalIdentificationCard,
                _ => null
            };

            if (content == null)
            {
                return null; // or handle the case when the specified certificate is not found
            }

            return File(content, "image/jpeg");
        }
    }
}
