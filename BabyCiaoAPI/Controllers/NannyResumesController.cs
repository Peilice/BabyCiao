using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BabyCiaoAPI.Models;
using Microsoft.AspNetCore.Cors;
using BabyCiaoAPI.DTO;

namespace BabyCiaoAPI.Controllers
{
    [EnableCors("andy")]
    [Route("api/[controller]")]
    [ApiController]
    public class NannyResumesController : ControllerBase
    {
        private readonly BabyciaoContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public NannyResumesController(BabyciaoContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: api/NannyResumes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NannyResume>>> GetNannyResumes()
        {
            return await _context.NannyResumes.Select(c => new NannyResume
            {
                City = c.City,
                District = c.District,
                Introduction = c.Introduction,
                TypeOfDaycare = c.TypeOfDaycare,
                ServiceItems = c.ServiceItems,
                QuasiPublicChildcare = c.QuasiPublicChildcare,
                ChildcareAvailableUnder2 = c.ChildcareAvailableUnder2,
                ChildcareAvailableOver2 = c.ChildcareAvailableOver2,
                Language = c.Language,
                ProfessionalPortrait = c.ProfessionalPortrait,

            }).ToListAsync();
        }

        // GET: api/NannyResumes/5
        [HttpGet("GetNannyResumeinfo")]
        public async Task<ActionResult<NannyResume>> GetNannyResumeinfo(int id)
        {
            var resumes = await _context.NannyResumes.Select(c => new NannyResumeDTO
            {
                Id = c.Id,
                NannyAccountUserAccount = c.NannyAccountUserAccount,
                City = c.City,
                District = c.District,
                Introduction = c.Introduction,
                //TypeOfDaycare = c.TypeOfDaycare,
                //ServiceItems = c.ServiceItems,
                QuasiPublicChildcare = c.QuasiPublicChildcare,
                ChildcareAvailableUnder2 = c.ChildcareAvailableUnder2,
                ChildcareAvailableOver2 = c.ChildcareAvailableOver2,
                Language = c.Language,
                ServiceCenter = c.ServiceCenter,
                ProfessionalPortrait = c.ProfessionalPortrait,
                //DisplayControl = c.DisplayControl
            }).ToListAsync();


            return Ok(resumes);

        }

        // PUT: api/NannyResumes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNannyResume(int id, NannyResume nannyResume)
        {
            if (id != nannyResume.Id)
            {
                return BadRequest();
            }

            _context.Entry(nannyResume).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NannyResumeExists(id))
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

        // POST: api/NannyResumes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NannyResume>> PostNannyResume(NannyResume nannyResume)
        {
            _context.NannyResumes.Add(nannyResume);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNannyResume", new { id = nannyResume.Id }, nannyResume);
        }

        // DELETE: api/NannyResumes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNannyResume(int id)
        {
            var nannyResume = await _context.NannyResumes.FindAsync(id);
            if (nannyResume == null)
            {
                return NotFound();
            }

            _context.NannyResumes.Remove(nannyResume);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NannyResumeExists(int id)
        {
            return _context.NannyResumes.Any(e => e.Id == id);
        }
    }
}