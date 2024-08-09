using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BabyCiaoAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using BabyCiaoAPI.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Humanizer;
using NuGet.Protocol;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BabyCiaoAPI.Controllers
{
    [EnableCors("andy")]
    [Route("api/[controller]")]
    [ApiController]
    public class NannyRequirmentsController : ControllerBase
    {
        private readonly BabyciaoContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly IHttpContextAccessor _contextAccessor;
        


        public NannyRequirmentsController(BabyciaoContext context, IWebHostEnvironment environment, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _environment = environment;
            _contextAccessor = contextAccessor;
        }




        [HttpGet("NannyRequirmentget")]
        public async Task<ActionResult<IEnumerable<NannyRequirmentDTO>>> NannyRequirmentget()
        {
            string username = _contextAccessor.HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Name);

            bool check = _context.NannyRequirments.Any(c => c.NannyAccountUserAccount == username);

            if (check)
            {
                var results = _context.NannyRequirments
                    .Where(c => c.NannyAccountUserAccount == username)
                    .Select(nny => new NannyRequirmentNEWDTO
                    {
                        NannyAccountUserAccount = nny.NannyAccountUserAccount,
                        PoliceCriminalRecordCertificate = nny.PoliceCriminalRecordCertificate,
                        ChildCareCertificate = nny.ChildCareCertificate,
                        NationalIdentificationCard = nny.NationalIdentificationCard,
                        AddressesOfAgencies = nny.AddressesOfAgencies,
                        ValidPeriodsOfCertificates = nny.ValidPeriodsOfCertificates
                    })
                    .ToList();

                return Ok(results); // Return the results with a 200 OK status
            }
            else
            {
                return Ok(new List<NannyRequirmentDTO>()); // Return an empty list instead of null
            }
        }





        // GET: api/NannyRequirments/5
        [HttpGet("GetNannyRequirmentinfo/{id}")]
        public async Task<ActionResult<NannyRequirment>> GetNannyRequirmentinfo(int id)
        {
            var nannyRequirment = await _context.NannyRequirments.FindAsync(id);

            if (nannyRequirment == null)
            {
                return NotFound();
            }

            return nannyRequirment;
        }



        [HttpPost("apply")]
        public async Task<string>  Apply ([FromBody]NannyRequirmentDTO dto)
        {
            var nannyRequirment = new NannyRequirment
            {   
                NannyAccountUserAccount = dto.NannyAccountUserAccount,
                PoliceCriminalRecordCertificate = dto.PoliceCriminalRecordCertificates,
                ChildCareCertificate = dto.ChildCareCertificates,
                NationalIdentificationCard = dto.NationalIdentificationCards,
                AddressesOfAgencies = dto.AddressesOfAgencies,
                ValidPeriodsOfCertificates = dto.ValidPeriodsOfCertificates,
            };
            try
            {
                _context.NannyRequirments.Add(nannyRequirment);
                   await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return "Ok";
        }



        //讀取個人報名的活動(讀取)
        //Get api/OnlineCompetitions/MyCompetition/{account}
        [HttpGet("MyNanny/{account}")]
        public async Task<ActionResult<IEnumerable<CompetitionDetailDTO>>> MyNanny(string account)
        {
            //讀取參加過的比賽資訊(List)
            var myResume = await (from com in _context.NannyResumes
                                       join comd in _context.NannyRequirments
                                       on com.NannyAccountUserAccount equals comd.NannyAccountUserAccount
                                       where comd.NannyAccountUserAccount == account
                                       select new NannyRequirment_NEWDTO
                                       {
                                           Id = com.Id,
                                           Nickname = com.Nickname,
                                           NannyAccountUserAccount = comd.NannyAccountUserAccount,
                                           PoliceCriminalRecordCertificate = comd.PoliceCriminalRecordCertificate,
                                           ChildCareCertificate = comd.ChildCareCertificate,
                                           NationalIdentificationCard = comd.NationalIdentificationCard,
                                           AddressesOfAgencies = comd.AddressesOfAgencies,
                                           ValidPeriodsOfCertificates = comd.ValidPeriodsOfCertificates,
                                           City = com.City,
                                           Introduction = com.Introduction,
                                           TypeOfDaycare = com.TypeOfDaycare,
                                           ServiceType = com.ServiceItems,
                                           QuasiPublicChildcare = com.QuasiPublicChildcare,
                                           ChildcareAvailableUnder2 = com.ChildcareAvailableUnder2,
                                           ChildcareAvailableOver2 = com.ChildcareAvailableOver2,
                                           ServiceCenter = com.ServiceCenter,
                                           ProfessionalPortrait = com.ProfessionalPortrait,
                                       }).ToListAsync();
            return Ok(myResume);

        }
        ////讀取個人報名的活動(讀取)
        ////Get api/OnlineCompetitions/MyCompetition/{account}
        //[HttpGet("MyResume/{account}")]
        //public async Task<ActionResult<IEnumerable<CompetitionDetailDTO>>> MyResume(string account)
        //{
        //    //讀取參加過的比賽資訊(List)
        //    var myResume = await (from com in _context.BabyResumes
        //                          join comd in _context.NannyRequirments
        //                          on com.AccountUserAccount equals comd.NannyAccountUserAccount
        //                          where comd.NannyAccountUserAccount == account
        //                          select new NannyRequirmentNEWDTO
        //                          {
        //                              Id = com.Id,
        //                              Nickname = com.Nickname,
        //                              NannyAccountUserAccount = comd.NannyAccountUserAccount,
        //                              PoliceCriminalRecordCertificate = comd.PoliceCriminalRecordCertificate,
        //                              ChildCareCertificate = comd.ChildCareCertificate,
        //                              NationalIdentificationCard = comd.NationalIdentificationCard,
        //                              AddressesOfAgencies = comd.AddressesOfAgencies,
        //                              ValidPeriodsOfCertificates = comd.ValidPeriodsOfCertificates,
        //                              City = com.City,
        //                              Introduction = com.Introduction,
        //                              TypeOfDaycare = com.TypeOfDaycare,
        //                              ServiceType = com.ServiceItems,
        //                              QuasiPublicChildcare = com.QuasiPublicChildcare,
        //                              ChildcareAvailableUnder2 = com.ChildcareAvailableUnder2,
        //                              ChildcareAvailableOver2 = com.ChildcareAvailableOver2,
        //                              ServiceCenter = com.ServiceCenter,
        //                              ProfessionalPortrait = com.ProfessionalPortrait,
        //                          }).ToListAsync();
        //    return Ok(myResume);

        //}



        //[HttpPost("UploadFile")]
        //public async Task<IActionResult> UploadFile(IFormFile file, string fileType)
        //{
        //    if (file == null || file.Length == 0)
        //    {
        //        return BadRequest("No file uploaded.");
        //    }

        //    // 根據 fileType 選擇檔案夾
        //    string folderPath = fileType switch
        //    {
        //        "policeCriminalRecordCertificate" => "良民證",
        //        "childCareCertificate" => "保母證",
        //        "nationalIdentificationCard" => "身分證",
        //        _ => throw new ArgumentException("Invalid file type.")
        //    };

        //    var uploadsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "NannnyAndPerant", folderPath);

        //    if (!Directory.Exists(uploadsDirectory))
        //    {
        //        Directory.CreateDirectory(uploadsDirectory);
        //    }

        //    var filePath = Path.Combine(uploadsDirectory, file.FileName);

        //    try
        //    {
        //        using (var stream = new FileStream(filePath, FileMode.Create))
        //        {
        //            await file.CopyToAsync(stream);
        //        }

        //        // 返回相對於 wwwroot 的 URL
        //        return Ok(new { FilePath = $"/NannnyAndPerant/{folderPath}/{file.FileName}" });
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception and return an error response
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}





        [HttpPost("PostNannyRequirmentinfo")]
        public async Task<string> PostNannyRequirmentinfo([FromBody] NannyRequirmentNEWDTO dto)
        {
            try {
                var nannyRequirment = new NannyRequirment
                {
                    NannyAccountUserAccount = dto.NannyAccountUserAccount,
                    AddressesOfAgencies = dto.AddressesOfAgencies,
                    ValidPeriodsOfCertificates = dto.ValidPeriodsOfCertificates,
                    PoliceCriminalRecordCertificate = dto.PoliceCriminalRecordCertificate,
                    ChildCareCertificate = dto.ChildCareCertificate,
                    NationalIdentificationCard = dto.NationalIdentificationCard,

                };
            }

            catch (Exception ex)
            {
                return ex.ToString();
            }

            return "Ok";
        
        }

        //[HttpPost("PostNannyRequirmentinfo")]
        //    public async Task<IActionResult> PostNannyRequirmentinfo(
        // [FromForm] string nannyAccountUserAccount,
        // [FromForm] string addressesOfAgencies,
        // [FromForm] DateOnly validPeriodsOfCertificates,
        // [FromForm] IFormFile policeCriminalRecordCertificate,
        // [FromForm] IFormFile childCareCertificate,
        // [FromForm] IFormFile nationalIdentificationCard)
        //    {
        //        // 儲存文件並獲取文件路徑
        //        string policeCriminalRecordCertificatePath = await SaveFileAsync(policeCriminalRecordCertificate, "Uploads/PoliceCriminalRecordCertificates");
        //        string childCareCertificatePath = await SaveFileAsync(childCareCertificate, "Uploads/ChildCareCertificates");
        //        string nationalIdentificationCardPath = await SaveFileAsync(nationalIdentificationCard, "Uploads/NationalIdentificationCards");

        //        // 建立 DTO 並填充數據
        //        var dto = new NannyRequirmentDTO
        //        {
        //            NannyAccountUserAccount = nannyAccountUserAccount,
        //            AddressesOfAgencies = addressesOfAgencies,
        //            ValidPeriodsOfCertificates = validPeriodsOfCertificates,
        //            PoliceCriminalRecordCertificatePath = policeCriminalRecordCertificatePath,
        //            ChildCareCertificatePath = childCareCertificatePath,
        //            NationalIdentificationCardPath = nationalIdentificationCardPath
        //        };

        //        // 儲存到數據庫
        //        var nannyRequirment = new NannyRequirment
        //        {
        //            NannyAccountUserAccount = dto.NannyAccountUserAccount,
        //            AddressesOfAgencies = dto.AddressesOfAgencies,
        //            ValidPeriodsOfCertificates = dto.ValidPeriodsOfCertificates,
        //            PoliceCriminalRecordCertificatePath = dto.PoliceCriminalRecordCertificatePath,
        //            ChildCareCertificatePath = dto.ChildCareCertificatePath,
        //            NationalIdentificationCardPath = dto.NationalIdentificationCardPath
        //        };

        //        _context.NannyRequirments.Add(nannyRequirment);
        //        await _context.SaveChangesAsync();

        //        return CreatedAtAction(nameof(GetNannyRequirmentinfo), new { id = nannyRequirment.Id }, dto);
        //    }

        //    private async Task<string> SaveFileAsync(IFormFile file, string directory)
        //    {
        //        if (file == null || file.Length == 0)
        //        {
        //            return null;
        //        }

        //        var filePath = Path.Combine(Directory.GetCurrentDirectory(), directory, file.FileName);
        //        using (var stream = new FileStream(filePath, FileMode.Create))
        //        {
        //            await file.CopyToAsync(stream);
        //        }
        //        return filePath;
        //    }





        //[HttpPost("PostNannyRequirmentinfo")]
        //public async Task<ActionResult<NannyRequirmentDTO>> PostNannyRequirmentinfo([FromBody] NannyRequirmentDTO dto)
        //{
        //    Console.WriteLine("Received request for PostNannyRequirmentinfo");

        //    var nannyRequirment = new NannyRequirment
        //    {
        //        Id = dto.Id,
        //        RequirementDate = dto.RequirementDate,
        //        NannyAccountUserAccount = dto.NannyAccountUserAccount,
        //        PoliceCriminalRecordCertificate = dto.PoliceCriminalRecordCertificate,
        //        ChildCareCertificate = dto.ChildCareCertificate,
        //        NationalIdentificationCard = dto.NationalIdentificationCard,
        //        AddressesOfAgencies = dto.AddressesOfAgencies,
        //        ValidPeriodsOfCertificates = dto.ValidPeriodsOfCertificates,
        //        Statement = dto.Statement
        //    };

        //    _context.NannyRequirments.Add(nannyRequirment);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction(nameof(GetNannyRequirmentinfo), new { id = nannyRequirment.Id }, new NannyRequirmentDTO
        //    {
        //        RequirementDate = nannyRequirment.RequirementDate,
        //        NannyAccountUserAccount = nannyRequirment.NannyAccountUserAccount,
        //        PoliceCriminalRecordCertificate = nannyRequirment.PoliceCriminalRecordCertificate,
        //        ChildCareCertificate = nannyRequirment.ChildCareCertificate,
        //        NationalIdentificationCard = nannyRequirment.NationalIdentificationCard,
        //        AddressesOfAgencies = nannyRequirment.AddressesOfAgencies,
        //        ValidPeriodsOfCertificates = nannyRequirment.ValidPeriodsOfCertificates,
        //        Statement = nannyRequirment.Statement
        //    });
        //}




        // PUT: api/NannyRequirments/5
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
