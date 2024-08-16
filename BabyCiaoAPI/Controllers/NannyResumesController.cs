using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BabyCiaoAPI.Models;
using Microsoft.AspNetCore.Cors;
using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;
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
        private readonly IHttpContextAccessor _contextAccessor;


        public NannyResumesController(BabyciaoContext context, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _contextAccessor = contextAccessor;
        }


        [HttpGet("GetUserName_NannyResumes")]
        public async Task<ActionResult<string>> GetUserName_NannyResumes()
        {
            var username = _contextAccessor.HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Name);
            if (username != null)
            {
                return username;
            }
            return null;
        }



        [HttpGet("GetFullinformation")]
        public async Task<ActionResult<IEnumerable<NannyResumeDTO_new>>> GetFullinformation()
        {
            try
            {
                var resume = await _context.NannyResumes.Select(c => new NannyResumeDTO_new
                {
                    Id = c.Id,
                    NannyAccountUserAccount = c.NannyAccountUserAccount,
                    Nickname = c.Nickname,
                    City = c.City,
                    District = c.District,
                    Introduction = c.Introduction,
                    TypeOfDaycare = c.TypeOfDaycare,
                    ServiceType = c.ServiceType,
                    ServiceItems = c.ServiceItems,
                    QuasiPublicChildcare = c.QuasiPublicChildcare,
                    ChildcareAvailableUnder2 = c.ChildcareAvailableUnder2,
                    ChildcareAvailableOver2 = c.ChildcareAvailableOver2,
                    Language = c.Language,
                    ServiceCenter = c.ServiceCenter,
                    ProfessionalPortrait = c.ProfessionalPortrait,
                    DisplayControl = c.DisplayControl,
                }).ToListAsync();


                return Ok(resume);
     
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
                    Nickname = resume.Nickname,
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
                    ServiceItems = resume.ServiceItems,
                    ServiceType = resume.ServiceType switch
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
                    ServiceItems = resume.ServiceItems,
                    ServiceType = resume.ServiceType switch
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
                    }).ToList()
                };

                return Ok(resumeDTO);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }



        [HttpGet("GetNannyResume")]
//        public async Task<ActionResult<IEnumerable<NannyResumeDTO_new>>> GetNannyResume()
//        {
//            try
//            {
//                // 獲取用戶名
//                string username = _contextAccessor.HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Name);

//                if (username == null)
//                {
//                    return Unauthorized("User not authenticated.");
//                }

//                // 從數據庫中獲取數據

//                var resume = _context.NannyResumes
//                    .Where(c => c.NannyAccountUserAccount == username).FirstOrDefault();



//                var inquiries = await _context.Inquires
//                    .Where(i => i.UserAccountresponse == nannyResume.NannyAccountUserAccount)
//                    .Select(i => new InquireDTO
//                    {
//                        UserAccountresponse = i.UserAccountresponse,
//                        UserAccountinquire = i.UserAccountinquire,
//                    })
//                    .ToListAsync();

//                var serviceTypeText = nannyResume.ServiceType switch

//                {
//                    return NotFound("No resumes found.");
//            }

//                // 轉換數據為 DTO
//                NannyResumeDTO_new DTO = new NannyResumeDTO_new();
//            DTO.Id = resume.Id;
//            DTO.NannyAccountUserAccount = resume.NannyAccountUserAccount;
//            DTO.Nickname = resume.Nickname;
//            DTO.City = resume.City;
//            DTO.District = resume.District;
//            DTO.Introduction = resume.Introduction;
//            DTO.TypeOfDaycare = resume.TypeOfDaycare;
//            DTO.ServiceType = resume.ServiceType;
//            DTO.ServiceItems = resume.ServiceItems;
//            DTO.QuasiPublicChildcare = resume.QuasiPublicChildcare;
//            DTO.ChildcareAvailableUnder2 = resume.ChildcareAvailableUnder2;
//            DTO.ChildcareAvailableOver2 = resume.ChildcareAvailableOver2;
//            DTO.Language = resume.Language;
//            DTO.ServiceCenter = resume.ServiceCenter;
//            DTO.ProfessionalPortrait = resume.ProfessionalPortrait;
//            DTO.DisplayControl = resume.DisplayControl;

