using BabyCiaoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using BabyCiaoAPI.DTO;
using Microsoft.AspNetCore.Cors;
using System;
using Microsoft.JSInterop.Infrastructure;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.ComponentModel;


namespace BabyCiaoAPI.Controllers
{
    [EnableCors("andy")]
    [Route("api/[controller]")]
    [ApiController]
    public class OnlineCompetitionsController : ControllerBase
    {
        private readonly BabyciaoContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public OnlineCompetitionsController(BabyciaoContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        //比賽活動 (讀取所有活動、讀取單一活動及選手、報名、刪除報名)
        // GET: api/OnlineCompetitions (讀取所有活動)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OnlineCompetitionsDTO>>> Get()
        {
            var Competition = await (from com in _context.OnlineCompetitions
                                     join comp in _context.CompetitionPhotos
                                     on com.Id equals comp.IdOnlineCompetition into comps
                                     from subcomp in comps.DefaultIfEmpty()
                                     select new OnlineCompetitionsDTO
                                     {
                                         Id = com.Id,
                                         CompetitionName = com.CompetitionName,
                                         Content = com.Content,
                                         StartTime = com.StartTime,
                                         EndTime = com.EndTime,
                                         Statement = com.Statement,
                                         CompetitionPhotoNames = subcomp != null ? subcomp.PhotoName : null,
                                     }).ToListAsync();

            return Ok(Competition);
        }

        // GET api/OnlineCompetitions/{id}/{account} (讀取單一活動及所有選手)
        [HttpGet("{id}/{account}")]
        //設定回傳物件是DTO陣列(list)
        public async Task<ActionResult<List<CompetitionDetailDTO>>> voteInfo(int id, string account)
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
                               CompetitorContent=comd.Content,
                           }).ToListAsync();

            //找出票數
            var b = await (from comr in _context.CompetitionRecords
                           where comr.IdOnlineCompetition == id
                           select comr).CountAsync();

            List<int> ids = new List<int>();
            List<int> nums = new List<int>();

            //找出選手資料的id，並將結果儲存到 List<int> ids裡面
            foreach (var c in a) {
                ids.Add(c.CompetitionDetailId);
            }

            //再利用ids遍歷得票數
            //***[var num = _context.CompetitionRecords.Where(c2 => c2.IdCompetitionDetail== ???不能是var).Count();]***
            foreach (var item in ids)
            {
                var num = await _context.CompetitionRecords.Where(c2 => c2.IdCompetitionDetail == item && c2.IdOnlineCompetition == id).CountAsync();
                nums.Add(num);

            }

            //確認是否有收藏
            var like = _context.CompetitionFavorites.Any(cf => cf.IdOnlineCompetition == id && cf.AccountUserAccount == account);
            bool isLike = false;
            bool isDislike= false;
            if (like == false)
            {
                isLike = false;
                isDislike = true;

            }
            else
            {
                isLike = true;
                isDislike = false;
            }

            //尋找個人投票紀錄
            ////var vote = _context.CompetitionRecords.Where(c => c.IdOnlineCompetition == id && c.VoterAccount == account).SingleOrDefault();

            //儲存比對結果，Dictionary<isNotVote,isVote>
            ////Dictionary<bool,bool> bools = new Dictionary<bool,bool>();
            ////var voteToid = vote.IdCompetitionDetail;

            ////foreach (var item in a)
            ////{

            ////    if (voteToid == item.CompetitionDetailId)
            ////    {
            ////        bools.Add(false, true);

            ////    }
            ////    else
            ////    {
            ////        bools.Add(true, false);
            ////    }
            ////}

            var vote = _context.CompetitionRecords
                        .Where(c => c.IdOnlineCompetition == id && c.VoterAccount == account)
                        .SingleOrDefault();

            //Dictionary<bool, bool> bools = new Dictionary<bool, bool>();
            List<string> isClass = new List<string>();
            //bool isVote = false;
            //bool isNotVote = false;

            foreach (var item in a)
            {
                
                if (vote != null)
                {
                    var voteToId = vote.IdCompetitionDetail;
                    if (voteToId == item.CompetitionDetailId)
                    {
                        isClass.Add("btn-warning");
                    }
                    else
                    {
                        isClass.Add("btn-outline-warning");
                    }

                }
                else
                {
                    isClass.Add("btn-outline-warning");
                }
                   
            }

            // 更新字典值
            //bools[false] = isNotVote;
            //bools[true] = isVote;


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
                dto.CompetitorContent = a[i].CompetitorContent;
                dto.number = nums[i];
                dto.Id = a[i].Id;
                dto.allnumber = b;
                dto.IsLike = isLike;
                dto.IsDisLike = isDislike;
                dto.IsVoteorNot = isClass[i];

