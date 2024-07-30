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
    public class EvaluatesController : ControllerBase
    {
        private readonly BabyciaoContext _context;

        public EvaluatesController(BabyciaoContext context)
        {
            _context = context;
        }

        // GET: api/Evaluates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Evaluate>>> GetEvaluates()
        {
            return await _context.Evaluates.ToListAsync();
        }

        // GET: api/Evaluates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Evaluate>> GetEvaluate(int id)
        {
            var evaluate = await _context.Evaluates.FindAsync(id);

            if (evaluate == null)
            {
                return NotFound();
            }

            return evaluate;
        }

        // PUT: api/Evaluates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvaluate(int id, Evaluate evaluate)
        {
            if (id != evaluate.Id)
            {
                return BadRequest();
            }

            _context.Entry(evaluate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EvaluateExists(id))
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

        // POST: api/Evaluates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Evaluate>> PostEvaluate(Evaluate evaluate)
        {
            _context.Evaluates.Add(evaluate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEvaluate", new { id = evaluate.Id }, evaluate);
        }

        // DELETE: api/Evaluates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvaluate(int id)
        {
            var evaluate = await _context.Evaluates.FindAsync(id);
            if (evaluate == null)
            {
                return NotFound();
            }

            _context.Evaluates.Remove(evaluate);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EvaluateExists(int id)
        {
            return _context.Evaluates.Any(e => e.Id == id);
        }
    }
}