//            return Ok(DTO);
//        }
//            catch (Exception ex)
//            {
//                // 記錄異常並返回 500 錯誤
//                // 記錄異常到日誌或控制台
//                Console.Error.WriteLine($"An error occurred: {ex.Message}");
//                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
//    }
//}



[HttpGet("GetNannyResume/{id}")]
public async Task<ActionResult<NannyResumeDTO_new>> GetNannyResume(int id)
{

    // 查找指定 Id 的 BabyResume 对象
    var resume = await _context.NannyResumes
        .Where(c => c.Id == id)
        .Select(c => new NannyResumeDTO_new
        {
            Id = c.Id,
            NannyAccountUserAccount = c.NannyAccountUserAccount,
            Nickname = c.Nickname,
            City = c.City,
            District = c.District,
            Introduction = c.Introduction,
            TypeOfDaycare = c.TypeOfDaycare,
            ServiceType = c.ServiceType,
            ServiceItems = c.ServiceItems,
            QuasiPublicChildcare = c.QuasiPublicChildcare,
            ChildcareAvailableUnder2 = c.ChildcareAvailableUnder2,
            ChildcareAvailableOver2 = c.ChildcareAvailableOver2,
            Language = c.Language,
            ServiceCenter = c.ServiceCenter,
            ProfessionalPortrait = c.ProfessionalPortrait,
            DisplayControl = c.DisplayControl
        }).FirstOrDefaultAsync();

    return Ok(resume); // 返回找到的对象

}



[HttpPost("PostNannyResume")]
public async Task<IActionResult> PostNannyResume([FromForm] NannyResumeDTO_new nannyResumeDTO, IFormFile professionalPortrait)
{
    try
    {
        if (nannyResumeDTO == null || professionalPortrait == null || professionalPortrait.Length == 0)
        {
            return BadRequest("Invalid data.");
        }

        // 保存上傳的文件
        var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "NannnyAndParent/nannyreume", professionalPortrait.FileName);
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await professionalPortrait.CopyToAsync(stream);
        }

        // 轉換 DTO 為實體
        var nannyResume = new NannyResume
        {
            NannyAccountUserAccount = nannyResumeDTO.NannyAccountUserAccount,
            Nickname = nannyResumeDTO.Nickname,
            City = nannyResumeDTO.City,
            District = nannyResumeDTO.District,
            Introduction = nannyResumeDTO.Introduction,
            TypeOfDaycare = nannyResumeDTO.TypeOfDaycare,
            ServiceType = nannyResumeDTO.ServiceType,
            ServiceItems = nannyResumeDTO.ServiceItems,
            QuasiPublicChildcare = nannyResumeDTO.QuasiPublicChildcare,
            ChildcareAvailableUnder2 = nannyResumeDTO.ChildcareAvailableUnder2,
            ChildcareAvailableOver2 = nannyResumeDTO.ChildcareAvailableOver2,
            Language = nannyResumeDTO.Language,
            ServiceCenter = nannyResumeDTO.ServiceCenter,
            ProfessionalPortrait = professionalPortrait.FileName,
        };

        _context.NannyResumes.Add(nannyResume);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetNannyResume), new { id = nannyResume.Id }, nannyResume);
    }
    catch (Exception ex)
    {
        Console.Error.WriteLine($"An error occurred: {ex.Message}");
        return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
    }
}

[HttpPost("SubmitResume")]
public async Task<ActionResult<int>> SubmitResume([FromBody] NannyResumeDTO_new model)
{
    string username = _contextAccessor.HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Name);


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
    };
    _context.NannyResumes.Add(nannyResume);
    await _context.SaveChangesAsync();

    return CreatedAtAction("GetNannyResume", new { id = nannyResume.Id }, nannyResume);
}

private string ConvertServiceItems(int ServiceType)
{
    return ServiceType switch
    {
        0 => "無",
        1 => "料理服務",
        2 => "接送服務",
        3 => "家教服務",
        _ => "未知"
    };
}

private string ConvertChildcareAvailable(int available)
{
    return available switch
    {
        1 => "一位",
        2 => "二位",
        3 => "三位",
        4 => "四位",
        _ => "未知"
    };


}
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
