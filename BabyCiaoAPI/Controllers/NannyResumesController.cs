using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BabyCiaoAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using BabyCiao.Models;
using Microsoft.Extensions.Hosting;

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
        [HttpGet("GetNannyResumes")]
        public async Task<ActionResult<IEnumerable<NannyResume>>> GetNannyResumes()
        {
            return await _context.NannyResumes.Select(c => new NannyResume
            {
                City=c.City,
                District=c.District,
                Introduction=c.Introduction,
                TypeOfDaycare=c.TypeOfDaycare,
                ServiceItems=c.ServiceItems,
                QuasiPublicChildcare=c.QuasiPublicChildcare,
                ChildcareAvailableUnder2=c.ChildcareAvailableUnder2,
                ChildcareAvailableOver2=c.ChildcareAvailableOver2,
                Language=c.Language,
                ProfessionalPortrait=c.ProfessionalPortrait,
            }).ToListAsync();
        }

        // GET: api/NannyResumes/5
        [HttpGet("GetNannyResumeinfo")]
        public async Task<ActionResult<IEnumerable<NannyResumeDTO>>> GetNannyResumeinfo()
        {
            try
            {
                var resumes = await _context.NannyResumes.ToListAsync();

                var resumeDTOs = resumes.Select(c => new NannyResumeDTO
                {
                    Id = c.Id,
                    Nickname = c.Nickname,
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
                    DisplayControl = c.DisplayControl,
                }).ToList();

                if (resumeDTOs == null || resumeDTOs.Count == 0)
                {
                    return NotFound();
                }

                return Ok(resumeDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        // GET: api/NannyResumes/GetNannyResumeinfo/5
        [HttpGet("GetNannyResumeinfo/{Id}")]
        public async Task<ActionResult<NannyResumeDTO>> GetNannyResumeinfo(int Id)
        {
            try
            {
                // 查找指定 Id 的 NannyResume 对象
                var resume = await _context.NannyResumes
                    .Where(c => c.Id == Id)
                    .FirstOrDefaultAsync();

                if (resume == null)
                {
                    return NotFound(); // 如果没有找到，返回 404 Not Found
                }

                // 将 NannyResume 对象映射到 NannyResumeDTO 对象
                var resumeDTO = new NannyResumeDTO
                {
                    Id = resume.Id,
                    Nickname=resume.Nickname,
                    NannyAccountUserAccount = resume.NannyAccountUserAccount,
                    City = resume.City,
                    District = resume.District,
                    Introduction = resume.Introduction,
                    TypeOfDaycare = resume.TypeOfDaycare,
                    ServiceItems = resume.ServiceItems,
                    QuasiPublicChildcare = resume.QuasiPublicChildcare,
                    ChildcareAvailableUnder2 = resume.ChildcareAvailableUnder2, 
                    ChildcareAvailableOver2 = resume.ChildcareAvailableOver2, // Assuming int, converting to string
                    Language = resume.Language,
                    ServiceCenter = resume.ServiceCenter,
                    ProfessionalPortrait = resume.ProfessionalPortrait,
                    DisplayControl = resume.DisplayControl  // Assuming boolean, converting to string
                };

                return Ok(resumeDTO); // 返回指定 Id 的 DTO
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        // GET: api/NannyResumes/GetNannyResumeinfoJoin
        [HttpGet("GetNannyResumeinfoJoin")]
        public async Task<ActionResult<IEnumerable<NannyResumeDetailDTO>>> GetNannyResumeinfoJoin()
        {
            try
            {
                // 查找所有 NannyResume 对象
                var resumes = await _context.NannyResumes.ToListAsync();

                if (!resumes.Any())
                {
                    return NotFound(); // 如果没有找到记录，返回404
                }

                // 获取相关的评价和询问数据
                var evaluations = await _context.Evaluates.ToListAsync();
                var inquiries = await _context.Inquires.ToListAsync();

                // 在内存中处理数据转换
                var resumeDTOs = resumes.Select(resume => new NannyResumeDetailDTO
                {
                    Id = resume.Id,
                    NannyAccountUserAccount = resume.NannyAccountUserAccount,
                    Nickname = resume.Nickname,
                    City = resume.City,
                    District = resume.District,
                    Introduction = resume.Introduction,
                    TypeOfDaycare = resume.TypeOfDaycare,
                    ServiceType = resume.ServiceType,
                    ServiceItems = resume.ServiceItems switch
                    {
                        0 => "無",
                        1 => "料理服務",
                        2 => "接送服務",
                        3 => "家教服務",
                        _ => "未知"
                    },
                    QuasiPublicChildcare = resume.QuasiPublicChildcare,
                    ChildcareAvailableUnder2 = resume.ChildcareAvailableUnder2,
                    ChildcareAvailableOver2 = resume.ChildcareAvailableOver2,
                    Language = resume.Language,
                    ServiceCenter = resume.ServiceCenter,
                    ProfessionalPortrait = resume.ProfessionalPortrait,
                    DisplayControl = resume.DisplayControl.HasValue && resume.DisplayControl.Value,
                    Evaluations = evaluations
                        .Where(e => e.AppraiseeUserAccount == resume.NannyAccountUserAccount)
                        .Select(e => new EvaluateDTO
                        {
                            Id = e.Id,
                            EvaluatorUserAccount = e.EvaluatorUserAccount,
                            AppraiseeUserAccount = e.AppraiseeUserAccount,
                            EvaluateTime = e.EvaluateTime,
                            Score = e.Score,
                            Memo = e.Memo,
                            Display = e.Display
                        }).ToList(),
                    Inquiries = inquiries
                        .Where(i => i.UserAccountresponse == resume.NannyAccountUserAccount)
                        .Select(i => new InquireDTO
                        {
                            UserAccountresponse = i.UserAccountresponse,
                            UserAccountinquire = i.UserAccountinquire,
                            Times = i.Times
                        }).ToList()
                }).ToList();

                return Ok(resumeDTOs);
            }
            catch (Exception ex)
            {
                // 记录异常信息
                Console.WriteLine(ex.Message);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet("GetNannyResumeinfoJoin/{id}")]
        public async Task<ActionResult<NannyResumeDetailDTO>> GetNannyResumeinfoJoin(int id)
        {
            try
            {
                // 获取所有相关数据
                var resume = await _context.NannyResumes
                    .Where(n => n.Id == id)
                    .FirstOrDefaultAsync();

                if (resume == null)
                {
                    return NotFound(); // 如果没有找到，返回 404 Not Found
                }

                var evaluations = await _context.Evaluates
                    .Where(e => e.AppraiseeUserAccount == resume.NannyAccountUserAccount)
                    .ToListAsync();

                var inquiries = await _context.Inquires
                    .Where(i => i.UserAccountinquire == resume.NannyAccountUserAccount)
                    .ToListAsync();

                // 使用 switch 在内存中处理数据
                var resumeDTO = new NannyResumeDetailDTO
                {
                    Id = resume.Id,
                    NannyAccountUserAccount = resume.NannyAccountUserAccount,
                    Nickname = resume.Nickname,
                    City = resume.City,
                    District = resume.District,
                    Introduction = resume.Introduction,
                    TypeOfDaycare = resume.TypeOfDaycare,
                    ServiceType = resume.ServiceType,
                    ServiceItems = resume.ServiceItems switch
                    {
                        0 => "無",
                        1 => "料理服務",
                        2 => "接送服務",
                        3 => "家教服務",
                        _ => "未知"
                    },
                    QuasiPublicChildcare = resume.QuasiPublicChildcare,
                    ChildcareAvailableUnder2 = resume.ChildcareAvailableUnder2,
                    ChildcareAvailableOver2 = resume.ChildcareAvailableOver2 ,
                    Language = resume.Language,
                    ServiceCenter = resume.ServiceCenter,
                    ProfessionalPortrait = resume.ProfessionalPortrait,
                    DisplayControl = resume.DisplayControl.HasValue && resume.DisplayControl.Value,
                    Evaluations = evaluations.Select(e => new EvaluateDTO
                    {
                        Id = e.Id,
                        EvaluatorUserAccount = e.EvaluatorUserAccount,
                        AppraiseeUserAccount = e.AppraiseeUserAccount,
                        EvaluateTime = e.EvaluateTime,
                        Score = e.Score,
                        Memo = e.Memo,
                        Display = e.Display
                    }).ToList(),
                    Inquiries = inquiries.Select(i => new InquireDTO
                    {
                        UserAccountresponse = i.UserAccountresponse,
                        UserAccountinquire = i.UserAccountinquire,
                        Times = i.Times
                    }).ToList()
                };

                return Ok(resumeDTO);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex.Message);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/Contact/5
        [HttpGet("Contact/{id}")]
        public async Task<ActionResult<NannyResumeDetailDTO>> Contact(int id)
        {
            if (id == 0)
            {
                return BadRequest("Invalid id.");
            }

            try
            {
                var nannyResume = await _context.NannyResumes
                    .Where(c => c.Id == id)
                    .Select(c => new
                    {
                        c.Id,
                        c.NannyAccountUserAccount,
                        c.Nickname,
                        c.City,
                        c.District,
                        c.Introduction,
                        c.TypeOfDaycare,
                        c.ServiceType,
                        c.ServiceItems,
                        c.QuasiPublicChildcare,
                        c.ChildcareAvailableUnder2,
                        c.ChildcareAvailableOver2,
                        c.Language,
                        c.ServiceCenter,
                        c.ProfessionalPortrait,
                        c.DisplayControl
                    })
                    .FirstOrDefaultAsync();

                if (nannyResume == null || !nannyResume.DisplayControl.GetValueOrDefault())
                {
                    return NotFound(new { message = "該委託單不存在" });
                }

                var photos = await _context.NannyResumePhotos
                    .Where(ph => ph.IdNannyResume == id)
                    .Select(ph => new NannyResumePhotoDTO
                    {
                        Id = ph.Id,
                        IdNannyResume = ph.IdNannyResume,
                        PhotoName = ph.PhotoName,
                        ModifiedTime = ph.ModifiedTime
                    })
                    .ToListAsync();

                var evaluations = await _context.Evaluates
                    .Where(e => e.AppraiseeUserAccount == nannyResume.NannyAccountUserAccount)
                    .Select(e => new EvaluateDTO
                    {
                        Id = e.Id,
                        EvaluatorUserAccount = e.EvaluatorUserAccount,
                        AppraiseeUserAccount = e.AppraiseeUserAccount,
                        EvaluateTime = e.EvaluateTime,
                        Score = e.Score,
                        Memo = e.Memo,
                        Display = e.Display
                    })
                    .ToListAsync();

                var inquiries = await _context.Inquires
                    .Where(i => i.UserAccountresponse == nannyResume.NannyAccountUserAccount)
                    .Select(i => new InquireDTO
                    {
                        UserAccountresponse = i.UserAccountresponse,
                        UserAccountinquire = i.UserAccountinquire,
                        Times = i.Times
                    })
                    .ToListAsync();

                var serviceItemsText = nannyResume.ServiceItems switch
                {
                    0 => "無",
                    1 => "料理服務",
                    2 => "接送服務",
                    3 => "家教服務",
                    _ => "未知"
                };

                var nannyResumeDetail = new NannyResumeDetailDTO
                {
                    Id = nannyResume.Id,
                    NannyAccountUserAccount = nannyResume.NannyAccountUserAccount,
                    Nickname = nannyResume.Nickname,
                    City = nannyResume.City,
                    District = nannyResume.District,
                    Introduction = nannyResume.Introduction,
                    TypeOfDaycare = nannyResume.TypeOfDaycare,
                    ServiceType = nannyResume.ServiceType,
                    ServiceItems = serviceItemsText,
                    QuasiPublicChildcare = nannyResume.QuasiPublicChildcare,
                    ChildcareAvailableUnder2 = nannyResume.ChildcareAvailableUnder2,
                    ChildcareAvailableOver2 = nannyResume.ChildcareAvailableOver2,
                    Language = nannyResume.Language,
                    ServiceCenter = nannyResume.ServiceCenter,
                    ProfessionalPortrait = nannyResume.ProfessionalPortrait,
                    DisplayControl = nannyResume.DisplayControl,
                    Photos = photos,
                    Evaluations = evaluations,
                    Inquiries = inquiries
                };

                return Ok(nannyResumeDetail);
            }
            catch (Exception ex)
            {
                // Log the exception (use a logging framework)
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost("SubmitResume")]
        public async Task<ActionResult<int>> SubmitResume([FromBody] NannyResumeDTO model)
        {
        
            var nannyResume = new NannyResume
            {
                NannyAccountUserAccount = model.NannyAccountUserAccount,
                Nickname = model.Nickname,
                City = model.City,
                District = model.District,
                Introduction = model.Introduction,
                TypeOfDaycare = model.TypeOfDaycare,
                ServiceType = model.ServiceType,
                ServiceItems = model.ServiceItems,
                QuasiPublicChildcare = model.QuasiPublicChildcare,
                ChildcareAvailableUnder2 = model.ChildcareAvailableUnder2,
                ChildcareAvailableOver2 = model.ChildcareAvailableOver2,
                Language = model.Language,
                ServiceCenter = model.ServiceCenter,
                ProfessionalPortrait = model.ProfessionalPortrait,
                DisplayControl = model.DisplayControl
            };
            return Ok();
        }

        //private string ConvertServiceItems(int serviceItems)
        //{
        //    return serviceItems switch
        //    {
        //        0 => "無",
        //        1 => "料理服務",
        //        2 => "接送服務",
        //        3 => "家教服務",
        //        _ => "未知"
        //    };
        //}

        //private string ConvertChildcareAvailable(int available)
        //{
        //    return available switch
        //    {
        //        1 => "一位",
        //        2 => "二位",
        //        3 => "三位",
        //        4 => "四位",
        //        _ => "未知"
        //    };
        //}
    





    //    private void NotifyRecipient(string recipientAccount)
    //    {
    //        // Implement notification logic
    //        // Example: Send a message or trigger an event to notify the recipient
    //    }


    //    private string ConvertServiceItemsToString(IEnumerable<int> serviceItems)
    //    {
    //        // This is a placeholder function. Implement according to your service items logic.
    //        return string.Join(", ", serviceItems.Select(item => item.ToString()));
    //    }


    //[HttpPut("{id}")]
    //    public async Task<IActionResult> MarkAsRead(int id)
    //    {
    //        var notification = await _context.Notifications.FindAsync(id);
    //        if (notification == null)
    //        {
    //            return NotFound();
    //        }

    //        notification.IsRead = true;
    //        _context.Entry(notification).State = EntityState.Modified;
    //        await _context.SaveChangesAsync();

    //        return NoContent();
    //    }



    //[HttpPost("SubmitInquiry")]
    //public async Task<ActionResult> SubmitInquiry([FromBody] InquiryDTO model)
    //{
    //    if (model == null)
    //    {
    //        return BadRequest("Model is null");
    //    }

    //    // Save the inquiry to the database
    //    var inquiry = new Inquiry
    //    {
    //        UserAccountresponse = model.UserAccountresponse,
    //        UserAccountinquire = model.UserAccountinquire,
    //        Times = model.Times,
    //    };

    //    _context.Inquiries.Add(inquiry);
    //    await _context.SaveChangesAsync();

    //    // Notify the respondent
    //    NotifyRespondent(inquiry);

    //    return Ok(new { inquiryId = inquiry.Id });
    //}






    //PUT: api/NannyResumes/5
    //To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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
