using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<ActionResult<IEnumerable<NannyResumeDTO>>> GetNannyResumes(
            [FromQuery] string city = null,
            [FromQuery] string district = null,
            [FromQuery] string serviceType = null,
            [FromQuery] string experience = null)
        {
            return await _context.NannyResumes.Select(c => new NannyResumeDTO
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
        [HttpGet("{id}")]
        public async Task<ActionResult<NannyResumeDTO>> GetNannyResumeinfo(int id)
        {
            var resume = await _context.NannyResumes.Select(c => new NannyResumeDTO
            {
                Id = c.Id,
                NannyAccountUserAccount = c.NannyAccountUserAccount,
                City = c.City,
                District = c.District,
                Introduction = c.Introduction,
                TypeOfDaycare = c.TypeOfDaycare,
                ServiceItems = c.ServiceItems,
                QuasiPublicChildcare = c.QuasiPublicChildcare,
                ChildcareAvailableUnder2 = c.ChildcareAvailableUnder2,
                ChildcareAvailableOver2 = c.ChildcareAvailableOver2,
                Language = c.Language,
                ServiceCenter = c.ServiceCenter,
                ProfessionalPortrait = c.ProfessionalPortrait,
                DisplayControl = c.DisplayControl
            }).FirstOrDefaultAsync(c => c.Id == id);

            if (resume == null)
            {
                return NotFound();
            }

            return Ok(resume);
        }

        [HttpPost]
        public async Task<ActionResult<NannyResumeDTO>> PostNannyResume(NannyResumeDTO nannyResumeDTO)
        {
            var nannyResume = new NannyResume
            {
                NannyAccountUserAccount = nannyResumeDTO.NannyAccountUserAccount,
                City = nannyResumeDTO.City,
                District = nannyResumeDTO.District,
                Introduction = nannyResumeDTO.Introduction,
                TypeOfDaycare = nannyResumeDTO.TypeOfDaycare,
                ServiceItems = nannyResumeDTO.ServiceItems,
                QuasiPublicChildcare = nannyResumeDTO.QuasiPublicChildcare,
                ChildcareAvailableUnder2 = nannyResumeDTO.ChildcareAvailableUnder2,
                ChildcareAvailableOver2 = nannyResumeDTO.ChildcareAvailableOver2,
                Language = nannyResumeDTO.Language,
                ServiceCenter = nannyResumeDTO.ServiceCenter,
                ProfessionalPortrait = nannyResumeDTO.ProfessionalPortrait,
                DisplayControl = nannyResumeDTO.DisplayControl
            };

            _context.NannyResumes.Add(nannyResume);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNannyResumeinfo), new { id = nannyResume.Id }, nannyResumeDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutNannyResume(int id, NannyResumeDTO nannyResumeDTO)
        {
            if (id != nannyResumeDTO.Id)
            {
                return BadRequest();
            }

            var nannyResume = await _context.NannyResumes.FindAsync(id);

            if (nannyResume == null)
            {
                return NotFound();
            }

            nannyResume.NannyAccountUserAccount = nannyResumeDTO.NannyAccountUserAccount;
            nannyResume.City = nannyResumeDTO.City;
            nannyResume.District = nannyResumeDTO.District;
            nannyResume.Introduction = nannyResumeDTO.Introduction;
            nannyResume.TypeOfDaycare = nannyResumeDTO.TypeOfDaycare;
            nannyResume.ServiceItems = nannyResumeDTO.ServiceItems;
            nannyResume.QuasiPublicChildcare = nannyResumeDTO.QuasiPublicChildcare;
            nannyResume.ChildcareAvailableUnder2 = nannyResumeDTO.ChildcareAvailableUnder2;
            nannyResume.ChildcareAvailableOver2 = nannyResumeDTO.ChildcareAvailableOver2;
            nannyResume.Language = nannyResumeDTO.Language;
            nannyResume.ServiceCenter = nannyResumeDTO.ServiceCenter;
            nannyResume.ProfessionalPortrait = nannyResumeDTO.ProfessionalPortrait;
            nannyResume.DisplayControl = nannyResumeDTO.DisplayControl;

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
