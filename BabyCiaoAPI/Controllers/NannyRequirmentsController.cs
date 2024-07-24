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
    public class NannyRequirmentsController : ControllerBase
    {
        private readonly BabyciaoContext _context;

        public NannyRequirmentsController(BabyciaoContext context)
        {
            _context = context;
        }

        // GET: api/NannyRequirments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NannyRequirment>>> GetNannyRequirments()
        {
            return await _context.NannyRequirments.ToListAsync();
        }

        // GET: api/NannyRequirments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NannyRequirment>> GetNannyRequirment(int id)
        {
            var nannyRequirment = await _context.NannyRequirments.FindAsync(id);

            if (nannyRequirment == null)
            {
                return NotFound();
            }

            return nannyRequirment;
        }

        // PUT: api/NannyRequirments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNannyRequirment(int id, NannyRequirment nannyRequirment)
        {
            if (id != nannyRequirment.Id)
            {
                return BadRequest();
            }

            _context.Entry(nannyRequirment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NannyRequirmentExists(id))
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

        // POST: api/NannyRequirments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NannyRequirment>> PostNannyRequirment(NannyRequirment nannyRequirment)
        {
            _context.NannyRequirments.Add(nannyRequirment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNannyRequirment", new { id = nannyRequirment.Id }, nannyRequirment);
        }

        // DELETE: api/NannyRequirments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNannyRequirment(int id)
        {
            var nannyRequirment = await _context.NannyRequirments.FindAsync(id);
            if (nannyRequirment == null)
            {
                return NotFound();
            }

            _context.NannyRequirments.Remove(nannyRequirment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NannyRequirmentExists(int id)
        {
            return _context.NannyRequirments.Any(e => e.Id == id);
        }
    }
}
