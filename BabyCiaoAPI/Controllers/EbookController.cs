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
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;



namespace BabyCiaoAPI.Controllers
{
    [EnableCors("andy")]
    [Route("api/[controller]")]
    [ApiController]
    public class EbookController : ControllerBase
    {
        private readonly BabyciaoContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        private string image_dir = "StaticFiles/images";
        

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
                BabyPhoto= "/images/背景2.png",
            };
            try
            {
                _context.ContactBooks.Add(ebook);
                await _context.SaveChangesAsync();
                var newContactBook = _context.ContactBooks.Where(a => a.BabyName == DTO.BabyName).FirstOrDefault();
                HealthInformation healthInfos = new HealthInformation()
                {
                    IdContactBook = newContactBook.Id,
                    MedicalHistory = "無資料",
                    AllergyHistory = "無資料",
                    Height = 0,
                    Weight = 0,
                    HeadCircumference = 0,
                    ModifiedDate = DateTime.Now,
                    Memo = "無資料",
                    Age = "0歲0月",
                };
                _context.Add(healthInfos);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) {
                return ex.ToString();
            }
            return "Ok";
        }
        [HttpGet("GetEBookById/{id}")]
        public async Task<IEnumerable<EBook_DTO>> GetEBookById(int id)
        {
            string username = _contextAccessor.HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Name);

            bool check = _context.ContactBooks.Where(c => c.ParentsIdUserAccount == username).Any();

            if (check)
            {
                var b = _context.ContactBooks.Where(c => c.ParentsIdUserAccount == username && c.Id== id).Select(ebook => new EBook_DTO
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
        //寶寶基本資訊的CRUD
        [HttpPost("CreateBabyInfos")]
        public async Task<ActionResult<EBook_HealthInfos_DTO>> CreateBabyInfos([FromBody] EBook_HealthInfos_DTO DTO)
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
                Age = DTO.Age,
                ModifiedDate = DTO.ModifiedDate,
            };
            _context.HealthInformations.AddAsync(healthInformation);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpGet("getBabyInfos/{id}")]
        public async Task<ActionResult<EBook_GetBabyInfos_DTO>> getBabyInfos(int id)
        {
            var datas = _context.HealthInformations.Where(h => h.IdContactBook == id).OrderByDescending(d=>d.ModifiedDate).ToList();
            List<int> ages = new List<int>();
            foreach (var item in datas)
            {
                string[] age_str = item.Age.Split(new char[3] { '歲', '個', '月' });
                int year = int.Parse(age_str[0]);
                int months = int.Parse(age_str[1]);
                int age_num = year * 12 + months;
                ages.Add(age_num);
            }
            int maxAge = ages.Max();
            int maxIndex = ages.IndexOf(maxAge);
            var s = datas[maxIndex];


            var c = _context.ContactBooks.Where(h => h.Id == id).FirstOrDefault();

            EBook_GetBabyInfos_DTO DTO = new EBook_GetBabyInfos_DTO()
            {
                HealthInfosId = s.Id,
                IdContactBook = id,
                MedicalHistory = s.MedicalHistory,
                AllergyHistory = s.AllergyHistory,
                Height = s.Height,
                Weight = s.Weight,
                HeadCircumference = s.HeadCircumference,
                ModifiedDate = s.ModifiedDate,
                Memo = s.Memo,
                Age = s.Age,

                ParentsIdUserAccount = c.ParentsIdUserAccount,
                BabyName = c.BabyName,
                Gender = c.Gender,
                Birthday = c.Birthday,
                BloodType = c.BloodType,
                EmergencyContact = c.EmergencyContact,
                EmergencyContactPhone1 = c.EmergencyContactPhone1,
                EmergencyContactPhone2 = c.EmergencyContactPhone2,
            };


            return DTO;
        }
        [HttpGet("getHealthInfos/{id}")]
        public async Task<ActionResult<List<EBook_HealthInfos_DTO>>> getHealthInfos(int id)
        {
            var datas = _context.HealthInformations.Where(h => h.IdContactBook == id).OrderByDescending(d => d.ModifiedDate).ToList();
            
            Dictionary<EBook_HealthInfos_DTO, int> kv_datas = new Dictionary<EBook_HealthInfos_DTO, int>();
            
            foreach (var item in datas)
            {
                EBook_HealthInfos_DTO DTO = new EBook_HealthInfos_DTO();
                DTO.HealthInfosId = item.Id;
                DTO.IdContactBook = item.IdContactBook;
                DTO.MedicalHistory = item.MedicalHistory;
                DTO.AllergyHistory = item.AllergyHistory;
                DTO.Height = item.Height;
                DTO.Weight = item.Weight;
                DTO.HeadCircumference = item.HeadCircumference;
                DTO.Memo = item.Memo;
                DTO.Age = item.Age;
                DTO.ModifiedDate = item.ModifiedDate;

                string[] age_str = item.Age.Split(new char[3] { '歲', '個', '月' });
                int year = int.Parse(age_str[0]);
                int months = int.Parse(age_str[1]);
                int age_num = year * 12 + months;

                kv_datas.Add(DTO, age_num);
            }

            Dictionary<EBook_HealthInfos_DTO, int> sort_kv_datas = kv_datas.OrderByDescending(o => o.Value).ToDictionary(kv => kv.Key, kv => kv.Value);

            List<EBook_HealthInfos_DTO> new_datas = new List<EBook_HealthInfos_DTO>();
            foreach (KeyValuePair<EBook_HealthInfos_DTO, int> item in sort_kv_datas)
            {
                new_datas.Add(item.Key);
            }
            return new_datas;
        }
        [HttpPut("UpdateBabyInfos/{id}")]
        public async Task<ActionResult<string>> UpdateBabyInfos(int id,[FromForm] Ebook_UpdateBabyInfos_DTO DTO)
        {
            string URL = "StaticFiles/images/背景2.png";

            //新增公告照片
            if (DTO.BabyPhoto!=null)
            {
                URL = await CopyPictureAndGetURL(DTO.BabyPhoto);
            }
            
            var ebook = _context.ContactBooks.Where(h => h.Id == id).FirstOrDefault();
            
            ebook.BloodType = DTO.BloodType;
            ebook.EmergencyContact = DTO.EmergencyContact;
            ebook.EmergencyContactPhone1 = DTO.EmergencyContactPhone1;
            ebook.EmergencyContactPhone2 = DTO.EmergencyContactPhone2;
            ebook.BabyPhoto = URL;
            
            _context.Update(ebook);
            await _context.SaveChangesAsync();
            return Ok("更新完成");
        }
        [HttpPut("UpdateHealthInfos/{id}")]
        public async Task<ActionResult<string>> UpdateHealthInfos(int id, [FromBody] EBook_UpdateHealthInfos_DTO DTO)
        {
            var healthInfos = _context.HealthInformations.Where(h => h.Id == id).FirstOrDefault();

            healthInfos.Weight= DTO.Weight;
            healthInfos.Height= DTO.Height;
            healthInfos.HeadCircumference= DTO.HeadCircumference;
            healthInfos.ModifiedDate= DTO.ModifiedDate;

            _context.Update(healthInfos);
            await _context.SaveChangesAsync();
            return Ok("更新完成");
        }
        [HttpDelete("DeleteHealthInfos/{id}")]
        public async Task<string> DeleteHealthInfos(int id)
        {
            var target = _context.HealthInformations.Find(id);
            if (target != null)
            {
                _context.HealthInformations.Remove(target);
                await _context.SaveChangesAsync();
                return "刪除成功";
            }
            return "刪除失敗";
        }
        [HttpGet("getBabyPhoto/{id}")]
        public async Task<FileResult> getBabyPhoto(int id) 
        {
            var photo=_context.ContactBooks.Where(c=>c.Id== id).FirstOrDefault();
            string photoURL = photo.BabyPhoto;
            
            var fileBytes = await System.IO.File.ReadAllBytesAsync(photoURL);
            string miniType = GetMimeType(photoURL);

            return File(fileBytes, miniType, Path.GetFileName(photoURL));
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
        [HttpDelete("DeleteDietDetail/{id}")]
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
        [HttpDelete("DeleteDiaperDetail/{id}")]
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
                WakeUpTime = DTO.RecodeTime,
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
                RecodeTime = dto.WakeUpTime,
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
            s.WakeUpTime = DTO.RecodeTime;
            s.ModifiedTime = DTO.ModifiedTime;
            s.AccountUserAccount = DTO.AccountUserAccount;

            _context.Update(s);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("DeleteSleepDetail/{id}")]
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
                Category = "備註",
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
        [HttpDelete("DeleteMemo/{id}")]
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

        //複製檔案並依時間與隨機數取名
        private async Task<string> CopyPictureAndGetURL(IFormFile file)
        {
            


            if (file != null)
            {
                string fileExtension = Path.GetExtension(file.FileName).ToLower();
                Random r = new Random();
                string updateFileName = DateTime.Now.ToString("yyMMddHHmmss") + r.Next(1000, 10000).ToString() + fileExtension;
                string fullFileName = image_dir +"/"+ updateFileName;

                long size = file.Length;
                if (size > 0)
                {
                    using (var stream = System.IO.File.Create(fullFileName))
                    {
                        await file.CopyToAsync(stream);
                        return fullFileName;
                    }
                }
                return null;
            }
            return null;
        }

        //依讀取到的檔案類型產生該檔案類型的mini型態(file內的定義)
        private string GetMimeType(string filePath)
        {
            var extension = Path.GetExtension(filePath).ToLowerInvariant();
            return extension switch
            {
                ".jpg" => "image/jpeg",
                ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                _ => "application/octet-stream",
            };
        }
    }
}
