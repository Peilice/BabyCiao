using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BabyCiaoAPI.Models;
using BabyCiaoAPI.DTO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BabyCiaoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Route("api/[controller]")]
    public class NannyResumesController : ControllerBase
    {
        private readonly BabyciaoContext _context;

        public NannyResumesController(BabyciaoContext context)
        {
            _context = context;
        }

        // GET: api/NannyResumes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NannyResumeDTO>>> GetNannyResumes(
            [FromQuery] string city = null,
            [FromQuery] string district = null,
            [FromQuery] string serviceType = null,
            [FromQuery] string experience = null)
        {
            var query = _context.NannyResumes.AsQueryable();

            if (!string.IsNullOrEmpty(city))
            {
                query = query.Where(n => n.City.Contains(city));
        }

            if (!string.IsNullOrEmpty(district))
            {
                query = query.Where(n => n.District.Contains(district));
            }

            if (!string.IsNullOrEmpty(serviceType))
            {
                query = query.Where(n => n.TypeOfDaycare == serviceType);
            }

            if (!string.IsNullOrEmpty(experience))
            {
                // Add your experience filtering logic here
            }

            var results = await query.Select(n => new NannyResumeDTO
            {
                Id = n.Id,
                NannyAccountUserAccount = n.NannyAccountUserAccount,
                City = n.City,
                District = n.District,
                Introduction = n.Introduction,
                TypeOfDaycare = n.TypeOfDaycare,
                ServiceItems = n.ServiceItems,
                QuasiPublicChildcare = n.QuasiPublicChildcare,
                ChildcareAvailableUnder2 = n.ChildcareAvailableUnder2,
                ChildcareAvailableOver2 = n.ChildcareAvailableOver2,
                Language = n.Language,
                ServiceCenter = n.ServiceCenter,
                ProfessionalPortrait = n.ProfessionalPortrait,
                DisplayControl = n.DisplayControl

            }).ToListAsync();

            return results;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NannyResumeDTO>> GetNannyResume(int id)
        {
            var nannyResume = await (from n in _context.NannyResumes
                                    join u in _context.NannyResumePhotos
                                    on n.Id equals u.IdNannyResume
                                    where u.Id == id
                                    select new NannyResumeDTO
                                    {
                                        Id = n.Id,
                                        NannyAccountUserAccount = n.NannyAccountUserAccount,
                                        City = n.City,
                                        District = n.District,
                                        Introduction = n.Introduction,
                                        TypeOfDaycare = n.TypeOfDaycare,
                                        ServiceItems = n.ServiceItems,
                                        QuasiPublicChildcare = n.QuasiPublicChildcare,
                                        ChildcareAvailableUnder2 = n.ChildcareAvailableUnder2,
                                        ChildcareAvailableOver2 = n.ChildcareAvailableOver2,
                                        Language = n.Language,
                                        ServiceCenter = n.ServiceCenter,
                                        ProfessionalPortrait = n.ProfessionalPortrait,
                                        DisplayControl = n.DisplayControl,
                                        Photo = u.PhotoName
                                    }).FirstOrDefaultAsync();




            if (nannyResume == null)
            {
                return NotFound();
            }

            Console.WriteLine("Photo URL: " + nannyResume.Photo);


            return nannyResume;
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

            return CreatedAtAction(nameof(GetNannyResume), new { id = nannyResume.Id }, nannyResumeDTO);
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
