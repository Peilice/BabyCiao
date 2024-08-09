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
using System.ComponentModel;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Globalization;
using System.Numerics;
using System.Security.Cryptography;
using System.IO;

namespace BabyCiaoAPI.Controllers
{
    [EnableCors("andy")]
    [Route("api/[controller]")]
    [ApiController]
    public class BabyResumesController : ControllerBase
    {
        private readonly BabyciaoContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _targetRootPath = @"C:\users\user\desktop\babyciao-main2\babyciao\wwwroot\nannnyandperant\babyreume";

        public BabyResumesController(BabyciaoContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
        }

        //[HttpGet("NAB")]
        //public async Task<ActionResult<IEnumerable<NABDTO>>> NAB()
        //{ 
        //var babyResume = await _context.BabyResumes
        //        .Where(n=>n.Display)
        //        .ToListAsync();
        //}

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BabyResume>>> GetBabyResume()
        {
            return await _context.BabyResumes.Select(c => new BabyResume
            {
                FirstName = c.FirstName,
                City = c.City,
                District = c.District,
                RequireDate = c.RequireDate,
                TypeOfDaycare = c.TypeOfDaycare,
                TimeSlot = c.TimeSlot,
                Photo = c.Photo,
            }).ToListAsync();
        }

        [HttpGet("Fullinformation")]
        public async Task<ActionResult<IEnumerable<BabyResumeDTO>>> GetFullinformation()
        {
            try
            {
                var resume = await _context.BabyResumes.Select(c => new BabyResumeDTO
                {
                    Id = c.Id,
                    AccountUserAccount = c.AccountUserAccount,
                    Photo = c.Photo,
                    FirstName = c.FirstName,
                    City = c.City,
                    District = c.District,
                    ApplyDate = c.ApplyDate,
                    RequireDate = c.RequireDate,
                    Babyage = c.Babyage,
                    TypeOfDaycare = c.TypeOfDaycare,
                    TimeSlot = c.TimeSlot,
                    Memo = c.Memo,
                    Display = c.Display
                }).ToListAsync();

                return Ok(resume);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }
        // GET: api/BabyResumes/Fullinformation/5
        [HttpGet("Fullinformation/{id}")]
        public async Task<ActionResult<BabyResumeDTO>> GetFullinformation(int id)
        {
            try
            {
                // 查找指定 Id 的 BabyResume 对象
                var resume = await _context.BabyResumes
                    .Where(c => c.Id == id)
                    .Select(c => new BabyResumeDTO
                    {
                        Id = c.Id,
                        AccountUserAccount = c.AccountUserAccount,
                        Photo = c.Photo,
                        FirstName = c.FirstName,
                        City = c.City,
                        District = c.District,
                        ApplyDate = c.ApplyDate,
                        RequireDate = c.RequireDate,
                        Babyage = c.Babyage,
                        TypeOfDaycare = c.TypeOfDaycare,
                        TimeSlot = c.TimeSlot,
                        Memo = c.Memo,
                        Display = c.Display
                    })
                    .FirstOrDefaultAsync();

                if (resume == null)
                {
                    return NotFound(); // 如果没有找到，返回 404 Not Found
                }

                return Ok(resume); // 返回找到的对象
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error"); // 返回 500 Internal Server Error
            }
        }

        [HttpGet("Filter")]
        public async Task<ActionResult<IEnumerable<BabyResumeDTO>>> GetFilteredResumes(
            string AccountUserAccount,
            string FirstName,
            string City,
            string District,
            DateOnly? ApplyDate,
            DateOnly? RequireDate,
            int? Babyage,
            string TimeSlot,
            string TypeOfDaycare)
        {
            try
            {
                var query = _context.BabyResumes.AsQueryable();

                // Filtering conditions
                if (!string.IsNullOrWhiteSpace(AccountUserAccount))
                {
                    query = query.Where(a => a.AccountUserAccount == AccountUserAccount);
                }

                if (!string.IsNullOrWhiteSpace(FirstName))
                {
                    query = query.Where(a => a.FirstName.Contains(FirstName));
                }

                if (!string.IsNullOrWhiteSpace(City))
                {
                    query = query.Where(a => a.City.Contains(City));
                }

                if (!string.IsNullOrWhiteSpace(District))
                {
                    query = query.Where(a => a.District.Contains(District));
                }

                if (Babyage.HasValue && Babyage > 0) // Ensure Babyage is greater than 0
                {
                    query = query.Where(a => a.Babyage == Babyage.Value.ToString());
                }

                if (!string.IsNullOrWhiteSpace(TimeSlot))
                {
                    query = query.Where(a => a.TimeSlot.Contains(TimeSlot));
                }

                if (!string.IsNullOrWhiteSpace(TypeOfDaycare))
                {
                    query = query.Where(a => a.TypeOfDaycare.Contains(TypeOfDaycare));
                }

                if (ApplyDate.HasValue)
                {
                    query = query.Where(a => a.ApplyDate == ApplyDate.Value);
                }

                if (RequireDate.HasValue)
                {
                    query = query.Where(a => a.RequireDate == RequireDate.Value);
                }

                // Execute the query and convert to DTO
                var resume = await query.Select(a => new BabyResumeDTO
                {
                    Id = a.Id,
                    AccountUserAccount = a.AccountUserAccount,
                    Photo = a.Photo,
                    FirstName = a.FirstName,
                    City = a.City,
                    District = a.District,
                    ApplyDate = a.ApplyDate,
                    RequireDate = a.RequireDate,
                    Babyage = a.Babyage.ToString(),
                    TypeOfDaycare = a.TypeOfDaycare,
                    TimeSlot = a.TimeSlot,
                    Memo = a.Memo,
                    Display = a.Display
                }).ToListAsync();

                return Ok(resume);
            }
            catch (Exception ex)
            {
                // Return error message
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        // GET: api/BabyResumes/GetPicture/5
        [HttpGet("GetPicture/{id}")]
        public async Task<IActionResult> GetPicture(int id)
        {
            var babyResume = await _context.BabyResumes.FindAsync(id);
            if (babyResume == null || string.IsNullOrEmpty(babyResume.Photo))
            {
                return NotFound();
            }

            // 使用絕對路徑
            var imagePath = Path.Combine(_targetRootPath, babyResume.Photo);
            if (!System.IO.File.Exists(imagePath))
            {
                return NotFound();
            }

            var imageContent = await System.IO.File.ReadAllBytesAsync(imagePath);
            return File(imageContent, "image/jpeg");
        }

        [HttpPost("Upload")]
        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
            {
                return BadRequest("No files were uploaded.");
            }

            // 使用絕對路徑
            if (!Directory.Exists(_targetRootPath))
            {
                Directory.CreateDirectory(_targetRootPath);
            }

            var uploadedFiles = new List<string>();
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var filePath = Path.Combine(_targetRootPath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    uploadedFiles.Add(fileName);
                }
            }

            return Ok(new { UploadedFiles = uploadedFiles });
        }



        // PUT: api/BabyResumes/5
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
        [HttpPost]
        public async Task<ActionResult<BabyResume>> PostBabyResume(BabyResume babyResume)
        {
            try
            {
                _context.BabyResumes.Add(babyResume);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetBabyResume", new { id = babyResume.Id }, babyResume);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
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