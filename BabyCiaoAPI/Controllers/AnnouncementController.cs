using BabyCiaoAPI.DTO;
using BabyCiaoAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BabyCiaoAPI.Controllers
{
    [EnableCors("andy")]
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementController : ControllerBase
    {
        private readonly BabyciaoContext _context;
        public AnnouncementController(BabyciaoContext context)
        {
            _context = context;
        }



        // GET: api/Announcements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnnouncementWithPhotosDTO>>> Announcements()
        {
            var announcements = await _context.Announcements
                .Select(a => new AnnouncementWithPhotosDTO
                {
                    Id = a.Id,
                    AccountUserAccount = a.AccountUserAccount,
                    PublishTime = a.PublishTime,
                    Tittle = a.Tittle,
                    Article = a.Article,
                    ReferenceName = a.ReferenceName,
                    ReferenceRoute = a.ReferenceRoute,
                    Type = a.Type,
                    Display = a.Display,
                    Photos = a.AnnouncementPhotos.Select(p => new AnnouncementPhotoDTO
                    {
                        Id = p.Id,
                        IdAnnouncement = p.IdAnnouncement,
                        PhotoName = p.PhotoName,
                        BuiledTime = p.BuiledTime
                    }).ToList()
                }).ToListAsync();

            return Ok(announcements);
        }

        // GET: api/Announcements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AnnouncementWithPhotosDTO>> GetAnnouncement(int id)
        {
            var announcement = await _context.Announcements
                .Where(a => a.Id == id)
                .Select(a => new AnnouncementWithPhotosDTO
                {
                    Id = a.Id,
                    AccountUserAccount = a.AccountUserAccount,
                    PublishTime = a.PublishTime,
                    Tittle = a.Tittle,
                    Article = a.Article,
                    ReferenceName = a.ReferenceName,
                    ReferenceRoute = a.ReferenceRoute,
                    Type = a.Type,
                    Display = a.Display,
                    Photos = a.AnnouncementPhotos.Select(p => new AnnouncementPhotoDTO
                    {
                        Id = p.Id,
                        IdAnnouncement = p.IdAnnouncement,
                        PhotoName = p.PhotoName,
                        BuiledTime = p.BuiledTime
                    }).ToList()
                }).FirstOrDefaultAsync();

            if (announcement == null)
            {
                return NotFound();
            }

            return Ok(announcement);
        }

        // POST: api/Announcements
        //[HttpPost]
        //public async Task<ActionResult<AnnouncementWithPhotosDTO>> CreateAnnouncement(AnnouncementWithPhotosDTO announcementDto)
        //{
        //    var announcement = new Announcement
        //    {
        //        AccountUserAccount = announcementDto.AccountUserAccount,
        //        PublishTime = announcementDto.PublishTime,
        //        Tittle = announcementDto.Tittle,
        //        Article = announcementDto.Article,
        //        ReferenceName = announcementDto.ReferenceName,
        //        ReferenceRoute = announcementDto.ReferenceRoute,
        //        Type = announcementDto.Type,
        //        Display = announcementDto.Display,
        //        AnnouncementPhotos = announcementDto.Photos.Select(p => new AnnouncementPhoto
        //        {
        //            PhotoName = p.PhotoName,
        //            BuiledTime = p.BuiledTime
        //        }).ToList()
        //    };

        //    _context.Announcements.Add(announcement);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction(nameof(GetAnnouncement), new { id = announcement.Id }, announcementDto);
        //}

        // PUT: api/Announcements/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAnnouncement(int id, AnnouncementWithPhotosDTO announcementDto)
        {
            if (id != announcementDto.Id)
            {
                return BadRequest();
            }

            var announcement = await _context.Announcements.Include(a => a.AnnouncementPhotos).FirstOrDefaultAsync(a => a.Id == id);

            if (announcement == null)
            {
                return NotFound();
            }

            announcement.AccountUserAccount = announcementDto.AccountUserAccount;
            announcement.PublishTime = announcementDto.PublishTime;
            announcement.Tittle = announcementDto.Tittle;
            announcement.Article = announcementDto.Article;
            announcement.ReferenceName = announcementDto.ReferenceName;
            announcement.ReferenceRoute = announcementDto.ReferenceRoute;
            announcement.Type = announcementDto.Type;
            announcement.Display = announcementDto.Display;

            // 更新照片
            _context.AnnouncementPhotos.RemoveRange(announcement.AnnouncementPhotos);
            announcement.AnnouncementPhotos = announcementDto.Photos.Select(p => new AnnouncementPhoto
            {
                IdAnnouncement = id,
                PhotoName = p.PhotoName,
                BuiledTime = p.BuiledTime
            }).ToList();

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnnouncementExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Announcements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnnouncement(int id)
        {
            var announcement = await _context.Announcements.Include(a => a.AnnouncementPhotos).FirstOrDefaultAsync(a => a.Id == id);

            if (announcement == null)
            {
                return NotFound();
            }

            _context.Announcements.Remove(announcement);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AnnouncementExists(int id)
        {
            return _context.Announcements.Any(e => e.Id == id);
        }



        [HttpGet("getAnnouncements")]
        public async Task<ActionResult<List<getAnnouncement_DTO>>> getAnnouncements()
        {
            var s = _context.Announcements.Where(a => a.Display == true).ToList();

            List<getAnnouncement_DTO> DTOs = new List<getAnnouncement_DTO>();
            foreach (var item in s)
            {
                getAnnouncement_DTO dTO = new getAnnouncement_DTO();
                dTO.id = item.Id;
                dTO.Tittle = item.Tittle;
                dTO.PublishTime = item.PublishTime;
                dTO.Type = item.Type;
                DTOs.Add(dTO);
            }

            return DTOs;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<getAnnouncement_once_DTO>> getAnnouncement_once(int id)
        {
            var s = _context.Announcements.Where(c => c.Id == id).FirstOrDefault();
            getAnnouncement_once_DTO DTO = new getAnnouncement_once_DTO();

            DTO.Tittle = s.Tittle;
            DTO.PublishTime = s.PublishTime;
            DTO.Article = s.Article;
            DTO.ReferenceName = s.ReferenceName;
            DTO.ReferenceRoute = s.ReferenceRoute;
            DTO.Type = s.Type;

            return DTO;
        }
    }
}

