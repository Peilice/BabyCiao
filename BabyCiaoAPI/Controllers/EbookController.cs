using BCrypt.Net;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BabyCiaoAPI.Models;
using BabyCiaoAPI.DTO;
using Microsoft.AspNetCore.Http.HttpResults;



namespace BabyCiaoAPI.Controllers
{
    [EnableCors("andy")]
    [Route("api/[controller]")]
    [ApiController]
    public class EbookController : ControllerBase
    {
        private readonly BabyciaoContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        //private readonly ContactBook _contactBook;

        public EbookController(BabyciaoContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            //_contactBook = contactBook;
            _contextAccessor = contextAccessor;
        }

        [HttpGet("GetUserName_Ebook")]
        public async Task<ActionResult<string>> GetUserName_Ebook()
        {
            var username = _contextAccessor.HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Name);
            if (username != null)
            {
                return username;
            }
            return null;
        }

        //電子聯絡簿的CRUD
        // GET: api/<EbookController>
        [HttpGet("GetEBooks")]
        public async Task<IEnumerable<EBook_DTO>> GetEBooks()
        {
            string username = _contextAccessor.HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Name);

            bool check = _context.ContactBooks.Where(c => c.ParentsIdUserAccount == username).Any();

            if (check)
            {
                var b = _context.ContactBooks.Where(c => c.ParentsIdUserAccount == username).Select(ebook => new EBook_DTO
                {
                    Id = ebook.Id,
                    ParentsIdUserAccount = ebook.ParentsIdUserAccount,
                    BabyName = ebook.BabyName,
                    Gender = ebook.Gender,
                    Birthday = ebook.Birthday,
                });
                return b;
            }
            else
            {
                return null;
            }
        }
        [HttpPost("createEbook")]
        public async Task<string> createEbook([FromBody] EBook_create_DTO DTO)
        {
            ContactBook ebook = new ContactBook()
            {
                ParentsIdUserAccount = DTO.ParentsIdUserAccount,
                BabyName = DTO.BabyName,
                Gender = DTO.Gender,
                Birthday = DTO.Birthday,
                BloodType = DTO.BloodType,
                EmergencyContact = DTO.EmergencyContact,
                EmergencyContactPhone1 = DTO.EmergencyContactPhone1,
            };
            try
            {
                _context.ContactBooks.Add(ebook);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) {
                return ex.ToString();
            }
            return "Ok";
        }

        //健康資訊的CRUD
        [HttpPost("CreateHealthInfos")]
        public async Task<ActionResult<EBook_HealthInfos_DTO>> CreateHealthInfos([FromBody] EBook_HealthInfos_DTO DTO)
        {
            HealthInformation healthInformation = new HealthInformation()
            {
                IdContactBook = DTO.IdContactBook,
                MedicalHistory = DTO.MedicalHistory,
                AllergyHistory = DTO.AllergyHistory,
                Height = DTO.Height,
                Weight = DTO.Weight,
                HeadCircumference = DTO.HeadCircumference,
                Memo = DTO.Memo,
            };
            _context.HealthInformations.AddAsync(healthInformation);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpGet("GetHealthInfos/{id}")]
        public async Task<IEnumerable<EBook_HealthInfos_DTO>> GetHealthInfos(int id)
        {
            var DTO = _context.HealthInformations.Where(h => h.IdContactBook == id).Select(healthInfor => new EBook_HealthInfos_DTO
            {
                IdContactBook = healthInfor.IdContactBook,
                MedicalHistory = healthInfor.MedicalHistory,
                AllergyHistory = healthInfor.AllergyHistory,
                Height = healthInfor.Height,
                Weight = healthInfor.Weight,
                HeadCircumference = healthInfor.HeadCircumference,
                Memo = healthInfor.Memo,
            });
            return DTO;
        }
        [HttpPut("UpdateHealthInfos")]
        public async Task<ActionResult<EBook_HealthInfos_DTO>> UpdateHealthInfos([FromBody] EBook_HealthInfos_DTO DTO)
        {
            var healthInfor = _context.HealthInformations.Where(h => h.Id == DTO.Id).FirstOrDefault();

            healthInfor.IdContactBook = DTO.IdContactBook;
            healthInfor.MedicalHistory = DTO.MedicalHistory;
            healthInfor.AllergyHistory = DTO.AllergyHistory;
            healthInfor.Height = DTO.Height;
            healthInfor.Weight = DTO.Weight;
            healthInfor.HeadCircumference = DTO.HeadCircumference;
            healthInfor.Memo = DTO.Memo;

            _context.Update(healthInfor);
            await _context.SaveChangesAsync();
            return Ok();
        }


        //餵食狀況的CRUD
        [HttpPost("CreateDietDetail")]
        public async Task<ActionResult<Ebook_DietDetail_DTO>> CreateDietDetail([FromBody] Ebook_DietDetail_DTO DTO)
        {
            DietDetail dietDetail = new DietDetail()
            {
                IdContactBook = DTO.IdContactBook,
                RecodeTime=DTO.RecodeTime,
                Type=DTO.Type,
                Amount = DTO.Amount,
                Quantity = DTO.Quantity,
                ModifiedTime = DTO.ModifiedTime,
                AccountUserAccount = DTO.AccountUserAccount,
            };
            _context.DietDetails.AddAsync(dietDetail);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpGet("GetDietDetail/{id}")]
        public async Task<IEnumerable<Ebook_DietDetail_DTO>> GetDietDetail(int id)
        {
            var DTOs = _context.DietDetails.Where(h => h.IdContactBook == id).Select(DietDetail => new Ebook_DietDetail_DTO
            {
                Category="飲食",
                Id = DietDetail.Id,
                IdContactBook = DietDetail.IdContactBook,
                RecodeTime = DietDetail.RecodeTime,
                Type = DietDetail.Type,
                Amount = DietDetail.Amount,
                Quantity = DietDetail.Quantity,
                ModifiedTime = DietDetail.ModifiedTime,
                AccountUserAccount = DietDetail.AccountUserAccount,
            });
            return DTOs;
        }
        [HttpPut("UpdateDietDetail/{id}")]
        public async Task<ActionResult<Ebook_DietDetail_DTO>> UpdateDietDetail(int id,[FromBody] Ebook_DietDetail_DTO DTO)
        {
            var DietDetail = _context.DietDetails.Find(id);

            DietDetail.IdContactBook = DTO.IdContactBook;
            DietDetail.RecodeTime = DTO.RecodeTime;
            DietDetail.Type = DTO.Type;
            DietDetail.Amount = DTO.Amount;
            DietDetail.Quantity = DTO.Quantity;
            DietDetail.ModifiedTime = DTO.ModifiedTime;
            DietDetail.AccountUserAccount = DTO.AccountUserAccount;

            _context.Update(DietDetail);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("CreateDietDetail")]
        public async Task<string> DeleteDietDetail(int id)
        {
            var target=_context.DietDetails.Find(id);
            if (target != null)
            {
                _context.DietDetails.Remove(target);
                await _context.SaveChangesAsync();
                return "刪除成功";
            }
            return "刪除失敗";
        }

        //尿布狀況的CRUD
        [HttpPost("CreateDiaperDetail")]
        public async Task<ActionResult<EBook_DiaperDetail_DTO>> CreateDiaperDetail([FromBody] EBook_DiaperDetail_DTO DTO)
        {
            DiaperDetail diaperDetail = new DiaperDetail()
            {
                IdContactBook = DTO.IdContactBook,
                RecodeTime = DTO.RecodeTime,
                Content = DTO.Content,
                BowelSituation = DTO.BowelSituation,
                ModifiedTime = DTO.ModifiedTime,
                AccountUserAccount = DTO.AccountUserAccount,
            };
            _context.DiaperDetails.AddAsync(diaperDetail);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpGet("GetDiaperDetail/{id}")]
        public async Task<IEnumerable<EBook_DiaperDetail_DTO>> GetDiaperDetail(int id)
        {
            var DTOs = _context.DiaperDetails.Where(h => h.IdContactBook == id).Select(dto => new EBook_DiaperDetail_DTO
            {
                Category = "尿布",
                Id = dto.Id,
                IdContactBook = dto.IdContactBook,
                RecodeTime = dto.RecodeTime,
                Content = dto.Content,
                BowelSituation = dto.BowelSituation,
                ModifiedTime = dto.ModifiedTime,
                AccountUserAccount = dto.AccountUserAccount,
            });
            return DTOs;
        }
        [HttpPut("UpdateDiaperDetail/{id}")]
        public async Task<ActionResult<EBook_DiaperDetail_DTO>> UpdateDiaperDetail(int id, [FromBody] EBook_DiaperDetail_DTO DTO)
        {
            var s = _context.DiaperDetails.Find(id);

            s.IdContactBook = DTO.IdContactBook;
            s.RecodeTime = DTO.RecodeTime;
            s.Content = DTO.Content;
            s.BowelSituation = DTO.BowelSituation;
            s.ModifiedTime = DTO.ModifiedTime;
            s.AccountUserAccount = DTO.AccountUserAccount;

            _context.Update(s);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("DeleteDiaperDetail")]
        public async Task<string> DeleteDiaperDetail(int id)
        {
            var target = _context.DiaperDetails.Find(id);
            if (target != null)
            {
                _context.DiaperDetails.Remove(target);
                await _context.SaveChangesAsync();
                return "刪除成功";
            }
            return "刪除失敗";
        }

        //睡眠狀態的CRUD
        [HttpPost("CreateSleepDetail")]
        public async Task<ActionResult<EBook_SleepDetail_DTO>> CreateSleepDetail([FromBody] EBook_SleepDetail_DTO DTO)
        {
            SleepDetail d = new SleepDetail()
            {
                IdContactBook = DTO.IdContactBook,
                SleepTime = DTO.SleepTime,
                WakeUpTime = DTO.WakeUpTime,
                Content = DTO.Content,
                SleepState=DTO.SleepState,
                ModifiedTime = DTO.ModifiedTime,
                AccountUserAccount = DTO.AccountUserAccount,
            };
            _context.SleepDetails.AddAsync(d);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpGet("GetSleepDetail/{id}")]
        public async Task<IEnumerable<EBook_SleepDetail_DTO>> GetSleepDetail(int id)
        {
            var DTOs = _context.SleepDetails.Where(h => h.IdContactBook == id).Select(dto => new EBook_SleepDetail_DTO
            {
                Category= "睡眠",
                Id = dto.Id,
                IdContactBook = dto.IdContactBook,
                SleepTime = dto.SleepTime,
                Content = dto.Content,
                SleepState = dto.SleepState,
                WakeUpTime = dto.WakeUpTime,
                ModifiedTime = dto.ModifiedTime,
                AccountUserAccount = dto.AccountUserAccount,
            });
            return DTOs;
        }
        [HttpPut("UpdateSleepDetail/{id}")]
        public async Task<ActionResult<EBook_SleepDetail_DTO>> UpdateSleepDetail(int id, [FromBody] EBook_SleepDetail_DTO DTO)
        {
            var s = _context.SleepDetails.Find(id);

            s.IdContactBook = DTO.IdContactBook;
            s.SleepTime = DTO.SleepTime;
            s.Content = DTO.Content;
            s.SleepState = DTO.SleepState;
            s.WakeUpTime = DTO.WakeUpTime;
            s.ModifiedTime = DTO.ModifiedTime;
            s.AccountUserAccount = DTO.AccountUserAccount;

            _context.Update(s);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("DeleteSleepDetail")]
        public async Task<string> DeleteSleepDetail(int id)
        {
            var target = _context.SleepDetails.Find(id);
            if (target != null)
            {
                _context.SleepDetails.Remove(target);
                await _context.SaveChangesAsync();
                return "刪除成功";
            }
            return "刪除失敗";
        }

        //其他事項的CRUD
        [HttpPost("CreateMemo")]
        public async Task<ActionResult<EBook_Memo_DTO>> CreateMemo([FromBody] EBook_Memo_DTO DTO)
        {
            Memo d = new Memo()
            {
                IdContactBook = DTO.IdContactBook,
                Memo1 = DTO.Memo1,
                RecodeTime = DTO.RecodeTime,
                ModifiedTime = DTO.ModifiedTime,
                AccountUserAccount = DTO.AccountUserAccount,
            };
            _context.Memos.AddAsync(d);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpGet("GetMemo/{id}")]
        public async Task<IEnumerable<EBook_Memo_DTO>> GetMemo(int id)
        {
            
            var DTOs = _context.Memos.Where(h => h.IdContactBook == id).Select(dto => new EBook_Memo_DTO
            {
                Category = "飲食",
                Id = dto.Id,
                IdContactBook = dto.IdContactBook,
                Memo1 = dto.Memo1,
                RecodeTime = dto.RecodeTime,
                ModifiedTime = dto.ModifiedTime,
                AccountUserAccount = dto.AccountUserAccount,
            });
            return DTOs;
        }
        [HttpPut("UpdateMemo/{id}")]
        public async Task<ActionResult<EBook_Memo_DTO>> UpdateMemo(int id, [FromBody] EBook_Memo_DTO DTO)
        {
            var s = _context.Memos.Find(id);

            s.IdContactBook = DTO.IdContactBook;
            s.Memo1 = DTO.Memo1;
            s.RecodeTime = DTO.RecodeTime;
            s.ModifiedTime = DTO.ModifiedTime;
            s.AccountUserAccount = DTO.AccountUserAccount;

            _context.Update(s);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("DeleteMemo")]
        public async Task<string> DeleteMemo(int id)
        {
            var target = _context.Memos.Find(id);
            if (target != null)
            {
                _context.Memos.Remove(target);
                await _context.SaveChangesAsync();
                return "刪除成功";
            }
            return "刪除失敗";
        }
    }
}