                //dto.IsNotVote = bools.ElementAt(i).Key;
                //dto.IsVote = bools.ElementAt(i).Value;

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
        public async Task<string> apply([FromForm] CompetitionDetail_createDTO Detail_createDTO)
        {
            //處理照片上傳
            var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            var filepath = Path.Combine(uploadPath, Detail_createDTO.CompetitionPhotoName.FileName);
            using (var fileStream = new FileStream(filepath, FileMode.Create))
            {
                await Detail_createDTO.CompetitionPhotoName.CopyToAsync(fileStream);// write file into fileStream
            }
            //Detail_createDTO.CompetitionPhotos = Detail_createDTO.CompetitionPhotoName.FileName;


            CompetitionDetail applyfor = new CompetitionDetail()
            {
                IdOnlineCompetition = Detail_createDTO.CompetitionId,
                AccountUserAccount = Detail_createDTO.AccountUserAccount,
                CompetitionPhoto = Detail_createDTO.CompetitionPhotoName.FileName,
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


        // Delete api/OnlineCompetitions/deleteApply (刪除報名)
        //[HttpDelete("deleteApply/id={id}&account=${account}")]
        [HttpDelete("deleteApply/{id}/{account}")]
        public async Task<string> deleteApply(int id, string account)
        {
            //讀出選手ID
            var delete = _context.CompetitionDetails.FirstOrDefault(c => c.IdOnlineCompetition == id && c.AccountUserAccount == account);
            int detailID = delete.Id;

            //讀出所有投票給此選手的帳號紀錄
            var votelist=_context.CompetitionRecords.Where(c=>c.IdCompetitionDetail == detailID).ToList();
            List<string> voteruser = new List<string>();
            foreach (var item in votelist)
            {
                voteruser.Add(item.VoterAccount);
            }

            //以投票者帳號與活動ID刪除投票紀錄
            foreach(var item in voteruser)
            {
                var delvote = await _context.CompetitionRecords.FirstOrDefaultAsync(c => c.IdOnlineCompetition == id && c.VoterAccount == item);
                _context.CompetitionRecords.Remove(delvote);
                await _context.SaveChangesAsync();
            }


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


        //收藏列表 (新增、刪除)
        //Delete api/OnlineCompetitions/deleteFavorite (刪除)
        [HttpDelete("deleteFavorite/{id}/{account}")]
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
        [HttpDelete("deleteVote/{id}/{account}")]
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
                                          CompetitorPhoto = comd.CompetitionPhoto,
                                          CompetitorContent = comd.Content,
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
        //Get api/OnlineCompetitions/MyVote/{account}
        [HttpGet("MyVote/{account}")]
        public async Task<ActionResult<IEnumerable<CompetitionRecordDTO>>> myVote (string account)
        {
            var vote=await(from comr in _context.CompetitionRecords
                           join com in _context.OnlineCompetitions
                           on comr.IdOnlineCompetition equals com.Id
                           join comd in _context.CompetitionDetails
                           on comr.IdCompetitionDetail equals comd.Id
                           where comr.VoterAccount == account
                           select new CompetitionRecordDTO 
                           { 
                           voteId=comr.Id,
                           voterAccount=comr.VoterAccount,
                           CompetitorName = comd.AccountUserAccount,
                           CompetitionName = com.CompetitionName,
                           Statement= com.Statement,
                           CompetitorPhoto=comd.CompetitionPhoto,
                           Content=comd.Content,
                           CompetitionID=com.Id,
                           }).ToListAsync();
            return Ok(vote);
        }


        //讀取個人收藏的活動(讀取)
        //Get api/OnlineCompetitions/
        [HttpGet("MyFavorite/{account}")]
        public async Task<ActionResult<IEnumerable<CompetitionFavoriteDTO>>> myFavorite(string account)
        {
            var Favorite = await (from com in _context.OnlineCompetitions
                                  join comf in _context.CompetitionFavorites
                                  on com.Id equals comf.IdOnlineCompetition
                                  join comp in _context.CompetitionPhotos
                                  on com.Id equals comp.IdOnlineCompetition
                                  where comf.AccountUserAccount == account
                                  select new CompetitionFavoriteDTO
                                  {
                                      FavoriteId = comf.Id,
                                      CompetitionName = com.CompetitionName,
                                      CompetitionContent = com.Content,
                                      StartTime = com.StartTime,
                                      EndTime = com.EndTime,
                                      Statement = com.Statement,
                                      CompetitionPhoto = comp.PhotoName,
                                      CompetitionId=com.Id,

                                  }).ToListAsync();

            return Ok(Favorite);
        }


    }
}
