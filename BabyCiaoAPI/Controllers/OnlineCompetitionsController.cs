using BabyCiaoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using BabyCiaoAPI.DTO;
using Microsoft.AspNetCore.Cors;
using System;
using Microsoft.JSInterop.Infrastructure;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace BabyCiaoAPI.Controllers
{
    [EnableCors("andy")]
    [Route("api/[controller]")]
    [ApiController]
    public class OnlineCompetitionsController : ControllerBase
    {
        private readonly BabyciaoContext _context;

        public OnlineCompetitionsController(BabyciaoContext context)
        {
            _context = context;
        }

        //比賽活動 (讀取所有活動、讀取單一活動及選手、報名、刪除報名)
        // GET: api/OnlineCompetitions (讀取所有活動)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OnlineCompetitionsDTO>>> Get()
        {
            var Competition = await (from com in _context.OnlineCompetitions
                                     join comp in _context.CompetitionPhotos
                                     on com.Id equals comp.IdOnlineCompetition
                                     select new OnlineCompetitionsDTO
                                     {
                                         Id = com.Id,
                                         CompetitionName = com.CompetitionName,
                                         Content = com.Content,
                                         StartTime = com.StartTime,
                                         EndTime = com.EndTime,
                                         Statement = com.Statement,
                                         CompetitionPhotoNames = comp.PhotoName,
                                     }).ToListAsync();
            return Ok(Competition);
        }

        // GET api/OnlineCompetitions/{id} (讀取單一活動及所有選手)
        [HttpGet("{id}")]
        //設定回傳物件是DTO陣列(list)
        public async Task<ActionResult<List<CompetitionDetailDTO>>> voteInfo(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //創建要回傳的DTO陣列(list)
            List<CompetitionDetailDTO> competitionDetailDTOs = new List<CompetitionDetailDTO>();

            //利用request的id找尋資料庫內的選手資料，並轉list
            var a = await (from com in _context.OnlineCompetitions
                           join comd in _context.CompetitionDetails
                           on com.Id equals comd.IdOnlineCompetition
                           where comd.IdOnlineCompetition == id
                           select new CompetitionDetailDTO
                           {
                               Id = comd.IdOnlineCompetition,
                               CompetitionName = com.CompetitionName,
                               StartTime = com.StartTime,
                               EndTime = com.EndTime,
                               Content = com.Content,
                               Statement = com.Statement,
                               AccountUserAccount = comd.AccountUserAccount,
                               CompetitionPhotos = comd.CompetitionPhoto,
                               CompetitionDetailId = comd.Id,
                           }).ToListAsync();

            var b = await (from comr in _context.CompetitionRecords
                           //join comd in _context.CompetitionDetails
                           //on comr.IdCompetitionDetail equals comd.Id
                           where comr.IdOnlineCompetition == id
                           select comr).CountAsync();

            List<int> allnum = new List<int>();
            allnum.Add(b);

            List<int> ids = new List<int>();
            List<int> nums = new List<int>();
            //找出選手資料的id，並將結果儲存到 List<int> ids裡面
            foreach (var c in a) {
                ids.Add(c.CompetitionDetailId);
            }
            //再利用ids遍歷得票數***(
            //var num = _context.CompetitionRecords.Where(c2 => c2.IdCompetitionDetail== ???不能是var).Count();
            //)***
            foreach (var item in ids)
            {
                var num = _context.CompetitionRecords.Where(c2 => c2.IdCompetitionDetail == item && c2.IdOnlineCompetition == id).Count();
                nums.Add(num);

            }
            //將選手資料及得票數包進DTO list內
            for (int i = 0; i < nums.Count(); i++)
            {
                CompetitionDetailDTO dto = new CompetitionDetailDTO();
                dto.AccountUserAccount = a[i].AccountUserAccount;
                dto.CompetitionPhotos = a[i].CompetitionPhotos;
                dto.CompetitionDetailId = a[i].CompetitionDetailId;
                dto.CompetitionName = a[i].CompetitionName;
                dto.Content = a[i].Content;
                dto.Statement = a[i].Statement;
                dto.StartTime = a[i].StartTime;
                dto.EndTime = a[i].EndTime;
                dto.number = nums[i];
                dto.Id = a[i].Id;
                dto.allnumber = allnum[0];

                competitionDetailDTOs.Add(dto);

            }

            return Ok(competitionDetailDTOs);
        }

        //GET api/OnlineCompetitions/getRecord/{id} (暫時沒用到)
        [HttpGet("getRecord/{id}")]
        public async Task<ActionResult<IEnumerable<CompetitionDetailDTO>>> getRecord(int id)
        {
            var RecordDetail = await (from com in _context.OnlineCompetitions
                                      join comd in _context.CompetitionDetails
                                      on com.Id equals comd.IdOnlineCompetition
                                      join comr in _context.CompetitionRecords
                                      on comd.Id equals comr.IdCompetitionDetail
                                      where comr.IdCompetitionDetail == id
                                      select new CompetitionDetailDTO
                                      {
                                          Id = com.Id,
                                          CompetitionName = com.CompetitionName,
                                          StartTime = com.StartTime,
                                          EndTime = com.EndTime,
                                          Content = com.Content,
                                          Statement = com.Statement,
                                          AccountUserAccount = comd.AccountUserAccount,
                                          CompetitionPhotos = comd.CompetitionPhoto,
                                          CompetitionDetailId = comd.IdOnlineCompetition,
                                      }).ToListAsync();
            return Ok(RecordDetail);

        }

        // POST api/OnlineCompetitions/apply (報名比賽)
        [HttpPost("apply")]
        public async Task<string> apply([FromBody] CompetitionDetail_createDTO Detail_createDTO)
        {
            CompetitionDetail applyfor = new CompetitionDetail()
            {
                IdOnlineCompetition = Detail_createDTO.CompetitionId,
                AccountUserAccount = Detail_createDTO.AccountUserAccount,
                CompetitionPhoto = Detail_createDTO.CompetitionPhotos,
                Content = Detail_createDTO.Content,
            };
            try
            {
                _context.CompetitionDetails.Add(applyfor);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return "Ok";

        }

        //*****重寫*****
        // Delete api/OnlineCompetitions/deleteApply (刪除報名)
        [HttpDelete("deleteApply")]
        public async Task<string> deleteApply(int id)
        {
            var delete = _context.CompetitionDetails.Find(id);
            if (delete != null)
            {
                _context.CompetitionDetails.Remove(delete);
                await _context.SaveChangesAsync();
                return "刪除成功";
            }
            else
            {
                return "刪除失敗";
            }
        }


        //收藏列表 (讀取、新增、刪除)
        //Get api/OnlineCompetitions/getfavorite (讀取)
        [HttpGet("getfavorite")]
        public async Task<ActionResult<IEnumerable<CompetitionFavoriteDTO>>> getfavorite(string account)
        {
            var favorite = await (from com in _context.OnlineCompetitions
                                  join comf in _context.CompetitionFavorites
                                  on com.Id equals comf.IdOnlineCompetition
                                  where comf.AccountUserAccount == account
                                  select new CompetitionFavoriteDTO
                                  {
                                      FavoriteId = comf.Id,
                                      CompetitionName = com.CompetitionName,
                                      myAccount = comf.AccountUserAccount,
                                      CompetitionId = com.Id,
                                  }).ToListAsync();
            return Ok(favorite);
        }

        //Delete api/OnlineCompetitions/DeleteFavorite (刪除)
        [HttpDelete("DeleteFavorite")]
        public async Task<IActionResult> DeleteFavorite(int id, string account)
        {
            var delete = await _context.CompetitionFavorites.FirstOrDefaultAsync(c => c.IdOnlineCompetition == id && c.AccountUserAccount == account);
            if (delete != null)
            {
                _context.CompetitionFavorites.Remove(delete);
                await _context.SaveChangesAsync();
                return Ok("刪除成功");
            }
            else
            {
                return NotFound("刪除失敗");
            }
        }

        //POST api/OnlineCompetitions/createFavorite (新增)
        [HttpPost("createFavorite")]
        public async Task<string> createFavorite([FromBody] CompetitionFavorite_createDTO Favorite_createDTO)
        {
            CompetitionFavorite Favorite = new CompetitionFavorite()
            {
                IdOnlineCompetition = Favorite_createDTO.CompetitionId,
                AccountUserAccount = Favorite_createDTO.myAccount,
            };
            try
            {
                _context.CompetitionFavorites.Add(Favorite);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return "新增失敗";
            }
            return "新增成功";
        }


        //投票 (新增、刪除)
        //Post api/OnlineCompetitions/createVote (新增)
        [HttpPost("createVote")]
        public async Task<string> createVote([FromBody] CompetitionRecord_createDTO Record_createDTO)
        {
            CompetitionRecord record = new CompetitionRecord()
            {
                IdOnlineCompetition = Record_createDTO.CompetitionId,
                VoterAccount = Record_createDTO.voterAccount,
                IdCompetitionDetail = Record_createDTO.CompetitorId,
            };
            try
            {
                _context.CompetitionRecords.Add(record);
                await _context.SaveChangesAsync();
                return "投票成功";
            }
            catch
            {
                return "投票失敗";
            }
        }

        //Delete api/OnlineCompetitions/deleteVote (刪除)
        [HttpDelete("deleteVote")]
        public async Task<IActionResult> deleteVote(int id, string account)
        {
            var delete = await _context.CompetitionRecords.FirstOrDefaultAsync(c => c.IdOnlineCompetition == id && c.VoterAccount == account);
            if (delete != null)
            {
                _context.CompetitionRecords.Remove(delete);
                await _context.SaveChangesAsync();
                return Ok("刪除成功");
            }
            else
            {
                return NotFound("刪除失敗");
            }
        }


        //讀取個人報名的活動(讀取)
        //Get api/OnlineCompetitions/MyCompetition/{account}
        [HttpGet("MyCompetition/{account}")]
        public async Task<ActionResult<IEnumerable<CompetitionDetailDTO>>> MyCompetition (string account)
        {
            //讀取參加過的比賽資訊(List)
            var myCompetition = await(from com in _context.OnlineCompetitions
                                      join comd in _context.CompetitionDetails
                                      on com.Id equals comd.IdOnlineCompetition
                                      where comd.AccountUserAccount == account
                                      select new CompetitionDetailDTO
                                      {
                                          Id= com.Id,
                                          CompetitionDetailId=comd.Id,
                                          CompetitionName = com.CompetitionName,
                                          StartTime=com.StartTime,
                                          EndTime=com.EndTime,
                                          Statement = com.Statement,
                                          CompetitionPhotos=comd.CompetitionPhoto,
                                      }).ToListAsync();
            //讀取單一比賽票數(List)
            //var count = await (from comd in _context.CompetitionDetails
            //                   join comr in _context.CompetitionRecords
            //                   on comd.Id equals comr.IdCompetitionDetail
            //                   where comr.IdOnlineCompetition == id && comd.AccountUserAccount == account
            //                   select comr).CountAsync();
            //List<int> voteCount = new List<int>();
            //voteCount.Add(count);

            return Ok(myCompetition);
        }


        //讀取個人已投票的活動(讀取)


        //讀取個人收藏的活動(讀取)
        //Get api/OnlineCompetitions/



    }
}
