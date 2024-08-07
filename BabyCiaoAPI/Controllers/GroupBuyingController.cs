﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BabyCiaoAPI.Models;
using BabyCiaoAPI.DTO;
using Microsoft.AspNetCore.Cors;

namespace BabyCiaoAPI.Controllers
{
    [EnableCors("andy")]
    [Route("api/[controller]")]
    [ApiController]
    public class GroupBuyingController : ControllerBase
    {
        private readonly BabyciaoContext _context;

        public GroupBuyingController(BabyciaoContext context)
        {
            _context = context;
        }

        // GET: api/GroupBuying
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GBDTO>>> GetGroupBuyings()
        {
			var groupBuys = await(from gb in _context.GroupBuyings
                                  where gb.Display
                                  join gbp in _context.GroupBuyingPhotos on gb.Id equals gbp.IdGroupBuying into pp
							from gbp in pp.OrderBy(p => p.PhotoName).Take(1).DefaultIfEmpty()
							select new GBDTO
							{
								Id = gb.Id,
								ProductName = gb.ProductName,
								ProductDescription = gb.ProductDescription,
								TargetCount = gb.TargetCount,
								Statement = gb.Statement,
								ModifiedTime = gb.ModifiedTime,
								ModifiedTimeView = gb.ModifiedTime.ToString("yyyy-MM-dd"),
								Display = gb.Display,
								DisplayString = gb.Display ? "☑" : "",
								//ProductType= gb.ProductType,        <<這裡API請確保更新API的媽斗~~~~~>>
								JoinQuantity = _context.GroupBuyingDetails.Where(id => id.GroupBuyingId == gb.Id).Sum(q => q.Quantity),
								photoUrl = gbp.PhotoName /*!= null ? $"<img src=\" /uploads/{gbp.PhotoName}\" width=\"100\" />" : "<img src=\" /img/noImage.jpg\" width=\"100\" />"*/,

                            }).ToListAsync();
			return Ok(groupBuys);
        }

        // GET: api/GroupBuying/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GroupBuying>> GetGroupBuying(int id)
        {
            var groupBuying = await _context.GroupBuyings.FindAsync(id);

            if (groupBuying == null)
            {
                return NotFound();
            }

            return groupBuying;
        }

        // PUT: api/GroupBuying/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGroupBuying(int id, GroupBuying groupBuying)
        {
            if (id != groupBuying.Id)
            {
                return BadRequest();
            }

            _context.Entry(groupBuying).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupBuyingExists(id))
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

        // POST: api/GroupBuying
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GroupBuying>> PostGroupBuying(GroupBuying groupBuying)
        {
            _context.GroupBuyings.Add(groupBuying);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGroupBuying", new { id = groupBuying.Id }, groupBuying);
        }

        // DELETE: api/GroupBuying/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroupBuying(int id)
        {
            var groupBuying = await _context.GroupBuyings.FindAsync(id);
            if (groupBuying == null)
            {
                return NotFound();
            }

            _context.GroupBuyings.Remove(groupBuying);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GroupBuyingExists(int id)
        {
            return _context.GroupBuyings.Any(e => e.Id == id);
        }
    }
}
