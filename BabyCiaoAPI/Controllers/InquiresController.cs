//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using BabyCiaoAPI.Models;
//using Microsoft.EntityFrameworkCore;

//namespace BabyCiaoAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class InquiresController : ControllerBase
//    {
//        private readonly BabyciaoContext _context;

//        public InquiresController(BabyciaoContext context)
//        {
//            _context = context;
//        }

//        // GET: api/Inquires
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<InquireDTO>>> GetInquires()
//        {
//            var inquires = await _context.Inquires
//                .Include(i => i.UserAccountinquireNavigation)
//                .Include(i => i.UserAccountresponseNavigation)
//                .ToListAsync();

//            return Ok(inquires);
//        }

//        // GET: api/Inquires/5
//        [HttpGet("{userAccountResponse}/{userAccountInquire}")]
//        public async Task<ActionResult<InquireDTO>> GetInquire(string userAccountResponse, string userAccountInquire)
//        {
//            var inquire = await _context.Inquires
//                .Include(i => i.UserAccountinquireNavigation)
//                .Include(i => i.UserAccountresponseNavigation)
//                .FirstOrDefaultAsync(i => i.UserAccountresponse == userAccountResponse && i.UserAccountinquire == userAccountInquire);

//            if (inquire == null)
//            {
//                return NotFound(new { Message = "Inquire not found" });
//            }

//            return Ok(inquire);
//        }

//        // POST: api/Inquires
//        public async Task<ActionResult<InquireDTO>> PostInquire(InquireDTO inquireDTO)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            try
//            {
//                // 转换 DTO 为实体模型
//                var inquire = new Inquire
//                {
//                    // 假设 Inquire 和 InquireDTO 有相同的属性
//                    // 需要手动映射所有属性
//                    UserAccountresponse = inquireDTO.UserAccountresponse,
//                    UserAccountinquire = inquireDTO.UserAccountinquire,
//                    // 其他属性映射...
//                };

//                _context.Inquires.Add(inquire);
//                await _context.SaveChangesAsync();

//                // 你可以在这里创建一个新的 InquireDTO 用于返回，或者直接返回 inquireDTO
//                var createdInquireDTO = new InquireDTO
//                {
//                    UserAccountresponse = inquire.UserAccountresponse,
//                    UserAccountinquire = inquire.UserAccountinquire,
//                    // 其他属性映射...
//                };

//                return CreatedAtAction(nameof(GetInquire), new { userAccountResponse = inquire.UserAccountresponse, userAccountInquire = inquire.UserAccountinquire }, createdInquireDTO);
//            }
//            catch (DbUpdateException ex)
//            {
//                return StatusCode(500, new { Message = "An error occurred while creating the inquire", Details = ex.Message });
//            }
//        }

//        // PUT: api/Inquires/5
//        [HttpPut("{userAccountResponse}/{userAccountInquire}")]
//        public async Task<IActionResult> PutInquire(string userAccountResponse, string userAccountInquire, InquireDTO inquireDTO)
//        {
//            if (userAccountResponse != inquireDTO.UserAccountresponse || userAccountInquire != inquireDTO.UserAccountinquire)
//            {
//                return BadRequest(new { Message = "Inquire account mismatch" });
//            }

//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            _context.Entry(inquireDTO).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!InquireExists(userAccountResponse, userAccountInquire))
//                {
//                    return NotFound(new { Message = "Inquire not found" });
//                }
//                else
//                {
//                    throw;
//                }
//            }
//            catch (DbUpdateException ex)
//            {
//                return StatusCode(500, new { Message = "An error occurred while updating the inquire", Details = ex.Message });
//            }

//            return NoContent();
//        }

//        // DELETE: api/Inquires/5
//        [HttpDelete("{userAccountResponse}/{userAccountInquire}")]
//        public async Task<IActionResult> DeleteInquire(string userAccountResponse, string userAccountInquire)
//        {
//            var inquire = await _context.Inquires
//                .FirstOrDefaultAsync(i => i.UserAccountresponse == userAccountResponse && i.UserAccountinquire == userAccountInquire);
//            if (inquire == null)
//            {
//                return NotFound(new { Message = "Inquire not found" });
//            }

//            try
//            {
//                _context.Inquires.Remove(inquire);
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateException ex)
//            {
//                return StatusCode(500, new { Message = "An error occurred while deleting the inquire", Details = ex.Message });
//            }

//            return NoContent();
//        }

//        private bool InquireExists(string userAccountResponse, string userAccountInquire)
//        {
//            return _context.Inquires.Any(e => e.UserAccountresponse == userAccountResponse && e.UserAccountinquire == userAccountInquire);
//        }
//    }
//}
