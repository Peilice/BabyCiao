using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
        public async Task<IEnumerable<BabyResumeDTO>> GetBabyResumes()
        {
            return  _context.BabyResumes.Select(
                bbr => new BabyResumeDTO 
                {
                    Id = bbr.Id,
                    AccountUserAccount = bbr.AccountUserAccount,
                    Photo = bbr.Photo,
                    FirstName = bbr.FirstName,
                    City=bbr.City,
                    District=bbr.District,
                    ApplyDate=bbr.ApplyDate,
                    RequireDate=bbr.RequireDate,
                    Babyage=bbr.Babyage,
                    TypeOfDaycare=bbr.TypeOfDaycare,
                    TimeSlot=bbr.TimeSlot,
                    Memo=bbr.Memo,
                    Display=bbr.Display
                });
        }

        // GET: api/BabyResumes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BabyResume>> GetBabyResume(int id)
        {
            var babyResume = await _context.BabyResumes.FindAsync(id);

            if (babyResume == null)
            {
                return NotFound();
            }

            return babyResume;
        }

        // PUT: api/BabyResumes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBabyResume(int id, BabyResume babyResume)
        {
            if (id != babyResume.Id)
            {
                return BadRequest();
            }

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

        // POST: api/BabyResumes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BabyResume>> PostBabyResume(BabyResume babyResume)
        {
            _context.BabyResumes.Add(babyResume);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBabyResume", new { id = babyResume.Id }, babyResume);
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
