using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BabyCiao.Models;
using BabyCiao.ViewModel;
using NuGet.Protocol;
using Microsoft.AspNetCore.Authorization;

namespace BabyCiao.Controllers
{
    [Authorize(Roles = "公告編輯")]
    public class AnnouncementsController : Controller
    {
        private readonly BabyciaoContext _context;

        public AnnouncementsController(BabyciaoContext context)
        {
            _context = context;
        }

        // GET: Announcements
        public async Task<IActionResult> Index()
        {
            var babyCiaoContext = _context.Announcements.Include(a => a.AccountUserAccountNavigation);
            return View(await babyCiaoContext.ToListAsync());
        }

        // GET: Announcements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var announcement = await _context.Announcements
                .Include(a => a.AccountUserAccountNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (announcement == null)
            {
                return NotFound();
            }

            return View(announcement);
        }

        // GET: Announcements/Create
        public IActionResult Create()
        {
            ViewData["AccountUserAccount"] = HttpContext.User.Identity.Name;
            return View();
        }

        //        // POST: Announcements/Create
        //        // To protect from overposting attacks, enable the specific properties you want to bind to.
        //        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AccountUserAccount,Tittle,Article,ReferenceName,ReferenceRoute,Type,Display,Picture")] andy_announcementViewModel my_vm)
        {
            Announcement announcement = new Announcement()
            {
                Tittle = my_vm.Tittle,
                AccountUserAccount = my_vm.AccountUserAccount,
                Article = my_vm.Article,
                ReferenceName = my_vm.ReferenceName,
                ReferenceRoute = my_vm.ReferenceRoute,
                Type = my_vm.Type,
                Display = my_vm.Display
            };
            
            

            if (ModelState.IsValid)
            {
                _context.Add(announcement);
                await _context.SaveChangesAsync();

                //先取得新公告的ID
                var newAnnouncement = await _context.Announcements.FindAsync(my_vm.Tittle);
                //新增公告照片
                AnnouncementPhoto announcementPhoto = new AnnouncementPhoto()
                {
                    PhotoName = my_vm.Picture,
                    IdAnnouncement = newAnnouncement.Id
                };
                _context.Add(announcementPhoto);

                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountUserAccount"] = new SelectList(_context.UserAccounts, "Account", "Account", announcement);
            return View(announcement);
        }

        //        // GET: Announcements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var announcement = await _context.Announcements.FindAsync(id);
            announcement.ToJson();

            if (announcement == null)
            {
                return NotFound();
            }
            ViewData["AccountUserAccount"] = new SelectList(_context.UserAccounts, "Account", "Account", announcement.AccountUserAccount);
            return View(announcement);
        }

        //        // POST: Announcements/Edit/5
        //        // To protect from overposting attacks, enable the specific properties you want to bind to.
        //        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AccountUserAccount,PublishTime,Tittle,Article,ReferenceName,ReferenceRoute,Type,Display")] andy_announcementViewModel my_announcement)
        {
            Announcement announcement = await _context.Announcements.FindAsync(id);

            if (id != my_announcement.Id)
            {
                return NotFound();
            }

            announcement.AccountUserAccount = my_announcement.AccountUserAccount;
            announcement.Tittle = my_announcement.Tittle;
            announcement.Article = my_announcement.Article;
            announcement.ReferenceName = my_announcement.ReferenceName;
            announcement.ReferenceRoute = my_announcement.ReferenceRoute;
            announcement.Type = my_announcement.Type;
            announcement.Display = my_announcement.Display;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(announcement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnnouncementExists(my_announcement.Id))
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
            ViewData["AccountUserAccount"] = new SelectList(_context.UserAccounts, "Account", "Account", my_announcement.AccountUserAccount);
            return View(my_announcement);
        }

        
        //        // GET: Announcements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var announcement = await _context.Announcements
                .Include(a => a.AccountUserAccountNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (announcement == null)
            {
                return NotFound();
            }

            return View(announcement);
        }

        //        // POST: Announcements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var announcement = await _context.Announcements.FindAsync(id);
            if (announcement != null)
            {
                _context.Announcements.Remove(announcement);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnnouncementExists(int id)
        {
            return _context.Announcements.Any(e => e.Id == id);
        }
        
        [HttpPost]
        public async Task<IActionResult> Index(int id)
        {
            var babyCiaoContext = _context.Announcements.Include(a => a.AccountUserAccountNavigation);
            var announcement = await _context.Announcements.FindAsync(id);
            if (announcement != null)
            {
                if (announcement.Display) {
                    announcement.Display=false;
                }
                else
                {
                    announcement.Display = true;
                }
                _context.Announcements.Update(announcement);
                await _context.SaveChangesAsync();
            }
            return PartialView("Index", babyCiaoContext);
        }

        //private void ReadUploadImage(AnnouncementPhoto announcementPhoto)
        //{
        //    using (BinaryReader br = new BinaryReader(Request.Form.Files["Picture"].OpenReadStream()))
        //    {
        //        announcementPhoto.PhotoName = br.ReadBytes((int)Request.Form.Files["Picture"].Length);
        //    }
        //}

        // GET: Categories/GetPicture/{id}
        //public async Task<FileResult> GetPicture(int id)
        //{
        //    AnnouncementPhoto announcementPhoto = await _context.AnnouncementPhotos.FindAsync(id);
        //    byte[] picture = announcementPhoto?.PhotoName;
        //    return File(picture, "image/jpeg");
        //}
       
    }
}
