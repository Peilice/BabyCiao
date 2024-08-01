using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BabyCiaoAPI.Models;

namespace BabyCiaoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BabyResumesController : ControllerBase
    {
        private readonly BabyciaoContext _context;

        public BabyResumesController(BabyciaoContext context)
        {
            _context = context;
        }

        // GET: api/BabyResumes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BabyResumeDTO>>> GetBabyResumes(
            [FromQuery] string city = null,
            [FromQuery] string district = null,
            [FromQuery] string typeOfDaycare = null)
        {
            var query = _context.BabyResumes.AsQueryable();

            if (!string.IsNullOrEmpty(city))
            {
                query = query.Where(b => b.City.Contains(city));
            }

            if (!string.IsNullOrEmpty(district))
            {
                query = query.Where(b => b.District.Contains(district));
            }

            if (!string.IsNullOrEmpty(typeOfDaycare))
            {
                query = query.Where(b => b.TypeOfDaycare == typeOfDaycare);
            }

            var results = await query.Select(b => new BabyResumeDTO
            {
                Id = b.Id,
                AccountUserAccount = b.AccountUserAccount,
                Photo = b.Photo,
                FirstName = b.FirstName,
                City = b.City,
                District = b.District,
                ApplyDate = b.ApplyDate,
                RequireDate = b.RequireDate,
                Babyage = b.Babyage,
                TypeOfDaycare = b.TypeOfDaycare,
                TimeSlot = b.TimeSlot,
                Memo = b.Memo,
                Display = b.Display
            }).ToListAsync();

            return results;
        }

        // GET: api/BabyResumes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BabyResumeDTO>> GetBabyResume(int id)
        {
            var babyResume = await _context.BabyResumes
                .Select(b => new BabyResumeDTO
                {
                    Id = b.Id,
                    AccountUserAccount = b.AccountUserAccount,
                    Photo = b.Photo,
                    FirstName = b.FirstName,
                    City = b.City,
                    District = b.District,
                    ApplyDate = b.ApplyDate,
                    RequireDate = b.RequireDate,
                    Babyage = b.Babyage,
                    TypeOfDaycare = b.TypeOfDaycare,
                    TimeSlot = b.TimeSlot,
                    Memo = b.Memo,
                    Display = b.Display
                })
                .FirstOrDefaultAsync(b => b.Id == id);

            if (babyResume == null)
            {
                return NotFound();
            }

            return babyResume;
        }

        // POST: api/BabyResumes
        [HttpPost]
        public async Task<ActionResult<BabyResumeDTO>> PostBabyResume(BabyResumeDTO babyResumeDTO)
        {
            var babyResume = new BabyResume
            {
                AccountUserAccount = babyResumeDTO.AccountUserAccount,
                Photo = babyResumeDTO.Photo,
                FirstName = babyResumeDTO.FirstName,
                City = babyResumeDTO.City,
                District = babyResumeDTO.District,
                ApplyDate = babyResumeDTO.ApplyDate,
                RequireDate = babyResumeDTO.RequireDate,
                Babyage = babyResumeDTO.Babyage,
                TypeOfDaycare = babyResumeDTO.TypeOfDaycare,
                TimeSlot = babyResumeDTO.TimeSlot,
                Memo = babyResumeDTO.Memo,
                Display = babyResumeDTO.Display
            };

            _context.BabyResumes.Add(babyResume);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBabyResume), new { id = babyResume.Id }, babyResumeDTO);
        }

        // PUT: api/BabyResumes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBabyResume(int id, BabyResumeDTO babyResumeDTO)
        {
            if (id != babyResumeDTO.Id)
            {
                return BadRequest();
            }

            var babyResume = await _context.BabyResumes.FindAsync(id);

            if (babyResume == null)
            {
                return NotFound();
            }

            babyResume.AccountUserAccount = babyResumeDTO.AccountUserAccount;
            babyResume.Photo = babyResumeDTO.Photo;
            babyResume.FirstName = babyResumeDTO.FirstName;
            babyResume.City = babyResumeDTO.City;
            babyResume.District = babyResumeDTO.District;
            babyResume.ApplyDate = babyResumeDTO.ApplyDate;
            babyResume.RequireDate = babyResumeDTO.RequireDate;
            babyResume.Babyage = babyResumeDTO.Babyage;
            babyResume.TypeOfDaycare = babyResumeDTO.TypeOfDaycare;
            babyResume.TimeSlot = babyResumeDTO.TimeSlot;
            babyResume.Memo = babyResumeDTO.Memo;
            babyResume.Display = babyResumeDTO.Display;

            _context.Entry(babyResume).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BabyResumeExists(id))
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

        // DELETE: api/BabyResumes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBabyResume(int id)
        {
            var babyResume = await _context.BabyResumes.FindAsync(id);

            if (babyResume == null)
            {
                return NotFound();
            }

            _context.BabyResumes.Remove(babyResume);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BabyResumeExists(int id)
        {
            return _context.BabyResumes.Any(e => e.Id == id);
        }
    }
}
