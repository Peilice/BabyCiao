using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BabyCiao.Models;
using BabyCiao.Models.DTO;
using NuGet.Protocol;
using Microsoft.CodeAnalysis;

namespace BabyCiao.Controllers
{
    public class OnlineCompetitionsController : Controller
    {
        private readonly BabyciaoContext _context;
        private IWebHostEnvironment _webHostEnvironment;

        public OnlineCompetitionsController(BabyciaoContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: OnlineCompetitions
        public async Task<IActionResult> Index()
        {
            var babyCiaoContext = _context.OnlineCompetitions.Include(o => o.AccountUserAccountNavigation);
            return View(await babyCiaoContext.ToListAsync());
        }

        // GET: OnlineCompetitions/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            var competitionDTO =await (from con in _context.OnlineCompetitions
                                       join cp in _context.CompetitionPhotos on con.Id equals cp.IdOnlineCompetition
                                       where con.Id == id
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

                                      CompetitionPhotoNames = cp.PhotoName,
                                      IdOnlineCompetition=cp.IdOnlineCompetition,
                                  }).FirstOrDefaultAsync();

            if (competitionDTO == null)
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
        public async Task<IActionResult> Create([FromForm] OnlineCompetitionsDTO onlineCompetitionDTO)

        {
            if (onlineCompetitionDTO == null)
            {
                return NotFound();
            }


            var newcompetiton = new OnlineCompetition
            {
                Id=onlineCompetitionDTO.Id,
                CompetitionName = onlineCompetitionDTO.CompetitionName,
                AccountUserAccount = onlineCompetitionDTO.AccountUserAccount,
                StartTime = onlineCompetitionDTO.StartTime,
                EndTime = onlineCompetitionDTO.EndTime,
                Content = onlineCompetitionDTO.Content,
                ModifiedTime = onlineCompetitionDTO.ModifiedTime,
                Statement = onlineCompetitionDTO.Statement,

            };
            _context.Add(newcompetiton);
            await _context.SaveChangesAsync();

            var newid= newcompetiton.Id;
            var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            var filepath = Path.Combine(uploadPath, onlineCompetitionDTO.CompetitionPhotoName.FileName);
            using (var fileStream = new FileStream(filepath, FileMode.Create))
            {
                await onlineCompetitionDTO.CompetitionPhotoName.CopyToAsync(fileStream);// write file into fileStream
            }

            onlineCompetitionDTO.CompetitionPhotoNames = onlineCompetitionDTO.CompetitionPhotoName.FileName;
            var newphoto = new CompetitionPhoto
            {
                IdOnlineCompetition = newid,
                PhotoName = onlineCompetitionDTO.CompetitionPhotoNames,
            };
            _context.Add(newphoto);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
            
        }

        // GET: OnlineCompetitions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var competitionDTO =await (from con in _context.OnlineCompetitions
                                       join cp in _context.CompetitionPhotos on con.Id equals cp.IdOnlineCompetition
                                       where con.Id == id
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


                                      CompetitionPhotoNames = cp.PhotoName,

                                  }).FirstOrDefaultAsync();



            var onlineCompetition = await _context.OnlineCompetitions.FindAsync(id);
            if (onlineCompetition == null)
            {
                return NotFound();
            }
            return View(competitionDTO);
        }

        // POST: OnlineCompetitions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] OnlineCompetitionsDTO CompetitionDTO)
        {
            
            if (id == 0)
            {
                return NotFound();
            }

            var editcompetition = await _context.OnlineCompetitions.FindAsync(id);
            if (editcompetition == null)
            {
                return NotFound();
            }

            editcompetition.CompetitionName = CompetitionDTO.CompetitionName;
            editcompetition.AccountUserAccount = CompetitionDTO.AccountUserAccount;
            editcompetition.StartTime = CompetitionDTO.StartTime;
            editcompetition.EndTime = CompetitionDTO.EndTime;
            editcompetition.Content = CompetitionDTO.Content;
            editcompetition.ModifiedTime = DateOnly.FromDateTime(DateTime.Now);
            editcompetition.Statement = CompetitionDTO.Statement;

            try
            {            
                _context.Update(editcompetition);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OnlineCompetitionExists(CompetitionDTO.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            //尋找照片資料
            var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            var filepath = Path.Combine(uploadPath, CompetitionDTO.CompetitionPhotoName.FileName);
            using (var fileStream = new FileStream(filepath, FileMode.Create))
            {
                await CompetitionDTO.CompetitionPhotoName.CopyToAsync(fileStream);// write file into fileStream
            }

            CompetitionDTO.CompetitionPhotoNames = CompetitionDTO.CompetitionPhotoName.FileName;

            var existingPhoto = _context.CompetitionPhotos
                            .FirstOrDefault(cp => cp.IdOnlineCompetition == id);

            if (existingPhoto != null)
            {
                existingPhoto.PhotoName = CompetitionDTO.CompetitionPhotoNames;
                existingPhoto.ModifiedTime = DateTime.Now;

                _context.CompetitionPhotos.Update(existingPhoto);
                _context.SaveChanges();
            }
            else
            {
                var newphoto = (from cp in _context.CompetitionPhotos
                                where cp.IdOnlineCompetition == id
                                select new CompetitionPhoto
                                {
                                    //修改新照片，但已經有原始的ID可以讀取
                                    IdOnlineCompetition = id,
                                    PhotoName = CompetitionDTO.CompetitionPhotoNames,
                                    ModifiedTime = DateTime.Now
                                }).FirstOrDefault();
                _context.CompetitionPhotos.Update(newphoto);
                await _context.SaveChangesAsync();
            }
           
            return RedirectToAction(nameof(Index));
            
        }


        [HttpPost]
        public async Task<IActionResult> DeletePhoto(int CompetitionPhotoId)
        {

            var photo = await _context.CompetitionPhotos.FindAsync(CompetitionPhotoId);
            if (photo == null)
            {
                return NotFound();
            }

            _context.CompetitionPhotos.Remove(photo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Edit), new { id = photo.IdOnlineCompetition });
        }


        // GET: OnlineCompetitions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var competitionDTO =await (from con in _context.OnlineCompetitions
                                  join cp in _context.CompetitionPhotos on con.Id equals cp.IdOnlineCompetition
                                 where con.Id == id
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

                                     CompetitionPhotoNames = cp.PhotoName,
                                 }).FirstOrDefaultAsync();

            if (competitionDTO == null)
            {
                return NotFound();
            }

            return View(competitionDTO);
        }

        // POST: OnlineCompetitions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, [FromForm]OnlineCompetitionsDTO competitionDTO)
        {
            var deletephoto = await _context.CompetitionPhotos.FirstOrDefaultAsync(cp => cp.IdOnlineCompetition == id);
            //deletephoto.IdOnlineCompetition=competitionDTO.IdOnlineCompetition;

            if (deletephoto != null)
            {
                _context.CompetitionPhotos.Remove(deletephoto);
                _context.SaveChanges();
            }

            var deletecompetition = await _context.OnlineCompetitions.FirstOrDefaultAsync(cop =>cop.Id == id);
            //deletecompetition.Id=competitionDTO.Id;

            if (deletecompetition != null)
            {
                _context.OnlineCompetitions.Remove(deletecompetition);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool OnlineCompetitionExists(int id)
        {
            return _context.OnlineCompetitions.Any(e => e.Id == id);
        }

        //public async Task<FileResult> GetPicture(int id)
        //{
        //    PlatformPhoto photoname = await _context.PlatformPhotos.FindAsync(id);
        //    byte[]? picture = photoname?.PhotoName;
        //    return File(picture, "image/jpeg");
        //}
    }
}
