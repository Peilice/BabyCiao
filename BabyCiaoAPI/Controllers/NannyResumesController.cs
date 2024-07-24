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
    public class NannyResumesController : ControllerBase
    {
        private readonly BabyciaoContext _context;

        public NannyResumesController(BabyciaoContext context)
        {
            _context = context;
        }

        // GET: api/NannyResumes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NannyResume>>> GetNannyResumes()
        {
            return await _context.NannyResumes.ToListAsync();
        }

        // GET: api/NannyResumes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NannyResume>> GetNannyResume(int id)
        {
            var nannyResume = await _context.NannyResumes.FindAsync(id);

            if (nannyResume == null)
            {
                return NotFound();
            }

            return nannyResume;
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
